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
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Project = EnvDTE.Project;
using Window = EnvDTE.Window;

namespace UnitTestBoilerplate
{
	public class CreateUnitTestBoilerplateViewModel : ViewModelBase
	{
		private readonly DTE2 dte;
		private string relativePath;
		private string className;

		private static readonly IList<string> ClassSuffixes = new List<string>
		{
			"ViewModel",
			"Service",
			"Provider",
			"Factory",
			"Manager"
		};

		public CreateUnitTestBoilerplateViewModel()
		{
			this.dte = (DTE2)ServiceProvider.GlobalProvider.GetService(typeof(DTE));


			this.TestProjects = new List<TestProject>();
			IList<Project> allProjects = Utilities.GetProjects(this.dte);

			string lastSelectedProject = Settings.GetLastSelectedProject(this.dte.Solution.FileName);


			var newProjectList = new List<TestProject>();
			foreach (Project project in allProjects)
			{
				TestProject testProject = new TestProject
				{
					Name = project.Name,
					Project = project
				};

				newProjectList.Add(testProject);
			}

			this.TestProjects = newProjectList.OrderBy(p => p.Name).ToList();

			// First see if we've saved an entry for the last selected test project for this solution.
			if (this.selectedProject == null && lastSelectedProject != null)
			{
				foreach (var project in this.TestProjects)
				{
					if (string.Equals(lastSelectedProject, project.Project.FileName, StringComparison.OrdinalIgnoreCase))
					{
						this.selectedProject = project;
						break;
					}
				}
			}

			// If we don't have an entry yet, look for a project name that contains "Test"
			if (this.selectedProject == null)
			{
				foreach (var project in this.TestProjects)
				{
					if (project.Name.ToLowerInvariant().Contains("test"))
					{
						this.selectedProject = project;
						break;
					}
				}
			}

			// Otherwise select the first project
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
					async () =>
					{
						try
						{
							IEnumerable<ProjectItemSummary> selectedFiles = Utilities.GetSelectedFiles(this.dte);
							var createdItems = new List<ProjectItem>();
							foreach (ProjectItemSummary selectedFile in selectedFiles)
							{
								createdItems.Add(await this.GenerateUnitTestFromProjectItemSummaryAsync(selectedFile));
							}

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

							Settings.SaveSelectedTestProject(this.dte.Solution.FileName, this.SelectedProject.Project.FileName);

							this.View.Close();
						}
						catch (Exception exception)
						{
							MessageBox.Show(exception.ToString());
						}
					}));
			}
		}

		private async Task<ProjectItem> GenerateUnitTestFromProjectItemSummaryAsync(ProjectItemSummary selectedFile)
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

			string unitTestContents = await this.GenerateUnitTestContentsFromFileAsync(selectedFile.FilePath);

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

		private async Task<string> GenerateUnitTestContentsFromFileAsync(string inputFilePath)
		{
			Microsoft.CodeAnalysis.Solution solution = CreateUnitTestBoilerplateCommandPackage.VisualStudioWorkspace.CurrentSolution;
			DocumentId documentId = solution.GetDocumentIdsWithFilePath(inputFilePath).FirstOrDefault();
			if (documentId == null)
			{
				this.HandleError("Could not find document in solution with file path " + inputFilePath);
			}

			var document = solution.GetDocument(documentId);

			SyntaxNode root = await document.GetSyntaxRootAsync();
			SemanticModel semanticModel = await document.GetSemanticModelAsync();

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

			NamespaceDeclarationSyntax namespaceDeclarationSyntax = null;
			if (!Utilities.TryGetParentSyntax(firstClassDeclaration, out namespaceDeclarationSyntax))
			{
				this.HandleError("Could not find class namespace.");
			}

			// Find property injection types
			var injectableProperties = new List<InjectableProperty>();

			string classFullName = namespaceDeclarationSyntax.Name + "." + classIdentifierToken;
			INamedTypeSymbol classType = semanticModel.Compilation.GetTypeByMetadataName(classFullName);

			foreach (ISymbol member in classType.GetBaseTypesAndThis().SelectMany(n => n.GetMembers()))
			{
				if (member.Kind == SymbolKind.Property)
				{
					IPropertySymbol property = (IPropertySymbol)member;

					foreach (AttributeData attribute in property.GetAttributes())
					{
						if (attribute.AttributeClass.ToString() == "Microsoft.Practices.Unity.DependencyAttribute")
						{
							injectableProperties.Add(new InjectableProperty(property.Name, property.Type.Name, property.Type.ContainingNamespace.ToString()));
						}
					}
				}
			}

			this.className = classIdentifierToken.ToString();

			// Find constructor injection types
			var constructorInjectionTypes = new List<InjectableType>();
			SyntaxNode constructorDeclaration = firstClassDeclaration.ChildNodes().FirstOrDefault(n => n.Kind() == SyntaxKind.ConstructorDeclaration);
			if (constructorDeclaration != null)
			{
				SyntaxNode parameterListNode = constructorDeclaration.ChildNodes().First(n => n.Kind() == SyntaxKind.ParameterList);
				var parameterNodes = parameterListNode.ChildNodes().Where(n => n.Kind() == SyntaxKind.Parameter);

				foreach (SyntaxNode node in parameterNodes)
				{
					SyntaxNode identifierNode = node.ChildNodes().FirstOrDefault(n => n.Kind() == SyntaxKind.IdentifierName);
					if (identifierNode != null)
					{
						SymbolInfo symbolInfo = semanticModel.GetSymbolInfo(identifierNode);

						constructorInjectionTypes.Add(
							new InjectableType(
								symbolInfo.Symbol.Name,
								symbolInfo.Symbol.ContainingNamespace.ToString()));
					}
					else
					{
						constructorInjectionTypes.Add(null);
					}
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

			return this.GenerateUnitTestContents(
				unitTestNamespace,
				this.className,
				namespaceDeclarationSyntax.Name.ToString(),
				injectableProperties,
				constructorInjectionTypes);
		}

		private string GenerateUnitTestContents(
			string unitTestNamespace,
			string className,
			string classNamespace,
			IList<InjectableProperty> properties,
			IList<InjectableType> constructorTypes)
		{
			TestFramework testFramework = Utilities.FindTestFramework(this.SelectedProject.Project);
			MockFramework mockFramework = Utilities.FindMockFramework(this.SelectedProject.Project);

			if (mockFramework == MockFramework.Unknown)
			{
				mockFramework = MockFramework.Moq;
			}

			string pascalCaseShortClassName = null;
			foreach (string suffix in ClassSuffixes)
			{
				if (className.EndsWith(suffix))
				{
					pascalCaseShortClassName = suffix;
					break;
				}
			}

			if (pascalCaseShortClassName == null)
			{
				pascalCaseShortClassName = className;
			}

			string classVariableName = pascalCaseShortClassName.Substring(0, 1).ToLowerInvariant() + pascalCaseShortClassName.Substring(1);

			List<InjectableType> injectedTypes = new List<InjectableType>(properties);
			injectedTypes.AddRange(constructorTypes.Where(t => t != null));

			var mockFields = new List<MockField>();
			foreach (InjectableType injectedType in injectedTypes)
			{
				mockFields.Add(
					new MockField(
						mockFramework == MockFramework.SimpleStubs ? "stub" + injectedType.TypeBaseName : "mock" + injectedType.TypeBaseName,
						mockFramework == MockFramework.SimpleStubs ? "Stub" + injectedType.TypeName : injectedType.TypeName));
			}

			List<string> namespaces = new List<string>();
			namespaces.AddRange(MockFrameworkAbstraction.GetUsings(mockFramework));
			namespaces.Add(TestFrameworkAbstraction.GetUsing(testFramework));
			namespaces.Add(classNamespace);
			namespaces.AddRange(injectedTypes.Select(t => t.TypeNamespace));
			namespaces = namespaces.Distinct().ToList();
			namespaces.Sort(StringComparer.Ordinal);

			StringBuilder builder = new StringBuilder();

			foreach (string ns in namespaces)
			{
				builder.AppendLine($"using {ns};");
			}

			builder.Append(
				Environment.NewLine +
				"namespace ");

			builder.Append(unitTestNamespace);
			builder.Append(
				Environment.NewLine +
				"{" + Environment.NewLine +
				$"    [{TestFrameworkAbstraction.GetTestClassAttribute(testFramework)}]" + Environment.NewLine +
				"    public class ");
			builder.Append(className);
			builder.Append(
				"Tests" + Environment.NewLine +
				"    {" + Environment.NewLine);
			if (mockFramework == MockFramework.Moq)
			{
				builder.Append("        private MockRepository mockRepository;" + Environment.NewLine);

				if (mockFields.Count > 0)
				{
					builder.AppendLine();
				}
			}

			foreach (MockField field in mockFields)
			{
				if (mockFramework == MockFramework.SimpleStubs)
				{
					builder.AppendLine($"        private {field.TypeName} {field.Name};");
				}
				else
				{
					builder.AppendLine($"        private Mock<{field.TypeName}> {field.Name};");
				}
			}

			builder.Append(
				Environment.NewLine +
				$"        [{TestFrameworkAbstraction.GetTestInitializeAttribute(testFramework)}]" + Environment.NewLine +
				"        public void TestInitialize()" + Environment.NewLine +
				"        {" + Environment.NewLine);

			if (mockFramework == MockFramework.Moq)
			{
				builder.AppendLine("            this.mockRepository = new MockRepository(MockBehavior.Strict);");

				if (mockFields.Count > 0)
				{
					builder.AppendLine();
				}
			}

			foreach (MockField field in mockFields)
			{
				string fieldCreationStatement;

				if (mockFramework == MockFramework.SimpleStubs)
				{
					fieldCreationStatement = $"new {field.TypeName}()";
				}
				else
				{
					fieldCreationStatement = $"this.mockRepository.Create<{field.TypeName}>()";
				}

				builder.AppendLine($"            this.{field.Name} = {fieldCreationStatement};");
			}

			builder.Append(
				"        }" + Environment.NewLine +
				Environment.NewLine);

			if (mockFramework == MockFramework.Moq)
			{
				builder.Append(
					$"        [{TestFrameworkAbstraction.GetTestCleanupAttribute(testFramework)}]" + Environment.NewLine +
					"        public void TestCleanup()" + Environment.NewLine +
					"        {" + Environment.NewLine +
					"            this.mockRepository.VerifyAll();" + Environment.NewLine +
					"        }" + Environment.NewLine +
					Environment.NewLine);
			}

			builder.Append(
				$"        [{TestFrameworkAbstraction.GetTestMethodAttribute(testFramework)}]" + Environment.NewLine +
				"        public void TestMethod1()" + Environment.NewLine +
				"        {" + Environment.NewLine +
				"            " + Environment.NewLine +
				"            " + Environment.NewLine);

			builder.AppendLine($"            {className} {classVariableName} = this.Create{pascalCaseShortClassName}();");
			builder.AppendLine("            ");
			builder.AppendLine("            ");
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
					string mockReferenceStatement;
					InjectableType constructorType = constructorTypes[i];
					if (constructorType == null)
					{
						mockReferenceStatement = "TODO";
					}
					else if (mockFramework == MockFramework.SimpleStubs)
					{
						mockReferenceStatement = $"this.stub{constructorType.TypeBaseName}";
					}
					else
					{
						mockReferenceStatement = $"this.mock{constructorType.TypeBaseName}.Object";
					}

					builder.Append($"                {mockReferenceStatement}");

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
					string mockReferenceStatement;
					if (mockFramework == MockFramework.SimpleStubs)
					{
						mockReferenceStatement = $"this.stub{property.TypeBaseName}";
					}
					else
					{
						mockReferenceStatement = $"this.mock{property.TypeBaseName}.Object";
					}

					builder.AppendLine($"                {property.Name} = {mockReferenceStatement},");
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
