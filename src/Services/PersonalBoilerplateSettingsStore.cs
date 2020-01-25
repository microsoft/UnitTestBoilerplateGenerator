using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestBoilerplate.Model;

namespace UnitTestBoilerplate.Services
{
	/// <summary>
	/// User-specific storage for UTBG settings. (Visual Studio settings manager)
	/// </summary>
	public class PersonalBoilerplateSettingsStore : IBoilerplateSettingsStore
	{
		public const string CollectionPath = "UnitTestBoilerplateGenerator";

		private const int LatestVersion = 1;

		private const string VersionKey = "Version";

		private const string PreferredTestFrameworkKey = "PreferredTestFramework";

		private const string PreferredMockFrameworkKey = "PreferredMockFramework";

		private const string FileNameTemplateKey = "FileNameTemplate";

		private readonly WritableSettingsStore store;

		public PersonalBoilerplateSettingsStore()
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
				this.SetVersionToLatest();
			}

			this.store = store;
		}

		private void SetVersionToLatest()
		{
			this.store.SetInt32(CollectionPath, VersionKey, LatestVersion);
		}

		public string PreferredTestFrameworkName
		{
			get
			{
				if (this.store.PropertyExists(CollectionPath, PreferredTestFrameworkKey))
				{
					return this.store.GetString(CollectionPath, PreferredTestFrameworkKey);
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
					this.store.SetString(CollectionPath, PreferredTestFrameworkKey, value);
				}
			}
		}

		public string PreferredMockFrameworkName
		{
			get
			{
				if (this.store.PropertyExists(CollectionPath, PreferredMockFrameworkKey))
				{
					return this.store.GetString(CollectionPath, PreferredMockFrameworkKey);
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
					this.store.SetString(CollectionPath, PreferredMockFrameworkKey, value);
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

				return null;
			}
			set
			{ 
			    this.store.SetString(CollectionPath, FileNameTemplateKey, value);
			}
		}

		/// <summary>
		/// Gets the template under the template settings key
		/// </summary>
		/// <param name="templateSettingsKey">The template settings key (not prefixed).</param>
		/// <returns>The template.</returns>
		public string GetTemplate(string templateSettingsKey)
		{
			templateSettingsKey = "Template_" + templateSettingsKey;

			if (this.store.PropertyExists(CollectionPath, templateSettingsKey))
			{
				return this.store.GetString(CollectionPath, templateSettingsKey);
			}

			return null;
		}

		/// <summary>
		/// Sets the template under the given template settings key.
		/// </summary>
		/// <param name="templateSettingsKey">The template settings key (not prefixed).</param>
		/// <param name="template">The template value to store.</param>
		public void SetTemplate(string templateSettingsKey, string template)
		{
			templateSettingsKey = "Template_" + templateSettingsKey;

			if (template == null)
			{
				this.store.DeleteProperty(CollectionPath, templateSettingsKey);
			}
			else
			{
				this.store.SetString(CollectionPath, templateSettingsKey, template);
			}
		}

		public void RevertTemplateToDefault(TestFramework testFramework, MockFramework mockFramework)
		{
			foreach (TemplateType templateType in Enum.GetValues(typeof(TemplateType)))
			{
				string templateSettingKey = GetPersonalTemplateSettingsKey(testFramework, mockFramework, templateType);

				if (this.store.PropertyExists(CollectionPath, templateSettingKey))
				{
					this.store.DeleteProperty(CollectionPath, templateSettingKey);
				}
			}
		}

		/// <summary>
		/// Gets all templates with simplified keys: testFramework_mockFramework_templateType
		/// </summary>
		/// <returns></returns>
		public IDictionary<string, string> GetAllTemplates()
		{
			var result = new Dictionary<string, string>();
			foreach (TestFramework testFramework in TestFrameworks.List)
			{
				foreach (MockFramework mockFramework in MockFrameworks.List)
				{
					foreach (TemplateType templateType in Enum.GetValues(typeof(TemplateType)))
					{
						string personalTemplateSettingKey = GetPersonalTemplateSettingsKey(testFramework, mockFramework, templateType);
						if (this.store.PropertyExists(CollectionPath, personalTemplateSettingKey))
						{
							string template = this.store.GetString(CollectionPath, personalTemplateSettingKey);
							if (template != BoilerplateSettings.GetDefaultTemplate(testFramework, mockFramework, templateType))
							{
								string normalTemplateSettingKey = BoilerplateSettings.GetTemplateSettingsKey(testFramework, mockFramework, templateType);
								result.Add(normalTemplateSettingKey, this.store.GetString(CollectionPath, personalTemplateSettingKey));
							}
						}
					}
				}
			}

			return result;
		}

		public void Apply()
		{
			// Do nothing. We apply immediately on calls to Set*.
		}

		private static string GetPersonalTemplateSettingsKey(TestFramework testFramework, MockFramework mockFramework, TemplateType templateType)
		{
			return "Template_" + BoilerplateSettings.GetTemplateSettingsKey(testFramework, mockFramework, templateType);
		}
	}
}
