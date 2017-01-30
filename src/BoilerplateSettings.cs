using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;
using Newtonsoft.Json;
using System.ComponentModel.Composition;

namespace UnitTestBoilerplate
{
	// TODO: Migrate from StaticBoilerplateSettings to this
	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export(typeof(IBoilerplateSettings))]
	public class BoilerplateSettings : IBoilerplateSettings
	{
		private const string CollectionPath = "UnitTestBoilerplateGenerator";

		private const string TestProjectsKey = "TestProjectsDictionary";

		private readonly WritableSettingsStore Store;

		// Key is the solution path and value is the last used unit test project for the solution.
		private readonly Dictionary<string, string> TestProjectsDictionary;

		//[ImportingConstructor]
		//public Encouragements(SVsServiceProvider vsServiceProvider)
		//{
		//	var shellSettingsManager = new ShellSettingsManager(vsServiceProvider);
		//	writableSettingsStore = shellSettingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);

		//	LoadSettings();
		//}

		[ImportingConstructor]
		public BoilerplateSettings(SVsServiceProvider vsServiceProvider)
		{
			SettingsManager settingsManager = new ShellSettingsManager(vsServiceProvider);
			Store = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);
			Store.CreateCollection(CollectionPath);

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

		public void SaveSelectedTestProject(string solutionPath, string testProjectPath)
		{
			TestProjectsDictionary[solutionPath] = testProjectPath;
			Store.SetString(CollectionPath, TestProjectsKey, JsonConvert.SerializeObject(TestProjectsDictionary));
		}

		public string GetLastSelectedProject(string solutionPath)
		{
			string result;
			if (TestProjectsDictionary.TryGetValue(solutionPath, out result))
			{
				return result;
			}

			return null;
		}
	}
}
