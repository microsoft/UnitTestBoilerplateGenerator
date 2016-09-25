using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;
using Newtonsoft.Json;

namespace UnitTestBoilerplate
{
	public static class Settings
	{
		private const string CollectionPath = "UnitTestBoilerplateGenerator";

		private const string TestProjectsKey = "TestProjectsDictionary";

		private static readonly WritableSettingsStore Store;

		// Key is the solution path and value is the last used unit test project for the solution.
		private static readonly Dictionary<string, string> TestProjectsDictionary;

		static Settings()
		{
			SettingsManager settingsManager = new ShellSettingsManager(ServiceProvider.GlobalProvider);
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
	}
}
