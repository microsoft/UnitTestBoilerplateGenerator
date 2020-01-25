using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestBoilerplate.Model;

namespace UnitTestBoilerplate.Services
{
	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export(typeof(ISettingsCoordinator))]
	public class SettingsCoordinator : ISettingsCoordinator
	{
		private readonly List<ISettingsPageViewModel> openViewModels = new List<ISettingsPageViewModel>();

		public void ReportSettingsPageOpen(ISettingsPageViewModel settingsPage)
		{
			this.openViewModels.Add(settingsPage);
		}

		public void ReportSettingsPageClosed(ISettingsPageViewModel settingsPage)
		{
			this.openViewModels.Remove(settingsPage);
		}

		public void RefreshOpenPages()
		{
			foreach (var openViewModel in this.openViewModels)
			{
				openViewModel.Refresh();
			}
		}

		public void SaveSettingsInOpenPages()
		{
			foreach (var openViewModel in this.openViewModels)
			{
				openViewModel.SaveCurrentSettings();
			}
		}
	}
}
