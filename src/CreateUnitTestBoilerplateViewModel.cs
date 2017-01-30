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
		}

		private IBoilerplateSettings GetSettings()
		{
			var componentModel = (IComponentModel)ServiceProvider.GlobalProvider.GetService(typeof(SComponentModel));
			return componentModel.DefaultExportProvider.GetExportedValue<IBoilerplateSettings>();
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

			List<InjectableType> injectedTypes = new List<InjectableType>(injectableProperties);
			injectedTypes.AddRange(constructorInjectionTypes.Where(t => t != null));

			// Compile information needed to generate the test
			var context = new TestGenerationContext(
				Utilities.FindMockFramework(this.SelectedProject.Project),
				Utilities.FindTestFramework(this.SelectedProject.Project),
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

			string fileTemplate = StaticBoilerplateSettings.GetTemplate(mockFramework, TemplateType.File);
			var builder = new StringBuilder();

			for (int i = 0; i < fileTemplate.Length; i++)
			{
				char c = fileTemplate[i];
				if (c == '$')
				{
					int endIndex = -1;
					for (int j = i + 1; j < fileTemplate.Length; j++)
					{
						if (fileTemplate[j] == '$')
						{
							endIndex = j;
							break;
						}
					}

					if (endIndex < 0)
					{
						// We couldn't find the end index for the replacement property name. Continue.
						builder.Append(c);
					}
					else
					{
						// Calculate values on demand from switch statement. Some are preset values, some need a bit of calc like base name,
						// some are dependent on the test framework (attributes), some need to pull down other templates and loop through mock fields
						string propertyName = fileTemplate.Substring(i + 1, endIndex - i - 1);
						switch (propertyName)
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
								WriteExplicitConstructor(builder, context, FindIndent(fileTemplate, i));
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
							case "TestClassAttribute":
								builder.Append(TestFrameworkAbstraction.GetTestClassAttribute(testFramework));
								break;
							case "TestInitializeAttribute":
								builder.Append(TestFrameworkAbstraction.GetTestInitializeAttribute(testFramework));
								break;
							case "TestCleanupAttribute":
								builder.Append(TestFrameworkAbstraction.GetTestCleanupAttribute(testFramework));
								break;
							case "TestMethodAttribute":
								builder.Append(TestFrameworkAbstraction.GetTestMethodAttribute(testFramework));
								break;
							default:
								// We didn't recognize it, just pass through.
								builder.Append($"${propertyName}$");
								break;
						}

						i = endIndex;
					}
				}
				else
				{
					builder.Append(c);
				}
			}

			SyntaxTree tree = CSharpSyntaxTree.ParseText(builder.ToString());
			SyntaxNode formattedNode = Formatter.Format(tree.GetRoot(), CreateUnitTestBoilerplateCommandPackage.VisualStudioWorkspace);

			return formattedNode.ToString();
		}

		private static void WriteUsings(StringBuilder builder, TestGenerationContext context)
		{
			List<string> namespaces = new List<string>();
			namespaces.AddRange(MockFrameworkAbstraction.GetUsings(context.MockFramework));
			namespaces.Add(TestFrameworkAbstraction.GetUsing(context.TestFramework));
			namespaces.Add(context.ClassNamespace);
			namespaces.AddRange(context.InjectedTypes.Select(t => t.TypeNamespace));
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
			string template = StaticBoilerplateSettings.GetTemplate(context.MockFramework, TemplateType.MockFieldDeclaration);
			WriteFieldLines(builder, context, template);
		}

		private static void WriteMockFieldInitializations(StringBuilder builder, TestGenerationContext context)
		{
			string template = StaticBoilerplateSettings.GetTemplate(context.MockFramework, TemplateType.MockFieldInitialization);
			WriteFieldLines(builder, context, template);
		}

		// Works for both field declarations and initializations.
		private static void WriteFieldLines(StringBuilder builder, TestGenerationContext context, string template)
		{
			for (int i = 0; i < context.InjectedTypes.Count; i++)
			{
				InjectableType injectedType = context.InjectedTypes[i];
				string line = template
					.Replace("$InterfaceName$", injectedType.TypeName)
					.Replace("$InterfaceNameBase$", injectedType.TypeBaseName);

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
						string template = StaticBoilerplateSettings.GetTemplate(context.MockFramework, TemplateType.MockObjectReference);
						mockReferenceStatement = template
							.Replace("$InterfaceName$", constructorType.TypeName)
							.Replace("$InterfaceNameBase$", constructorType.TypeBaseName);
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
					string template = StaticBoilerplateSettings.GetTemplate(context.MockFramework, TemplateType.MockObjectReference);
					string mockReferenceStatement = template
						.Replace("$InterfaceName$", property.TypeName)
						.Replace("$InterfaceNameBase$", property.TypeBaseName);

					builder.AppendLine($"{property.Name} = {mockReferenceStatement},");
				}

				builder.Append(@"}");
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

		private string GenerateUnitTestContentsOld(
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
				$"[{TestFrameworkAbstraction.GetTestClassAttribute(testFramework)}]" + Environment.NewLine +
				"public class ");
			builder.Append(className);
			builder.Append(
				"Tests" + Environment.NewLine +
				"{" + Environment.NewLine);
			if (mockFramework == MockFramework.Moq)
			{
				builder.Append("private MockRepository mockRepository;" + Environment.NewLine);

				if (mockFields.Count > 0)
				{
					builder.AppendLine();
				}
			}

			foreach (MockField field in mockFields)
			{
				if (mockFramework == MockFramework.SimpleStubs)
				{
					builder.AppendLine($"private {field.TypeName} {field.Name};");
				}
				else
				{
					builder.AppendLine($"private Mock<{field.TypeName}> {field.Name};");
				}
			}

			builder.Append(
				Environment.NewLine +
				$"[{TestFrameworkAbstraction.GetTestInitializeAttribute(testFramework)}]" + Environment.NewLine +
				"public void TestInitialize()" + Environment.NewLine +
				"{" + Environment.NewLine);

			if (mockFramework == MockFramework.Moq)
			{
				builder.AppendLine("this.mockRepository = new MockRepository(MockBehavior.Strict);");

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

				builder.AppendLine($"this.{field.Name} = {fieldCreationStatement};");
			}

			builder.Append(
				"}" + Environment.NewLine +
				Environment.NewLine);

			if (mockFramework == MockFramework.Moq)
			{
				builder.Append(
					$"[{TestFrameworkAbstraction.GetTestCleanupAttribute(testFramework)}]" + Environment.NewLine +
					"public void TestCleanup()" + Environment.NewLine +
					"{" + Environment.NewLine +
					"this.mockRepository.VerifyAll();" + Environment.NewLine +
					"}" + Environment.NewLine +
					Environment.NewLine);
			}

			builder.Append(
				$"[{TestFrameworkAbstraction.GetTestMethodAttribute(testFramework)}]" + Environment.NewLine +
				"public void TestMethod1()" + Environment.NewLine +
				"{" + Environment.NewLine +
				"" + Environment.NewLine +
				"" + Environment.NewLine);

			builder.AppendLine($"{className} {classVariableName} = this.Create{pascalCaseShortClassName}();");
			builder.AppendLine("");
			builder.AppendLine("");
			builder.AppendLine("}");
			builder.AppendLine();
			builder.AppendLine($"private {className} Create{pascalCaseShortClassName}()");
			builder.AppendLine("{");
			builder.Append($"return new {className}");

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

					builder.Append($"    {mockReferenceStatement}");

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
				builder.AppendLine("{");

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

					builder.AppendLine($"{property.Name} = {mockReferenceStatement},");
				}

				builder.Append(@"}");
			}

			builder.AppendLine(";");
			builder.AppendLine("}");
			builder.AppendLine("}");
			builder.AppendLine("}");

			SyntaxTree tree = CSharpSyntaxTree.ParseText(builder.ToString());
			SyntaxNode formattedNode = Formatter.Format(tree.GetRoot(), CreateUnitTestBoilerplateCommandPackage.VisualStudioWorkspace);

			return formattedNode.ToString();

			//return builder.ToString();
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
