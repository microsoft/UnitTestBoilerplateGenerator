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
		public const string UserBoilerPlateSettings = "UserBoilerPlateSettings";
		public static string UserCreatedSettingsPath;
		public static bool LoadUserCreatedSettings;

		private readonly DTE2 dte;

		private readonly PersonalBoilerplateSettingsStore personalStore = new PersonalBoilerplateSettingsStore();
		private readonly BoilerplateSettings personalSettings;

		private string workspaceStoreSolutionPath;
		private WorkspaceBoilerplateSettingsStore workspaceStore;
		private BoilerplateSettings workspaceSettings;

		public BoilerplateSettingsFactory()
		{
			this.dte = (DTE2)ServiceProvider.GlobalProvider.GetService(typeof(DTE));
			this.personalSettings = new BoilerplateSettings(this.personalStore);

			SettingsManager settingsManager = new ShellSettingsManager(ServiceProvider.GlobalProvider);
			WritableSettingsStore store = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);
			store.CreateCollection(PersonalBoilerplateSettingsStore.CollectionPath);
			if (store.PropertyExists(PersonalBoilerplateSettingsStore.CollectionPath, UserBoilerPlateSettings))
			{
				UserCreatedSettingsPath = store.GetString(PersonalBoilerplateSettingsStore.CollectionPath, UserBoilerPlateSettings);

				if (string.IsNullOrEmpty(UserCreatedSettingsPath) == false)
				{
					LoadUserCreatedSettings = true;
				}
				else
				{
					LoadUserCreatedSettings = false;
				}
			}
		}

		public IBoilerplateSettings Get()
		{
			this.UpdateWorkspaceStore();
			if (this.workspaceSettings != null)
			{
				return this.workspaceSettings;
			}

			return this.personalSettings;
		}

		public bool UsingWorkspaceSettings
		{
			get
			{
				this.UpdateWorkspaceStore();
				return this.workspaceStore != null;
			}
		}

		public void ReloadUserSettings()
		{
			ClearWorkspaceStore();
			UpdateWorkspaceStore();
		}

		private void UpdateWorkspaceStore()
		{
			var solution = this.dte.Solution;
			if (solution == null)
			{
				this.ClearWorkspaceStore();
				return;
			}

			string solutionSettingsFileName = LoadUserCreatedSettings ? UserCreatedSettingsPath : $"{solution.FullName}{WorkspaceSettingsFileSuffix}";
			if (!File.Exists(solutionSettingsFileName))
			{
				this.ClearWorkspaceStore();
				return;
			}

			if (this.workspaceStore != null && solution.FullName == this.workspaceStoreSolutionPath)
			{
				// We are current. Return.
				return;
			}

			try
			{
				// Initialize the new store from that settings file.
				this.workspaceStore = new WorkspaceBoilerplateSettingsStore(solutionSettingsFileName);
				this.workspaceSettings = new BoilerplateSettings(this.workspaceStore);
				this.workspaceStoreSolutionPath = solution.FullName;
			}
			catch
			{
				this.ClearWorkspaceStore();
			}
		}

		private void ClearWorkspaceStore()
		{
			this.workspaceStoreSolutionPath = null;
			this.workspaceStore = null;
			this.workspaceSettings = null;
		}
	}
}
