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
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.CodeAnalysis.Formatting;
using System.Reflection;

namespace UnitTestBoilerplate
{
	public class CreateUnitTestBoilerplateViewModel : ViewModelBase
	{
		private readonly DTE2 dte;
		private string relativePath;
		private string className;

		private static readonly HashSet<string> PropertyInjectionAttributeNames = new HashSet<string>
		{
			"Microsoft.Practices.Unity.DependencyAttribute",
			"Ninject.InjectAttribute",
			"Grace.DependencyInjection.Attributes.ImportAttribute"
		};

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


			string lastSelectedProject = StaticBoilerplateSettings.GetLastSelectedProject(this.dte.Solution.FileName);


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

			this.TestFrameworkChoices = TestFrameworks.List;
			this.MockFrameworkChoices = MockFrameworks.List;

			// Populate selected test/mock frameworks based on selected project
			this.UpdateSelectedFrameworks();
		}

		public ICreateUnitTestBoilerplateView View { get; set; }

		public List<TestProject> TestProjects { get; }

		private TestProject selectedProject;
		public TestProject SelectedProject
		{
			get { return this.selectedProject; }
			set
			{
				this.Set(ref this.selectedProject, value);
				this.UpdateSelectedFrameworks();
			}
		}

		public IList<TestFramework> TestFrameworkChoices { get; }

		private TestFramework selectedTestFramework;
		public TestFramework SelectedTestFramework
		{
			get { return this.selectedTestFramework; }
			set { this.Set(ref this.selectedTestFramework, value); }
		}

		public IList<MockFramework> MockFrameworkChoices { get; }

		private MockFramework selectedMockFramework;

		public MockFramework SelectedMockFramework
		{
			get { return this.selectedMockFramework; }
			set { this.Set(ref this.selectedMockFramework, value); }
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

							StaticBoilerplateSettings.SaveSelectedTestProject(this.dte.Solution.FileName, this.SelectedProject.Project.FileName);

							this.View.Close();
						}
						catch (Exception exception)
						{
							MessageBox.Show(exception.ToString());
						}
					}));
			}
		}

		private void UpdateSelectedFrameworks()
		{
			if (this.selectedProject == null)
			{
				this.SelectedTestFramework = TestFrameworks.Default;
				this.SelectedMockFramework = MockFrameworks.Default;
			}
			else
			{
				this.SelectedTestFramework = Utilities.FindTestFramework(this.selectedProject.Project);
				this.SelectedMockFramework = Utilities.FindMockFramework(this.selectedProject.Project);
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
			if (classIdentifierToken == default(SyntaxToken))
			{
				this.HandleError("Could not find class identifier.");
			}

			NamespaceDeclarationSyntax namespaceDeclarationSyntax = null;
			if (!Utilities.TryGetParentSyntax(firstClassDeclaration, out namespaceDeclarationSyntax))
			{
				this.HandleError("Could not find class namespace.");
			}

			TestFramework testFramework = this.SelectedTestFramework;
			MockFramework mockFramework = this.SelectedMockFramework;

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
						if (PropertyInjectionAttributeNames.Contains(attribute.AttributeClass.ToString()))
						{
							var injectableProperty = InjectableProperty.TryCreateInjectableProperty(property.Name, property.Type.ToString(), mockFramework);
							if (injectableProperty != null)
							{
								injectableProperties.Add(injectableProperty);
							}
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
					constructorInjectionTypes.Add(InjectableType.TryCreateInjectableTypeFromParameterNode(node, semanticModel, mockFramework));
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

			List<InjectableType> injectedTypes = new List<InjectableType>(injectableProperties);
			injectedTypes.AddRange(constructorInjectionTypes.Where(t => t != null));

			GenerateMockNames(injectedTypes);

			// Compile information needed to generate the test
			var context = new TestGenerationContext(
				mockFramework,
				testFramework,
				unitTestNamespace,
				this.className,
				namespaceDeclarationSyntax.Name.ToString(),
				injectableProperties,
				constructorInjectionTypes,
				injectedTypes);

			return this.GenerateUnitTestContents(context);
		}

		private string GenerateUnitTestContents(TestGenerationContext context)
		{
			TestFramework testFramework = context.TestFramework;
			MockFramework mockFramework = context.MockFramework;

			string fileTemplate = StaticBoilerplateSettings.GetTemplate(testFramework, mockFramework, TemplateType.File);
			string filledTemplate = StringUtilities.ReplaceTokens(
				fileTemplate,
				(tokenName, propertyIndex, builder) =>
				{
					switch (tokenName)
					{
						case "UsingStatements":
							WriteUsings(builder, context);
							break;
						case "Namespace":
							builder.Append(context.UnitTestNamespace);
							break;
						case "MockFieldDeclarations":
							WriteMockFieldDeclarations(builder, context);
							break;
						case "MockFieldInitializations":
							WriteMockFieldInitializations(builder, context);
							break;
						case "ExplicitConstructor":
							WriteExplicitConstructor(builder, context, FindIndent(fileTemplate, propertyIndex));
							break;
						case "ClassName":
							builder.Append(context.ClassName);
							break;
						case "ClassNameShort":
							builder.Append(GetShortClassName(context.ClassName));
							break;
						case "ClassNameShortLower":
							builder.Append(GetShortClassNameLower(context.ClassName));
							break;
						default:
							// We didn't recognize it, just pass through.
							builder.Append($"${tokenName}$");
							break;
					}
				});

			SyntaxTree tree = CSharpSyntaxTree.ParseText(filledTemplate);
			SyntaxNode formattedNode = Formatter.Format(tree.GetRoot(), CreateUnitTestBoilerplateCommandPackage.VisualStudioWorkspace);

			return formattedNode.ToString();
		}

		private static void WriteUsings(StringBuilder builder, TestGenerationContext context)
		{
			List<string> namespaces = new List<string>();
			namespaces.AddRange(context.MockFramework.UsingNamespaces);
			namespaces.Add(context.TestFramework.UsingNamespace);
			namespaces.Add(context.ClassNamespace);

			foreach (InjectableType injectedType in context.InjectedTypes)
			{
				namespaces.AddRange(injectedType.TypeNamespaces);
			}

			namespaces = namespaces.Distinct().ToList();
			namespaces.Sort(StringComparer.Ordinal);

			for (int i = 0; i < namespaces.Count; i++)
			{
				builder.Append($"using {namespaces[i]};");

				if (i < namespaces.Count - 1)
				{
					builder.AppendLine();
				}
			}
		}

		private static void WriteMockFieldDeclarations(StringBuilder builder, TestGenerationContext context)
		{
			string template = StaticBoilerplateSettings.GetTemplate(context.TestFramework, context.MockFramework, TemplateType.MockFieldDeclaration);
			WriteFieldLines(builder, context, template);
		}

		private static void WriteMockFieldInitializations(StringBuilder builder, TestGenerationContext context)
		{
			string template = StaticBoilerplateSettings.GetTemplate(context.TestFramework, context.MockFramework, TemplateType.MockFieldInitialization);
			WriteFieldLines(builder, context, template);
		}

		// Works for both field declarations and initializations.
		private static void WriteFieldLines(StringBuilder builder, TestGenerationContext context, string template)
		{
			for (int i = 0; i < context.InjectedTypes.Count; i++)
			{
				InjectableType injectedType = context.InjectedTypes[i];
				string line = ReplaceInterfaceTokens(template, injectedType);

				builder.Append(line);

				if (i < context.InjectedTypes.Count - 1)
				{
					builder.AppendLine();
				}
			}
		}

		private static void WriteExplicitConstructor(StringBuilder builder, TestGenerationContext context, string currentIndent)
		{
			builder.Append($"new {context.ClassName}");

			if (context.ConstructorTypes.Count > 0)
			{
				builder.AppendLine("(");

				for (int i = 0; i < context.ConstructorTypes.Count; i++)
				{
					string mockReferenceStatement;
					InjectableType constructorType = context.ConstructorTypes[i];
					if (constructorType == null)
					{
						mockReferenceStatement = "TODO";
					}
					else
					{
						string template = StaticBoilerplateSettings.GetTemplate(context.TestFramework, context.MockFramework, TemplateType.MockObjectReference);
						mockReferenceStatement = ReplaceInterfaceTokens(template, constructorType);
					}

					builder.Append($"{currentIndent}    {mockReferenceStatement}");

					if (i < context.ConstructorTypes.Count - 1)
					{
						builder.AppendLine(",");
					}
				}

				builder.Append(")");
			}
			else if (context.Properties.Count == 0)
			{
				builder.Append("()");
			}

			if (context.Properties.Count > 0)
			{
				builder.AppendLine();
				builder.AppendLine("{");

				foreach (InjectableProperty property in context.Properties)
				{
					string template = StaticBoilerplateSettings.GetTemplate(context.TestFramework, context.MockFramework, TemplateType.MockObjectReference);
					string mockReferenceStatement = ReplaceInterfaceTokens(template, property);

					builder.AppendLine($"{property.PropertyName} = {mockReferenceStatement},");
				}

				builder.Append(@"}");
			}
		}

		private static string ReplaceInterfaceTokens(string template, InjectableType injectableType)
		{
			return template
				.Replace("$InterfaceName$", injectableType.TypeName)
				.Replace("$InterfaceNameBase$", injectableType.TypeBaseName)
				.Replace("$InterfaceType$", injectableType.ToString())
				.Replace("$InterfaceMockName$", injectableType.MockName);
		}

		private static void GenerateMockNames(List<InjectableType> injectedTypes)
		{
			// Group them by TypeBaseName to see which ones need a more unique name
			var results = from t in injectedTypes
						  group t by t.TypeBaseName into g
						  select new { TypeBaseName = g.Key, Types = g.ToList() };

			foreach (var result in results)
			{
				if (result.Types.Count == 1)
				{
					result.Types[0].MockName = result.TypeBaseName;
				}
				else
				{
					foreach (var injectedType in result.Types)
					{
						injectedType.MockName = injectedType.LongMockName;
					}
				}
			}
		}

		private static string FindIndent(string template, int currentIndex)
		{
			// Go back and find line start
			int lineStart = -1;
			for (int i = currentIndex - 1; i >= 0; i--)
			{
				char c = template[i];
				if (c == '\n')
				{
					lineStart = i + 1;
					break;
				}
			}

			// Go forward and find first non-whitespace character
			for (int i = lineStart; i <= currentIndex; i++)
			{
				char c = template[i];
				if (c != ' ' && c != '\t')
				{
					return template.Substring(lineStart, i - lineStart);
				}
			}

			return string.Empty;
		}

		private static string GetShortClassName(string className)
		{
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

			return pascalCaseShortClassName;
		}

		private static string GetShortClassNameLower(string className)
		{
			string shortName = GetShortClassName(className);
			return shortName.Substring(0, 1).ToLowerInvariant() + shortName.Substring(1);
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
