using EnvDTE;
using EnvDTE80;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.VisualStudio.Shell;
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
				if (this.hasWorkspaceSettings)
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
				return !this.hasWorkspaceSettings;
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

						this.Refresh();
						this.SettingsCoordinator.RefreshOpenPages();
					}));
			}
		}
	}
}
