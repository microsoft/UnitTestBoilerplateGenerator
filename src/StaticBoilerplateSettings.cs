using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnitTestBoilerplate.Model;
using UnitTestBoilerplate.Utilities;

namespace UnitTestBoilerplate
{
	public static class StaticBoilerplateSettings
	{
		private const string CollectionPath = "UnitTestBoilerplateGenerator";

		private const string TestProjectsKey = "TestProjectsDictionary";

		private const string FileNameTemplateKey = "FileNameTemplate";

		private const int LatestVersion = 1;

		private const string VersionKey = "Version";

		private static readonly WritableSettingsStore Store;

		// Key is the solution path and value is the last used unit test project for the solution.
		private static readonly Dictionary<string, string> TestProjectsDictionary;

		static StaticBoilerplateSettings()
		{
			SettingsManager settingsManager = new ShellSettingsManager(ServiceProvider.GlobalProvider);
			Store = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);
			Store.CreateCollection(CollectionPath);

			// Setting upgrade if needed
			if (Store.PropertyExists(CollectionPath, VersionKey))
			{
				int storedVersion = Store.GetInt32(CollectionPath, VersionKey);
				if (storedVersion < LatestVersion)
				{
					SetVersionToLatest();
				}
			}
			else
			{
				if (Store.PropertyExists(CollectionPath, TestProjectsKey))
				{
					// We are upgrading from an old version (v0), as we didn't have version tracked, but had a test projects dictionary

					var mockFrameworks = new List<string> { "Moq", "AutoMoq", "SimpleStubs", "NSubstitute" };
					var templateTypes = new List<TemplateType> {TemplateType.File, TemplateType.MockFieldDeclaration, TemplateType.MockFieldInitialization, TemplateType.MockObjectReference};
					foreach (string mockFrameworkName in mockFrameworks)
					{
						MockFramework mockFramework = MockFrameworks.Get(mockFrameworkName);

						foreach (TemplateType templateType in templateTypes)
						{
							string templateKey = $"Template_{mockFrameworkName}_{templateType}";

							if (Store.PropertyExists(CollectionPath, templateKey))
							{
								string oldTemplate = Store.GetString(CollectionPath, templateKey);

								CreateEntryForTestFramework(oldTemplate, templateType, TestFrameworks.Get("VisualStudio"), mockFramework);
								CreateEntryForTestFramework(oldTemplate, templateType, TestFrameworks.Get("NUnit"), mockFramework);

								Store.DeleteProperty(CollectionPath, templateKey);
							}
						}
					}
				}

				SetVersionToLatest();
			}

			if (Store.PropertyExists(CollectionPath, TestProjectsKey))
			{
				string dictionaryString = Store.GetString(CollectionPath, TestProjectsKey);

				if (string.IsNullOrEmpty(dictionaryString))
				{
					TestProjectsDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
				}
				else
				{
					TestProjectsDictionary = new Dictionary<string, string>(JsonConvert.DeserializeObject<Dictionary<string, string>>(dictionaryString), StringComparer.OrdinalIgnoreCase);
				}
			}
			else
			{
				TestProjectsDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			}
		}

		private static void CreateEntryForTestFramework(string oldTemplate, TemplateType templateType, TestFramework testFramework, MockFramework mockFramework)
		{
			string newTemplate;

			// If it's a File template, we need to replace some framework-based placeholders for test attributes.
			if (templateType == TemplateType.File)
			{
				newTemplate = StringUtilities.ReplaceTokens(
					oldTemplate,
					(tokenName, propertyIndex, builder) =>
					{
						switch (tokenName)
						{
							case "TestClassAttribute":
								builder.Append(testFramework.TestClassAttribute);
								break;
							case "TestInitializeAttribute":
								builder.Append(testFramework.TestInitializeAttribute);
								break;
							case "TestCleanupAttribute":
								builder.Append(testFramework.TestCleanupAttribute);
								break;
							case "TestMethodAttribute":
								builder.Append(testFramework.TestMethodAttribute);
								break;
							default:
								// Pass through all other tokens.
								builder.Append($"${tokenName}$");
								break;
						}
					});
			}
			else
			{
				newTemplate = oldTemplate;
			}

			Store.SetString(CollectionPath, GetTemplateSettingsKey(testFramework, mockFramework, templateType), newTemplate);
		}

		private static void SetVersionToLatest()
		{
			Store.SetInt32(CollectionPath, VersionKey, LatestVersion);
		}

		public static void SaveSelectedTestProject(string solutionPath, string testProjectPath)
		{
			TestProjectsDictionary[solutionPath] = testProjectPath;
			Store.SetString(CollectionPath, TestProjectsKey, JsonConvert.SerializeObject(TestProjectsDictionary));
		}

		public static string GetLastSelectedProject(string solutionPath)
		{
			string result;
			if (TestProjectsDictionary.TryGetValue(solutionPath, out result))
			{
				return result;
			}

			return null;
		}

		public static string FileNameTemplate
		{
			get
			{
				if (Store.PropertyExists(CollectionPath, FileNameTemplateKey))
				{
					string fileNameTemplate = Store.GetString(CollectionPath, FileNameTemplateKey);
					if (!string.IsNullOrWhiteSpace(fileNameTemplate))
					{
						return fileNameTemplate;
					}
				}

				return "$ClassName$Tests";
			}

			set
			{
				Store.SetString(CollectionPath, FileNameTemplateKey, value);
			}
		}

		public static string GetTemplate(TestFramework testFramework, MockFramework mockFramework, TemplateType templateType)
		{
			string templateSettingKey = GetTemplateSettingsKey(testFramework, mockFramework, templateType);

			if (Store.PropertyExists(CollectionPath, templateSettingKey))
			{
				return Store.GetString(CollectionPath, templateSettingKey);
			}

			switch (templateType)
			{
				case TemplateType.File:
					var templateGenerator = new DefaultTemplateGenerator();
					return templateGenerator.Get(testFramework, mockFramework);
				case TemplateType.MockFieldDeclaration:
					return mockFramework.MockFieldDeclarationCode;
				case TemplateType.MockFieldInitialization:
					return mockFramework.MockFieldInitializationCode;
				case TemplateType.MockObjectReference:
					return mockFramework.MockObjectReferenceCode;
				default:
					throw new ArgumentOutOfRangeException(nameof(templateType), templateType, null);
			}
		}

		public static void SetTemplate(TestFramework testFramework, MockFramework mockFramework, TemplateType templateType, string template)
		{
			Store.SetString(CollectionPath, GetTemplateSettingsKey(testFramework, mockFramework, templateType), template);
		}

		public static void SetTemplate(string testFrameworkString, string mockFrameworkString, string templateTypeString, string template)
		{
			Store.SetString(CollectionPath, GetTemplateSettingsKey(testFrameworkString, mockFrameworkString, templateTypeString), template);
		}

		public static void RevertTemplateToDefault(TestFramework testFramework, MockFramework mockFramework)
		{
			foreach (TemplateType templateType in Enum.GetValues(typeof(TemplateType)))
			{
				string templateSettingKey = GetTemplateSettingsKey(testFramework, mockFramework, templateType);
				if (Store.PropertyExists(CollectionPath, templateSettingKey))
				{
					Store.DeleteProperty(CollectionPath, templateSettingKey);
				}
			}
		}

		private static string GetTemplateSettingsKey(TestFramework testFramework, MockFramework mockFramework, TemplateType templateType)
		{
			return GetTemplateSettingsKey(testFramework.Name, mockFramework.Name, templateType.ToString());
		}

		private static string GetTemplateSettingsKey(string testFrameworkString, string mockFrameworkString, string templateTypeString)
		{
			return $"Template_{testFrameworkString}_{mockFrameworkString}_{templateTypeString}";
		}
	}
}
