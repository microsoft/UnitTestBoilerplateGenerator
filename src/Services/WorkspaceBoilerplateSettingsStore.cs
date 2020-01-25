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
			string fileNameTemplate)
		{
			this.settingsFilePath = settingsFilePath;

			this.jsonObject = new BoilerplateSettingsJson
			{
				Version = 1,
				PreferredTestFrameworkName = preferredTestFrameworkName,
				PreferredMockFrameworkName = preferredMockFrameworkName,
				FileNameTemplate = fileNameTemplate,
				Templates = templates
			};
		}

		public string PreferredTestFrameworkName
		{
			get
			{
				return this.jsonObject.PreferredTestFrameworkName;
			}

			set
			{
				this.jsonObject.PreferredTestFrameworkName = value;
			}
		}

		public string PreferredMockFrameworkName
		{
			get
			{
				return this.jsonObject.PreferredMockFrameworkName;
			}

			set
			{
				this.jsonObject.PreferredMockFrameworkName = value;
			}
		}

		public string FileNameTemplate
		{
			get
			{
				return this.jsonObject.FileNameTemplate;
			}

			set
			{
				this.jsonObject.FileNameTemplate = value;
			}
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
