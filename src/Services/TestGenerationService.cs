using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using UnitTestBoilerplate.Model;
using UnitTestBoilerplate.Utilities;

namespace UnitTestBoilerplate.Services
{
	[Export(typeof(ITestGenerationService))]
    public class TestGenerationService : ITestGenerationService
    {
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
			"Manager",
			"Component"
		};

		[Import]
		internal IBoilerplateSettings Settings { get; set; }

		public async Task<string> GenerateUnitTestFileAsync(
			ProjectItemSummary selectedFile,
			EnvDTE.Project targetProject, 
			TestFramework testFramework,
			MockFramework mockFramework)
		{
			string sourceProjectDirectory = Path.GetDirectoryName(selectedFile.ProjectFilePath);
			string selectedFileDirectory = Path.GetDirectoryName(selectedFile.FilePath);

			if (sourceProjectDirectory == null || selectedFileDirectory == null || !selectedFileDirectory.StartsWith(sourceProjectDirectory, StringComparison.OrdinalIgnoreCase))
			{
				throw new InvalidOperationException("Error with selected file paths.");
			}

			string relativePath = this.GetRelativePath(selectedFile);

			TestGenerationContext context = await this.CollectTestGenerationContextAsync(selectedFile, targetProject, testFramework, mockFramework);

			string unitTestContents = this.GenerateUnitTestContents(context);

			string testFolder = Path.Combine(Path.GetDirectoryName(targetProject.FullName), relativePath);

			string testFileNameBase = StringUtilities.ReplaceTokens(
				this.Settings.FileNameTemplate,
				(tokenName, propertyIndex, builder) =>
				{
					if (WriteGlobalToken(tokenName, builder, context))
					{
						return;
					}

					WriteTokenPassthrough(tokenName, builder);
				});

			testFileNameBase = FileUtilities.CleanFileName(testFileNameBase);

			string testPath = Path.Combine(testFolder, testFileNameBase + ".cs");

			if (File.Exists(testPath))
			{
				throw new InvalidOperationException("Test file already exists.");
			}

			if (!Directory.Exists(testFolder))
			{
				Directory.CreateDirectory(testFolder);
			}

			File.WriteAllText(testPath, unitTestContents);

			return testPath;
		}

		public async Task GenerateUnitTestFileAsync(
			ProjectItemSummary selectedFile,
			string targetFilePath,
			string targetProjectNamespace,
			TestFramework testFramework,
			MockFramework mockFramework)
		{
			string sourceProjectDirectory = Path.GetDirectoryName(selectedFile.ProjectFilePath);
			string selectedFileDirectory = Path.GetDirectoryName(selectedFile.FilePath);

			if (sourceProjectDirectory == null || selectedFileDirectory == null || !selectedFileDirectory.StartsWith(sourceProjectDirectory, StringComparison.OrdinalIgnoreCase))
			{
				throw new InvalidOperationException("Error with selected file paths.");
			}

			TestGenerationContext context = await this.CollectTestGenerationContextAsync(selectedFile, targetProjectNamespace, testFramework, mockFramework);

			string unitTestContents = this.GenerateUnitTestContents(context);

			string testFolder = Path.GetDirectoryName(targetFilePath);

			if (File.Exists(targetFilePath))
			{
				throw new InvalidOperationException("Test file already exists.");
			}

			if (!Directory.Exists(testFolder))
			{
				Directory.CreateDirectory(testFolder);
			}

			File.WriteAllText(targetFilePath, unitTestContents);
		}

		public string GetRelativePath(ProjectItemSummary selectedFile)
		{
			string projectDirectory = Path.GetDirectoryName(selectedFile.ProjectFilePath);
			string selectedFileDirectory = Path.GetDirectoryName(selectedFile.FilePath);

			if (projectDirectory == null || selectedFileDirectory == null || !selectedFileDirectory.StartsWith(projectDirectory, StringComparison.OrdinalIgnoreCase))
			{
				throw new InvalidOperationException("Error with selected file paths.");
			}

			string relativePath = selectedFileDirectory.Substring(projectDirectory.Length);
			if (relativePath.StartsWith("\\", StringComparison.Ordinal))
			{
				relativePath = relativePath.Substring(1);
			}

			return relativePath;
		}

		private async Task<TestGenerationContext> CollectTestGenerationContextAsync(
			ProjectItemSummary selectedFile, 
			string targetProjectNamespace, 
			TestFramework testFramework,
			MockFramework mockFramework)
		{
			Microsoft.CodeAnalysis.Solution solution = CreateUnitTestBoilerplateCommandPackage.VisualStudioWorkspace.CurrentSolution;
			DocumentId documentId = solution.GetDocumentIdsWithFilePath(selectedFile.FilePath).FirstOrDefault();
			if (documentId == null)
			{
				throw new InvalidOperationException("Could not find document in solution with file path " + selectedFile.FilePath);
			}

			var document = solution.GetDocument(documentId);

			SyntaxNode root = await document.GetSyntaxRootAsync();
			SemanticModel semanticModel = await document.GetSemanticModelAsync();

			SyntaxNode firstClassDeclaration = root.DescendantNodes().FirstOrDefault(node => node.Kind() == SyntaxKind.ClassDeclaration);

			if (firstClassDeclaration == null)
			{
				throw new InvalidOperationException("Could not find class declaration.");
			}

			if (firstClassDeclaration.ChildTokens().Any(node => node.Kind() == SyntaxKind.AbstractKeyword))
			{
				throw new InvalidOperationException("Cannot unit test an abstract class.");
			}

			SyntaxToken classIdentifierToken = firstClassDeclaration.ChildTokens().FirstOrDefault(n => n.Kind() == SyntaxKind.IdentifierToken);
			if (classIdentifierToken == default(SyntaxToken))
			{
				throw new InvalidOperationException("Could not find class identifier.");
			}

			NamespaceDeclarationSyntax namespaceDeclarationSyntax = null;
			if (!TypeUtilities.TryGetParentSyntax(firstClassDeclaration, out namespaceDeclarationSyntax))
			{
				throw new InvalidOperationException("Could not find class namespace.");
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

			string className = classIdentifierToken.ToString();

			// Find constructor injection types
			List<InjectableType> constructorInjectionTypes = new List<InjectableType>();

			SyntaxNode constructorDeclaration = firstClassDeclaration.ChildNodes().FirstOrDefault(n => n.Kind() == SyntaxKind.ConstructorDeclaration);

			if (constructorDeclaration != null)
			{
				constructorInjectionTypes.AddRange(
					GetParameterListNodes(constructorDeclaration)
					.Select(node => InjectableType.TryCreateInjectableTypeFromParameterNode(node, semanticModel, mockFramework)));
			}

			// Find public method declarations
			IList<MethodDescriptor> methodDeclarations = new List<MethodDescriptor>();
			foreach (MethodDeclarationSyntax methodDeclaration in
				firstClassDeclaration.ChildNodes().Where(
					n => n.Kind() == SyntaxKind.MethodDeclaration
					&& ((MethodDeclarationSyntax)n).Modifiers.Any(m => m.IsKind(SyntaxKind.PublicKeyword))))
			{
				var parameterList = GetParameterListNodes(methodDeclaration).ToList();

				string[] parameterTypeNames = GetParameterTypeNames(parameterList);

				var isAsync = methodDeclaration.Modifiers.Any(m => m.IsKind(SyntaxKind.AsyncKeyword));

				methodDeclarations.Add(new MethodDescriptor(methodDeclaration.Identifier.Text, parameterTypeNames, isAsync));
			}

			string unitTestNamespace;

			string relativePath = this.GetRelativePath(selectedFile);

			if (string.IsNullOrEmpty(relativePath))
			{
				unitTestNamespace = targetProjectNamespace;
			}
			else
			{
				List<string> defaultNamespaceParts = targetProjectNamespace.Split('.').ToList();
				List<string> unitTestNamespaceParts = new List<string>(defaultNamespaceParts);
				unitTestNamespaceParts.AddRange(relativePath.Split('\\'));

				unitTestNamespace = string.Join(".", unitTestNamespaceParts);
			}

			List<InjectableType> injectedTypes = new List<InjectableType>(injectableProperties);
			injectedTypes.AddRange(constructorInjectionTypes.Where(t => t != null));

			GenerateMockNames(injectedTypes);

			return new TestGenerationContext(
				mockFramework,
				testFramework,
				unitTestNamespace,
				className,
				namespaceDeclarationSyntax.Name.ToString(),
				injectableProperties,
				constructorInjectionTypes,
				injectedTypes,
				methodDeclarations);
		}

		private static string[] GetParameterTypeNames(List<ParameterSyntax> parameterList)
		{
			string[] parameterTypeNames = new string[parameterList.Count()];

			for (int i = 0; i < parameterTypeNames.Count(); i++)
			{
				var parameterType = parameterList[i].Type;

				if (parameterType is PredefinedTypeSyntax predefinedType)
				{
					parameterTypeNames[i] = predefinedType.Keyword.ValueText;
				}
				else if (parameterType is IdentifierNameSyntax customType)
				{
					parameterTypeNames[i] = customType.Identifier.Text;
				}
				else
				{
					throw new NotSupportedException("Parameter type not supported");
				}
			}

			return parameterTypeNames;
		}

		private static IEnumerable<ParameterSyntax> GetParameterListNodes(SyntaxNode memberNode)
		{
			SyntaxNode parameterListNode = memberNode.ChildNodes().First(n => n.Kind() == SyntaxKind.ParameterList);

			return parameterListNode.ChildNodes().Where(n => n.Kind() == SyntaxKind.Parameter).Cast<ParameterSyntax>();
		}

		private async Task<TestGenerationContext> CollectTestGenerationContextAsync(ProjectItemSummary selectedFile, EnvDTE.Project targetProject, TestFramework testFramework, MockFramework mockFramework)
		{
			string targetProjectNamespace = targetProject.Properties.Item("DefaultNamespace").Value as string;
			return await this.CollectTestGenerationContextAsync(selectedFile, targetProjectNamespace, testFramework, mockFramework);
		}

		private string GenerateUnitTestContents(TestGenerationContext context)
		{
			TestFramework testFramework = context.TestFramework;
			MockFramework mockFramework = context.MockFramework;

			string fileTemplate = this.Settings.GetTemplate(testFramework, mockFramework, TemplateType.File);
			string filledTemplate = StringUtilities.ReplaceTokens(
				fileTemplate,
				(tokenName, propertyIndex, builder) =>
				{
					if (WriteGlobalToken(tokenName, builder, context))
					{
						return;
					}

					if (WriteContentToken(tokenName, propertyIndex, builder, context, fileTemplate))
					{
						return;
					}

					WriteTokenPassthrough(tokenName, builder);
				});

			SyntaxTree tree = CSharpSyntaxTree.ParseText(filledTemplate);
			SyntaxNode formattedNode = Formatter.Format(tree.GetRoot(), CreateUnitTestBoilerplateCommandPackage.VisualStudioWorkspace);

			return formattedNode.ToFullString();
		}

		private static string ReplaceInterfaceTokens(string template, InjectableType injectableType, TestGenerationContext context)
		{
			return StringUtilities.ReplaceTokens(
				template,
				(tokenName, propertyIndex, builder) =>
				{
					if (WriteGlobalToken(tokenName, builder, context))
					{
						return;
					}

					if (WriteInterfaceContentToken(injectableType, tokenName, builder))
					{
						return;
					}

					WriteTokenPassthrough(tokenName, builder);
				});
		}

		private string ReplaceGlobalOrContentTokens(string template, TestGenerationContext context)
		{
			return StringUtilities.ReplaceTokens(
				template,
				(tokenName, propertyIndex, builder) =>
				{
					if (WriteGlobalToken(tokenName, builder, context))
					{
						return;
					}

					if (WriteContentTokenNoIndent(tokenName, builder, context))
					{
						return;
					}

					WriteTokenPassthrough(tokenName, builder);
				});
		}

		private static bool WriteGlobalToken(string tokenName, StringBuilder builder, TestGenerationContext context)
		{
			switch (tokenName)
			{
				case "Namespace":
					builder.Append(context.UnitTestNamespace);
					break;

				case "ClassName":
					builder.Append(context.ClassName);
					break;

				case "ClassNameShort":
					builder.Append(GetShortClassName(context.ClassName));
					break;

				case "ClassNameShortLower":
					// Legacy, new syntax is ClassNameShort.CamelCase
					builder.Append(GetShortClassNameLower(context.ClassName));
					break;

				default:
					return false;
			}

			return true;
		}

		private bool WriteContentToken(string tokenName, int propertyIndex, StringBuilder builder, TestGenerationContext context, string fileTemplate)
		{
			switch (tokenName)
			{
				case "UsingStatements":
					WriteUsings(builder, context);
					break;

				case "MockFieldDeclarations":
					this.WriteMockFieldDeclarations(builder, context);
					break;

				case "MockFieldInitializations":
					this.WriteMockFieldInitializations(builder, context);
					break;

				case "ExplicitConstructor":
					WriteExplicitConstructor(builder, context, FindIndent(fileTemplate, propertyIndex));
					break;

				case "TestMethods":
					WriteTestMethods(builder, context);
					break;

				default:
					return false;
			}

			return true;
		}

		private bool WriteContentTokenNoIndent(string tokenName, StringBuilder builder, TestGenerationContext context)
		{
			switch (tokenName)
			{
				case "UsingStatements":
					WriteUsings(builder, context);
					break;

				case "MockFieldDeclarations":
					this.WriteMockFieldDeclarations(builder, context);
					break;

				case "MockFieldInitializations":
					this.WriteMockFieldInitializations(builder, context);
					break;

				case "ExplicitConstructor":
					WriteExplicitConstructor(builder, context, string.Empty);
					break;

				case "TestMethods":
					WriteTestMethods(builder, context);
					break;

				default:
					return false;
			}

			return true;
		}

		private static bool WriteInterfaceContentToken(InjectableType injectableType, string tokenName, StringBuilder builder)
		{
			switch (tokenName)
			{
				case "InterfaceName":
					builder.Append(injectableType.TypeName);
					break;

				case "InterfaceNameBase":
					builder.Append(injectableType.TypeBaseName);
					break;

				case "InterfaceType":
					builder.Append(injectableType.ToString());
					break;

				case "InterfaceMockName":
					builder.Append(injectableType.MockName);
					break;

				default:
					return false;
			}

			return true;
		}

		private static void WriteTokenPassthrough(string tokenName, StringBuilder builder)
		{
			builder.Append($"${tokenName}$");
		}

		private static void WriteUsings(StringBuilder builder, TestGenerationContext context)
		{
			List<string> namespaces = new List<string>();
			namespaces.AddRange(context.MockFramework.UsingNamespaces);
			namespaces.Add(context.TestFramework.UsingNamespace);
			namespaces.Add(context.ClassNamespace);

			if (context.TestFramework.TestCleanupStyle == TestCleanupStyle.Disposable)
			{
				namespaces.Add("System");
			}

			foreach (InjectableType injectedType in context.InjectedTypes)
			{
				namespaces.AddRange(injectedType.TypeNamespaces);
			}

			if (context.MethodDeclarations.Any(m => m.IsAsync))
			{
				namespaces.Add("System.Threading.Tasks");
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

		private void WriteMockFieldDeclarations(StringBuilder builder, TestGenerationContext context)
		{
			string template = this.Settings.GetTemplate(context.TestFramework, context.MockFramework, TemplateType.MockFieldDeclaration);
			WriteFieldLines(builder, context, template);
		}

		private void WriteMockFieldInitializations(StringBuilder builder, TestGenerationContext context)
		{
			string template = this.Settings.GetTemplate(context.TestFramework, context.MockFramework, TemplateType.MockFieldInitialization);
			WriteFieldLines(builder, context, template);
		}

		// Works for both field declarations and initializations.
		private static void WriteFieldLines(StringBuilder builder, TestGenerationContext context, string template)
		{
			for (int i = 0; i < context.InjectedTypes.Count; i++)
			{
				InjectableType injectedType = context.InjectedTypes[i];
				string line = ReplaceInterfaceTokens(template, injectedType, context);

				builder.Append(line);

				if (i < context.InjectedTypes.Count - 1)
				{
					builder.AppendLine();
				}
			}
		}

		private void WriteExplicitConstructor(StringBuilder builder, TestGenerationContext context, string currentIndent)
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
						string template = this.Settings.GetTemplate(context.TestFramework, context.MockFramework, TemplateType.MockObjectReference);
						mockReferenceStatement = ReplaceInterfaceTokens(template, constructorType, context);
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
					string template = this.Settings.GetTemplate(context.TestFramework, context.MockFramework, TemplateType.MockObjectReference);
					string mockReferenceStatement = ReplaceInterfaceTokens(template, property, context);

					builder.AppendLine($"{property.PropertyName} = {mockReferenceStatement},");
				}

				builder.Append(@"}");
			}
		}

		private void WriteTestMethods(StringBuilder builder, TestGenerationContext context)
		{
			if (context.MethodDeclarations.Count == 0)
			{
				return;
			}

			string testedObjectReferenceTemplate = this.Settings.GetTemplate(
				context.TestFramework,
				context.MockFramework,
				TemplateType.TestedObjectReference);
			var testedObjectReference = ReplaceGlobalOrContentTokens(testedObjectReferenceTemplate, context);

			string testedObjectCreationTemplate = this.Settings.GetTemplate(
				context.TestFramework,
				context.MockFramework,
				TemplateType.TestedObjectCreation);
			var testedObjectCreation = ReplaceGlobalOrContentTokens(testedObjectCreationTemplate, context);

			var testMethodPrefixes = new List<string>();

			int idx = 0;

			foreach (var methodDescriptor in context.MethodDeclarations)
			{
				if(idx > 0)
				{
					builder.AppendLine();
				}
				idx++;

				string testMethodPrefix = CreateUniqueTestMethodPrefix(testMethodPrefixes, methodDescriptor);

				string returnType = methodDescriptor.IsAsync ? "Task" : "void";

				string asyncModifier = methodDescriptor.IsAsync ? "async" : string.Empty;

				builder.AppendLine($"[{context.TestFramework.TestMethodAttribute}]");
				builder.AppendLine($"public {asyncModifier} {returnType} {testMethodPrefix}_Condition_Expectation()");
				builder.AppendLine("{");
				builder.AppendLine("// Arrange");
				if (!string.IsNullOrEmpty(context.MockFramework.TestArrangeCode))
				{
					builder.AppendLine(context.MockFramework.TestArrangeCode);
				}
		
				builder.AppendLine(testedObjectCreation);
				builder.AppendLine(); // Separator

				builder.AppendLine("// Act");
				if (methodDescriptor.IsAsync)
				{
					builder.Append("await ");
				}

				builder.Append($"{testedObjectReference}.{methodDescriptor.Name}(");
				var numberOfParameters = methodDescriptor.MethodParameterNames.Count();
				if (numberOfParameters == 0)
				{
					builder.AppendLine(")");
				}
				else
				{
					builder.AppendLine();

					for (int i = 0; i < numberOfParameters; i++)
					{
						builder.Append($"	default({methodDescriptor.MethodParameterNames[i]})");

						if (i < numberOfParameters - 1)
						{
							builder.AppendLine(",");
						}
						else
						{
							builder.AppendLine(");");
						}
					}
				}
				builder.AppendLine(); // Separator

				builder.AppendLine("// Assert");
				builder.AppendLine("Assert.Fail();");
				builder.AppendLine("}");

				testMethodPrefixes.Add(testMethodPrefix);
			}
		}

		private static string CreateUniqueTestMethodPrefix(List<string> testMethodPrefixes, MethodDescriptor methodDescriptor)
		{
			string testMethodPrefix = methodDescriptor.Name;

			int j = 1;

			while (testMethodPrefixes.Contains(testMethodPrefix))
			{
				testMethodPrefix = methodDescriptor.Name + j;
				j++;
			}

			return testMethodPrefix;
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
	}
}
