using EnvDTE;
using EnvDTE80;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
using UnitTestBoilerplate.Services;

namespace UnitTestBoilerplate.ViewModel
{
	public class WorkspaceSettingsDialogViewModel : ViewModelBase
	{
		private bool hasWorkspaceSettings;

		private readonly DTE2 dte = (DTE2)ServiceProvider.GlobalProvider.GetService(typeof(DTE));

		public WorkspaceSettingsDialogViewModel()
		{
		}

		[Import]
		internal IBoilerplateSettingsFactory SettingsFactory { get; set; }

		[Import]
		internal ISettingsCoordinator SettingsCoordinator { get; set; }

		public void Initialize()
		{
		}

		public void Refresh()
		{
			this.hasWorkspaceSettings = this.SettingsFactory.UsingWorkspaceSettings;
			this.RaisePropertyChanged(nameof(this.StatusText));
			this.RaisePropertyChanged(nameof(this.ShowCopyButton));
		}

		public string StatusText
		{
			get
			{
				if (SettingsFactory.LoadUserCreatedSettings)
				{
					return $"Workspace settings are stored in {SettingsFactory.UserCreatedSettingsPath}";
				}
				else if (this.hasWorkspaceSettings)
				{
					return $"Workspace settings are stored in {this.dte.Solution.FileName}{BoilerplateSettingsFactory.WorkspaceSettingsFileSuffix}";
				}
				else
				{
					return "There are no settings stored in this workspace.";
				}
			}
		}

		public bool ShowCopyButton
		{
			get
			{
				return !this.hasWorkspaceSettings || SettingsFactory.LoadUserCreatedSettings;
			}
		}

		private RelayCommand copySettingsToWorkspaceCommand;
		public RelayCommand CopySettingsToWorkspaceCommand
		{
			get
			{
				return this.copySettingsToWorkspaceCommand ?? (this.copySettingsToWorkspaceCommand = new RelayCommand(
					() =>
					{
						SettingsFactory.UserCreatedSettingsPath = null;
						SettingsManager settingsManager = new ShellSettingsManager(ServiceProvider.GlobalProvider);
						WritableSettingsStore store = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);
						store.CreateCollection(PersonalBoilerplateSettingsStore.CollectionPath);
						store.DeleteProperty(PersonalBoilerplateSettingsStore.CollectionPath, BoilerplateSettingsFactory.UserBoilerplateSettings);

						this.SettingsCoordinator.SaveSettingsInOpenPages();

						var personalSettingsStore = new PersonalBoilerplateSettingsStore();
						IDictionary<string, string> templates = personalSettingsStore.GetAllTemplates();

						WorkspaceBoilerplateSettingsStore workspaceStore = new WorkspaceBoilerplateSettingsStore(
							this.dte.Solution.FullName + BoilerplateSettingsFactory.WorkspaceSettingsFileSuffix,
							templates,
							preferredTestFrameworkName: personalSettingsStore.PreferredTestFrameworkName,
							preferredMockFrameworkName: personalSettingsStore.PreferredMockFrameworkName,
							fileNameTemplate: personalSettingsStore.FileNameTemplate,
							customMocks: personalSettingsStore.CustomMocks,
							customMockFieldDeclarationTemplate: personalSettingsStore.CustomMockFieldDeclarationTemplate,
							customMockFieldInitializationTemplate: personalSettingsStore.CustomMockFieldInitializationTemplate,
							customMockObjectReferenceTemplate: personalSettingsStore.CustomMockObjectReferenceTemplate);

						workspaceStore.Apply();

						SettingsFactory.ClearSettingsFileStore();
						this.Refresh();
						this.SettingsCoordinator.RefreshOpenPages();
					}));
			}
		}

		private RelayCommand loadSettingsFileCommand;
		public RelayCommand LoadSettingsFileCommand
		{
			get
			{
				return this.loadSettingsFileCommand ?? (this.loadSettingsFileCommand = new RelayCommand(
					() =>
					{
						Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
						openFileDialog.DefaultExt = ".json";
						openFileDialog.Filter = "JSON Files (*.json)|*.json";

						bool? result = openFileDialog.ShowDialog();

						if (result == true)
						{
							SettingsFactory.UserCreatedSettingsPath = openFileDialog.FileName;
							SettingsManager settingsManager = new ShellSettingsManager(ServiceProvider.GlobalProvider);
							WritableSettingsStore store = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);

							store.CreateCollection(PersonalBoilerplateSettingsStore.CollectionPath);
							store.SetString(PersonalBoilerplateSettingsStore.CollectionPath, BoilerplateSettingsFactory.UserBoilerplateSettings, SettingsFactory.UserCreatedSettingsPath);

							SettingsFactory.ClearSettingsFileStore();
							this.Refresh();
							this.SettingsCoordinator.RefreshOpenPages();
						}
					}));
			}
		}
	}
}
