using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestBoilerplate.Model;

namespace UnitTestBoilerplate.Services
{
	/// <summary>
	/// Workspace storage for UTBG settings. (JSON file)
	/// </summary>
	public class WorkspaceBoilerplateSettingsStore : IBoilerplateSettingsStore
	{
		private BoilerplateSettingsJson jsonObject;
		private readonly string settingsFilePath;

		private static readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented };

		public WorkspaceBoilerplateSettingsStore(string settingsFilePath)
		{
			this.settingsFilePath = settingsFilePath;

			this.jsonObject = JsonConvert.DeserializeObject<BoilerplateSettingsJson>(File.ReadAllText(settingsFilePath), jsonSettings);
		}

		public WorkspaceBoilerplateSettingsStore(
			string settingsFilePath,
			IDictionary<string, string> templates,
			string preferredTestFrameworkName,
			string preferredMockFrameworkName,
			string fileNameTemplate,
			IDictionary<string, string> customMocks,
			string customMockFieldDeclarationTemplate,
			string customMockFieldInitializationTemplate,
			string customMockObjectReferenceTemplate)
		{
			this.settingsFilePath = settingsFilePath;

			this.jsonObject = new BoilerplateSettingsJson
			{
				Version = 1,
				PreferredTestFrameworkName = preferredTestFrameworkName,
				PreferredMockFrameworkName = preferredMockFrameworkName,
				FileNameTemplate = fileNameTemplate,
				CustomMockFieldDeclarationTemplate = customMockFieldDeclarationTemplate,
				CustomMockFieldInitializationTemplate = customMockFieldInitializationTemplate,
				CustomMockObjectReferenceTemplate = customMockObjectReferenceTemplate,
				Templates = templates
			};

			if (customMocks != null && customMocks.Count > 0)
			{
				this.jsonObject.CustomMocks = customMocks;
			}
		}

		public string PreferredTestFrameworkName
		{
			get => this.jsonObject.PreferredTestFrameworkName;
			set => this.jsonObject.PreferredTestFrameworkName = value;
		}

		public string PreferredMockFrameworkName
		{
			get => this.jsonObject.PreferredMockFrameworkName;
			set => this.jsonObject.PreferredMockFrameworkName = value;
		}

		public string FileNameTemplate
		{
			get => this.jsonObject.FileNameTemplate;
			set => this.jsonObject.FileNameTemplate = value;
		}

		public IDictionary<string, string> CustomMocks
		{
			get => this.jsonObject.CustomMocks;
			set => this.jsonObject.CustomMocks = value;
		}

		public string CustomMockFieldDeclarationTemplate
		{
			get => this.jsonObject.CustomMockFieldDeclarationTemplate;
			set => this.jsonObject.CustomMockFieldDeclarationTemplate = value;
		}

		public string CustomMockFieldInitializationTemplate
		{
			get => this.jsonObject.CustomMockFieldInitializationTemplate;
			set => this.jsonObject.CustomMockFieldInitializationTemplate = value;
		}

		public string CustomMockObjectReferenceTemplate
		{
			get => this.jsonObject.CustomMockObjectReferenceTemplate;
			set => this.jsonObject.CustomMockObjectReferenceTemplate = value;
		}

		public string GetTemplate(string templateSettingsKey)
		{
			if (this.jsonObject.Templates.TryGetValue(templateSettingsKey, out string template))
			{
				return template;
			}

			return null;
		}

		public void SetTemplate(string templateSettingsKey, string template)
		{
			this.jsonObject.Templates[templateSettingsKey] = template;
		}

		public void RevertTemplateToDefault(TestFramework testFramework, MockFramework mockFramework)
		{
			foreach (TemplateType templateType in Enum.GetValues(typeof(TemplateType)))
			{
				string templateSettingKey = BoilerplateSettings.GetTemplateSettingsKey(testFramework, mockFramework, templateType);
				this.jsonObject.Templates.Remove(templateSettingKey);
			}
		}

		public void Apply()
		{
			File.WriteAllText(this.settingsFilePath, JsonConvert.SerializeObject(this.jsonObject, jsonSettings));
		}
	}
}
