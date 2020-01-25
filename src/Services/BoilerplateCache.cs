using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate.Services
{
	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export(typeof(IBoilerplateCache))]
	public class BoilerplateCache : IBoilerplateCache
	{
		private const string TestProjectsKey = "TestProjectsDictionary";

		// Key is the solution path and value is the last used unit test project for the solution.
		private readonly Dictionary<string, string> testProjectsDictionary;

		private readonly WritableSettingsStore store;

		public BoilerplateCache()
		{
			SettingsManager settingsManager = new ShellSettingsManager(ServiceProvider.GlobalProvider);
			this.store = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);
			this.store.CreateCollection(PersonalBoilerplateSettingsStore.CollectionPath);

			if (this.store.PropertyExists(PersonalBoilerplateSettingsStore.CollectionPath, TestProjectsKey))
			{
				string dictionaryString = this.store.GetString(PersonalBoilerplateSettingsStore.CollectionPath, TestProjectsKey);

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

		public void SaveSelectedTestProject(string solutionPath, string testProjectPath)
		{
			this.testProjectsDictionary[solutionPath] = testProjectPath;
			this.store.SetString(PersonalBoilerplateSettingsStore.CollectionPath, TestProjectsKey, JsonConvert.SerializeObject(this.testProjectsDictionary));
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
	}
}
