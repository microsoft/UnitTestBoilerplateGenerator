using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EnvDTE;
using EnvDTE80;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Window = EnvDTE.Window;

namespace UnitTestBoilerplate
{
    public class CreateUnitTestBoilerplateViewModel : ViewModelBase
    {
        private readonly DTE2 dte;
        private string relativePath;
        private string className;

        public CreateUnitTestBoilerplateViewModel()
        {
            this.dte = (DTE2)ServiceProvider.GlobalProvider.GetService(typeof(DTE));

            this.TestProjects = new List<TestProject>();
            IList<Project> allProjects = Utilities.GetProjects(this.dte);

            foreach (Project project in allProjects)
            {
                TestProject testProject = new TestProject
                {
                    Name = project.Name,
                    Project = project
                };

                this.TestProjects.Add(testProject);

                if (this.selectedProject == null && project.Name.ToLowerInvariant().Contains("test"))
                {
                    this.selectedProject = testProject;
                }
            }

            if (this.selectedProject == null && this.TestProjects.Count > 0)
            {
                this.selectedProject = this.TestProjects[0];
            }
        }

        public ICreateUnitTestBoilerplateView View { get; set; }

        public List<TestProject> TestProjects { get; }

        private TestProject selectedProject;

        public TestProject SelectedProject
        {
            get { return this.selectedProject; }
            set { this.Set(ref this.selectedProject, value); }
        }

        private RelayCommand createUnitTestCommand;

        public RelayCommand CreateUnitTestCommand
        {
            get
            {
                return this.createUnitTestCommand ?? (this.createUnitTestCommand = new RelayCommand(
                    () =>
                    {
                        try
                        {
                            IEnumerable<ProjectItemSummary> selectedFiles = Utilities.GetSelectedFiles(this.dte);
                            IEnumerable<ProjectItem> createdItems = selectedFiles.Select(this.GenerateUnitTestFromProjectItemSummary);

                            bool focusSet = false;
                            foreach (ProjectItem createdItem in createdItems)
                            {
                                Window testWindow = createdItem.Open(EnvDTE.Constants.vsViewKindCode);
                                testWindow.Visible = true;

                                if (!focusSet)
                                {
                                    testWindow.SetFocus();
                                    focusSet = true;
                                }
                            }

                            this.View.Close();
                        }
                        catch (Exception)
                        {
                        }
                    }));
            }
        }

        private ProjectItem GenerateUnitTestFromProjectItemSummary(ProjectItemSummary selectedFile)
        {
            string projectDirectory = Path.GetDirectoryName(selectedFile.ProjectFilePath);
            string selectedFileDirectory = Path.GetDirectoryName(selectedFile.FilePath);

            if (projectDirectory == null || selectedFileDirectory == null || !selectedFileDirectory.StartsWith(projectDirectory, StringComparison.OrdinalIgnoreCase))
            {
                this.HandleError("Error with selected file paths.");
            }

            this.relativePath = selectedFileDirectory.Substring(projectDirectory.Length);
            if (relativePath.StartsWith("\\", StringComparison.Ordinal))
            {
                relativePath = relativePath.Substring(1);
            }

            string unitTestContents = this.GenerateUnitTestContentsFromFile(selectedFile.FilePath);

            string testFolder = Path.Combine(this.SelectedProject.ProjectDirectory, this.relativePath);
            string testPath = Path.Combine(testFolder, this.className + "Tests.cs");

            if (File.Exists(testPath))
            {
                this.HandleError("Test file already exists.");
            }

            if (!Directory.Exists(testFolder))
            {
                Directory.CreateDirectory(testFolder);
            }

            File.WriteAllText(testPath, unitTestContents);

            // Add the file to project
            ProjectItem testItem = this.SelectedProject.Project.ProjectItems.AddFromFile(testPath);
            testItem.ExpandView();

            return testItem;
        }

        private string GenerateUnitTestContentsFromFile(string inputFilePath)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(File.ReadAllText(inputFilePath));
            SyntaxNode root = tree.GetRoot();
            SyntaxNode firstClassDeclaration = root.DescendantNodes().FirstOrDefault(node => node.Kind() == SyntaxKind.ClassDeclaration);

            if (firstClassDeclaration == null)
            {
                this.HandleError("Could not find class declaration.");
            }

            if (firstClassDeclaration.ChildTokens().Any(node => node.Kind() == SyntaxKind.AbstractKeyword))
            {
                this.HandleError("Cannot unit test an abstract class.");
            }

            SyntaxToken classIdentifierToken = firstClassDeclaration.ChildTokens().FirstOrDefault(n => n.Kind() == SyntaxKind.IdentifierToken);
            if (classIdentifierToken == null)
            {
                this.HandleError("Could not find class identifier.");
            }

            this.className = classIdentifierToken.ToString();

            // Find injectable properties
            var injectableProperties = new List<InjectableProperty>();
            foreach (SyntaxNode propertyDeclaration in firstClassDeclaration.ChildNodes().Where(node => node.Kind() == SyntaxKind.PropertyDeclaration))
            {
                var attributeListNode = propertyDeclaration.ChildNodes().FirstOrDefault(n => n.Kind() == SyntaxKind.AttributeList);
                if (attributeListNode == null)
                {
                    break;
                }

                bool propertyIsPublic = propertyDeclaration.ChildTokens().Any(n => n.Kind() == SyntaxKind.PublicKeyword);
                if (!propertyIsPublic)
                {
                    // If the property is not marked as public it's not exposed.
                    break;
                }

                var attributeNodeList = attributeListNode.ChildNodes().Where(n => n.Kind() == SyntaxKind.Attribute);
                foreach (var attribute in attributeNodeList)
                {
                    SyntaxNode attributeNameNode = attribute.ChildNodes().FirstOrDefault(n => n.Kind() == SyntaxKind.IdentifierName);
                    if (attributeNameNode != null)
                    {
                        if (attributeNameNode.ToString() == "Dependency")
                        {
                            SyntaxNode typeNameNode = propertyDeclaration.ChildNodes().FirstOrDefault(n => n.Kind() == SyntaxKind.IdentifierName);
                            SyntaxToken propertyNameToken = propertyDeclaration.ChildTokens().FirstOrDefault(n => n.Kind() == SyntaxKind.IdentifierToken);

                            if (typeNameNode != null && propertyNameToken != null)
                            {
                                injectableProperties.Add(new InjectableProperty(propertyNameToken.ToString(), typeNameNode.ToString()));
                            }
                        }
                    }
                }
            }

            // Find constructor injection types
            var constructorInjectionTypes = new List<string>();
            SyntaxNode constructorDeclaration = firstClassDeclaration.ChildNodes().FirstOrDefault(n => n.Kind() == SyntaxKind.ConstructorDeclaration);
            if (constructorDeclaration != null)
            {
                SyntaxNode parameterListNode = constructorDeclaration.ChildNodes().First(n => n.Kind() == SyntaxKind.ParameterList);
                var parameterNodes = parameterListNode.ChildNodes().Where(n => n.Kind() == SyntaxKind.Parameter);

                foreach (SyntaxNode node in parameterNodes)
                {
                    SyntaxNode identifierNode = node.ChildNodes().First(n => n.Kind() == SyntaxKind.IdentifierName);
                    constructorInjectionTypes.Add(identifierNode.ToString());
                }
            }

            string unitTestNamespace;
            string defaultNamespace = this.SelectedProject.Project.Properties.Item("DefaultNamespace").Value as string;

            if (string.IsNullOrEmpty(this.relativePath))
            {
                unitTestNamespace = defaultNamespace;
            }
            else
            {
                List<string> defaultNamespaceParts = defaultNamespace.Split('.').ToList();
                List<string> unitTestNamespaceParts = new List<string>(defaultNamespaceParts);
                unitTestNamespaceParts.AddRange(this.relativePath.Split('\\'));

                unitTestNamespace = string.Join(".", unitTestNamespaceParts);
            }

            return this.GenerateUnitTestContents(unitTestNamespace, this.className, injectableProperties, constructorInjectionTypes);
        }

        private string GenerateUnitTestContents(string unitTestNamespace, string className, IList<InjectableProperty> properties, IList<string> constructorTypes)
        {
            string pascalCaseShortClassName;
            if (className.EndsWith("ViewModel"))
            {
                pascalCaseShortClassName = "ViewModel";
            }
            else if (className.EndsWith("Service"))
            {
                pascalCaseShortClassName = "Service";
            }
            else
            {
                pascalCaseShortClassName = className;
            }

            string classVariableName = pascalCaseShortClassName.Substring(0, 1).ToLowerInvariant() + pascalCaseShortClassName.Substring(1);

            var mockFields = new List<MockField>();

            foreach (InjectableProperty property in properties)
            {
                mockFields.Add(new MockField("mock" + property.TypeBaseName, property.TypeName));
            }

            foreach (string constructorType in constructorTypes)
            {
                mockFields.Add(new MockField("mock" + Utilities.GetTypeBaseName(constructorType), constructorType));
            }

            StringBuilder builder = new StringBuilder();

            builder.Append(
                "using Microsoft.VisualStudio.TestTools.UnitTesting;" + Environment.NewLine +
                "using Moq;" + Environment.NewLine +
                "using System;" + Environment.NewLine +
                Environment.NewLine +
                "namespace ");

            builder.Append(unitTestNamespace);
            builder.Append(
                Environment.NewLine +
                "{" + Environment.NewLine +
                "    [TestClass]" + Environment.NewLine + 
                "    public class ");
            builder.Append(className);
            builder.Append(
                "Tests" + Environment.NewLine +
                "    {" + Environment.NewLine +
                "        private MockRepository mockRepository;" + Environment.NewLine);

            if (mockFields.Count > 0)
            {
                builder.AppendLine();
            }

            foreach (MockField field in mockFields)
            {
                builder.AppendLine($"        private Mock<{field.TypeName}> {field.Name};");
            }

            builder.Append(
                Environment.NewLine +
                @"        [TestInitialize]" + Environment.NewLine +
                "        public void TestInitialize()" + Environment.NewLine +
                "        {" + Environment.NewLine +
                "            this.mockRepository = new MockRepository(MockBehavior.Strict);" + Environment.NewLine);

            if (mockFields.Count > 0)
            {
                builder.AppendLine();
            }

            foreach (MockField field in mockFields)
            {
                builder.AppendLine($"            this.{field.Name} = this.mockRepository.Create<{field.TypeName}>();");
            }

            builder.Append(
                @"        }" + Environment.NewLine +
                Environment.NewLine +
                "        [TestMethod]" + Environment.NewLine +
                "        public void TestMethod1()" + Environment.NewLine +
                "        {" + Environment.NewLine +
                "            ");

            builder.AppendLine($"{className} {classVariableName} = this.Create{pascalCaseShortClassName}();");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine($"        private {className} Create{pascalCaseShortClassName}()");
            builder.AppendLine("        {");
            builder.Append($"            return new {className}");

            if (constructorTypes.Count > 0)
            {
                builder.AppendLine("(");

                for (int i = 0; i < constructorTypes.Count; i++)
                {
                    builder.Append($"                this.mock{Utilities.GetTypeBaseName(constructorTypes[i])}.Object");

                    if (i < constructorTypes.Count - 1)
                    {
                        builder.AppendLine(",");
                    }
                }

                builder.Append(")");
            }
            else if (properties.Count == 0)
            {
                builder.Append("()");
            }

            if (properties.Count > 0)
            {
                builder.AppendLine();
                builder.AppendLine("            {");

                foreach (InjectableProperty property in properties)
                {
                    builder.AppendLine($"                {property.Name} = this.mock{property.TypeBaseName}.Object,");
                }

                builder.Append(@"            }");
            }

            builder.AppendLine(";");
            builder.AppendLine("        }");
            builder.AppendLine("    }");
            builder.AppendLine("}");

            return builder.ToString();
        }

        private void HandleError(string message)
        {
            MessageBox.Show(message);
            throw new InvalidOperationException(message);
        }
    }

    public interface ICreateUnitTestBoilerplateView
    {
        void Close();
    }
}
