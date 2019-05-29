using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;
using Newtonsoft.Json;
using UnitTestBoilerplate.Model;
using UnitTestBoilerplate.Utilities;

namespace UnitTestBoilerplate.Services
{
	[Export(typeof(IBoilerplateSettings))]
    public class BoilerplateSettings : IBoilerplateSettings
    {
		private const string CollectionPath = "UnitTestBoilerplateGenerator";

	    private const string TestProjectsKey = "TestProjectsDictionary";

	    private const string FileNameTemplateKey = "FileNameTemplate";

	    private const string PreferredTestFrameworkKey = "PreferredTestFramework";

	    private const string PreferredMockFrameworkKey = "PreferredMockFramework";

	    private const int LatestVersion = 1;

	    private const string VersionKey = "Version";

	    private readonly WritableSettingsStore store;

	    // Key is the solution path and value is the last used unit test project for the solution.
	    private readonly Dictionary<string, string> testProjectsDictionary;

	    public BoilerplateSettings()
	    {
		    SettingsManager settingsManager = new ShellSettingsManager(ServiceProvider.GlobalProvider);
		    this.store = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);
		    this.store.CreateCollection(CollectionPath);

		    // Setting upgrade if needed
		    if (this.store.PropertyExists(CollectionPath, VersionKey))
		    {
			    int storedVersion = this.store.GetInt32(CollectionPath, VersionKey);
			    if (storedVersion < LatestVersion)
			    {
				    this.SetVersionToLatest();
			    }
		    }
		    else
		    {
			    if (this.store.PropertyExists(CollectionPath, TestProjectsKey))
			    {
				    // We are upgrading from an old version (v0), as we didn't have version tracked, but had a test projects dictionary

				    var mockFrameworks = new List<string> { "Moq", "AutoMoq", "SimpleStubs", "NSubstitute" };
				    var templateTypes = new List<TemplateType> { TemplateType.File, TemplateType.MockFieldDeclaration, TemplateType.MockFieldInitialization, TemplateType.MockObjectReference };
				    foreach (string mockFrameworkName in mockFrameworks)
				    {
					    MockFramework mockFramework = MockFrameworks.Get(mockFrameworkName);

					    foreach (TemplateType templateType in templateTypes)
					    {
						    string templateKey = $"Template_{mockFrameworkName}_{templateType}";

						    if (this.store.PropertyExists(CollectionPath, templateKey))
						    {
							    string oldTemplate = this.store.GetString(CollectionPath, templateKey);

							    this.CreateEntryForTestFramework(oldTemplate, templateType, TestFrameworks.Get("VisualStudio"), mockFramework);
							    this.CreateEntryForTestFramework(oldTemplate, templateType, TestFrameworks.Get("NUnit"), mockFramework);

							    this.store.DeleteProperty(CollectionPath, templateKey);
						    }
					    }
				    }
			    }

			    this.SetVersionToLatest();
		    }

		    if (this.store.PropertyExists(CollectionPath, TestProjectsKey))
		    {
			    string dictionaryString = this.store.GetString(CollectionPath, TestProjectsKey);

			    if (string.IsNullOrEmpty(dictionaryString))
			    {
				    this.testProjectsDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			    }
			    else
			    {
				    this.testProjectsDictionary = new Dictionary<string, string>(JsonConvert.DeserializeObject<Dictionary<string, string>>(dictionaryString), StringComparer.OrdinalIgnoreCase);
			    }
		    }
		    else
		    {
			    this.testProjectsDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		    }
	    }

	    private void CreateEntryForTestFramework(string oldTemplate, TemplateType templateType, TestFramework testFramework, MockFramework mockFramework)
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

		    this.store.SetString(CollectionPath, GetTemplateSettingsKey(testFramework, mockFramework, templateType), newTemplate);
	    }

	    private void SetVersionToLatest()
	    {
		    this.store.SetInt32(CollectionPath, VersionKey, LatestVersion);
	    }

	    public void SaveSelectedTestProject(string solutionPath, string testProjectPath)
	    {
		    this.testProjectsDictionary[solutionPath] = testProjectPath;
		    this.store.SetString(CollectionPath, TestProjectsKey, JsonConvert.SerializeObject(this.testProjectsDictionary));
	    }

	    public string GetLastSelectedProject(string solutionPath)
	    {
		    string result;
		    if (this.testProjectsDictionary.TryGetValue(solutionPath, out result))
		    {
			    return result;
		    }

		    return null;
	    }

	    public TestFramework PreferredTestFramework
	    {
		    get
		    {
			    if (this.store.PropertyExists(CollectionPath, PreferredTestFrameworkKey))
			    {
				    string preferredTestFrameworkName = this.store.GetString(CollectionPath, PreferredTestFrameworkKey);
				    return TestFrameworks.Get(preferredTestFrameworkName);
			    }
			    else
			    {
				    return null;
			    }
		    }

		    set
		    {
			    if (value == null)
			    {
				    this.store.DeleteProperty(CollectionPath, PreferredTestFrameworkKey);
			    }
			    else
			    {
				    this.store.SetString(CollectionPath, PreferredTestFrameworkKey, value.Name);
			    }
		    }
	    }

	    public MockFramework PreferredMockFramework
	    {
		    get
		    {
			    if (this.store.PropertyExists(CollectionPath, PreferredMockFrameworkKey))
			    {
				    string preferredMockFrameworkName = this.store.GetString(CollectionPath, PreferredMockFrameworkKey);
				    return MockFrameworks.Get(preferredMockFrameworkName);
			    }
			    else
			    {
				    return null;
			    }
		    }

		    set
		    {
			    if (value == null)
			    {
				    this.store.DeleteProperty(CollectionPath, PreferredMockFrameworkKey);
			    }
			    else
			    {
				    this.store.SetString(CollectionPath, PreferredMockFrameworkKey, value.Name);
			    }
		    }
	    }

	    public string FileNameTemplate
	    {
		    get
		    {
			    if (this.store.PropertyExists(CollectionPath, FileNameTemplateKey))
			    {
				    string fileNameTemplate = this.store.GetString(CollectionPath, FileNameTemplateKey);
				    if (!string.IsNullOrWhiteSpace(fileNameTemplate))
				    {
					    return fileNameTemplate;
				    }
			    }

			    return "$ClassName$Tests";
		    }

		    set
		    {
			    this.store.SetString(CollectionPath, FileNameTemplateKey, value);
		    }
	    }

	    public string GetTemplate(TestFramework testFramework, MockFramework mockFramework, TemplateType templateType)
	    {
		    string templateSettingKey = GetTemplateSettingsKey(testFramework, mockFramework, templateType);

		    if (this.store.PropertyExists(CollectionPath, templateSettingKey))
		    {
			    return this.store.GetString(CollectionPath, templateSettingKey);
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
				case TemplateType.TestedObjectReference:
					return DefaultTemplateGenerator.TestObjectReference;
				case TemplateType.TestedObjectCreation:
					return DefaultTemplateGenerator.GetTestObjectCreation(mockFramework);
				case TemplateType.TestMethodName:
					return DefaultTemplateGenerator.TestMethodName;
			    default:
				    throw new ArgumentOutOfRangeException(nameof(templateType), templateType, null);
		    }
	    }

	    public void SetTemplate(TestFramework testFramework, MockFramework mockFramework, TemplateType templateType, string template)
	    {
		    this.store.SetString(CollectionPath, GetTemplateSettingsKey(testFramework, mockFramework, templateType), template);
	    }

	    public void SetTemplate(string testFrameworkString, string mockFrameworkString, string templateTypeString, string template)
	    {
			string settingsKey = GetTemplateSettingsKey(testFrameworkString, mockFrameworkString, templateTypeString);

			if (template == null)
			{
				this.store.DeleteProperty(CollectionPath, settingsKey);
			}
			else
			{
				this.store.SetString(CollectionPath, settingsKey, template);
			}
		}

	    public void RevertTemplateToDefault(TestFramework testFramework, MockFramework mockFramework)
	    {
		    foreach (TemplateType templateType in Enum.GetValues(typeof(TemplateType)))
		    {
			    string templateSettingKey = GetTemplateSettingsKey(testFramework, mockFramework, templateType);
			    if (this.store.PropertyExists(CollectionPath, templateSettingKey))
			    {
				    this.store.DeleteProperty(CollectionPath, templateSettingKey);
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
