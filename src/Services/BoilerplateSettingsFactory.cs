using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate.Services
{
	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export(typeof(IBoilerplateSettingsFactory))]
	public class BoilerplateSettingsFactory : IBoilerplateSettingsFactory
	{
		public const string WorkspaceSettingsFileSuffix = ".utbg.json";
		public const string UserBoilerplateSettings = "UserBoilerplateSettings";
		
		private readonly DTE2 dte;
		private readonly PersonalBoilerplateSettingsStore personalStore = new PersonalBoilerplateSettingsStore();
		private readonly BoilerplateSettings personalSettings;

		private string settingsFilePath;
		private WorkspaceBoilerplateSettingsStore settingsFileStore;
		private BoilerplateSettings fileSettings;

		public BoilerplateSettingsFactory()
		{
			this.dte = (DTE2)ServiceProvider.GlobalProvider.GetService(typeof(DTE));
			this.personalSettings = new BoilerplateSettings(this.personalStore);

			SettingsManager settingsManager = new ShellSettingsManager(ServiceProvider.GlobalProvider);
			WritableSettingsStore store = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);
			store.CreateCollection(PersonalBoilerplateSettingsStore.CollectionPath);
			if (store.PropertyExists(PersonalBoilerplateSettingsStore.CollectionPath, UserBoilerplateSettings))
			{
				UserCreatedSettingsPath = store.GetString(PersonalBoilerplateSettingsStore.CollectionPath, UserBoilerplateSettings);
			}
		}

		public string UserCreatedSettingsPath { get; set; }
		public bool LoadUserCreatedSettings => !string.IsNullOrEmpty(this.UserCreatedSettingsPath);

		public IBoilerplateSettings Get()
		{
			this.UpdateSettingsFileStore();
			if (this.fileSettings != null)
			{
				return this.fileSettings;
			}

			return this.personalSettings;
		}

		public bool UsingWorkspaceSettings
		{
			get
			{
				this.UpdateSettingsFileStore();
				return this.settingsFileStore != null;
			}
		}

		public void ClearSettingsFileStore()
		{
			this.settingsFilePath = null;
			this.settingsFileStore = null;
			this.fileSettings = null;
		}

		private void UpdateSettingsFileStore()
		{
			var solution = this.dte.Solution;
			if (solution == null)
			{
				this.ClearSettingsFileStore();
				return;
			}

			string solutionSettingsFileName;
			if (this.LoadUserCreatedSettings && File.Exists(this.UserCreatedSettingsPath))
			{
				solutionSettingsFileName = UserCreatedSettingsPath;
			}
			else if(File.Exists(solution.FullName + WorkspaceSettingsFileSuffix))
			{
				solutionSettingsFileName = solution.FullName + WorkspaceSettingsFileSuffix;
			}
			else
			{
				this.ClearSettingsFileStore();
				return;
			}

			if (this.settingsFileStore != null && solution.FullName == this.settingsFilePath)
			{
				// We are current. Return.
				return;
			}

			try
			{
				// Initialize the new store from that settings file.
				this.settingsFileStore = new WorkspaceBoilerplateSettingsStore(solutionSettingsFileName);
				this.fileSettings = new BoilerplateSettings(this.settingsFileStore);
				this.settingsFilePath = solution.FullName;
			}
			catch
			{
				this.ClearSettingsFileStore();
			}
		}
	}
}
