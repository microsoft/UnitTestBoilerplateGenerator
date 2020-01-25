using UnitTestBoilerplate.Model;

namespace UnitTestBoilerplate.Services
{
	public interface ISettingsCoordinator
	{
		void RefreshOpenPages();
		void ReportSettingsPageClosed(ISettingsPageViewModel settingsPage);
		void ReportSettingsPageOpen(ISettingsPageViewModel settingsPage);
		void SaveSettingsInOpenPages();
	}
}