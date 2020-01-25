using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using UnitTestBoilerplate.ViewModel;

namespace UnitTestBoilerplate.View
{
	/// <summary>
	/// Interaction logic for OptionsDialogPageControl.xaml
	/// </summary>
	public partial class FileContentsOptionsDialogPageControl : UserControl
	{
		public FileContentsOptionsDialogViewModel ViewModel { get; }

		public FileContentsOptionsDialogPageControl()
		{
			this.InitializeComponent();

			this.ViewModel = new FileContentsOptionsDialogViewModel();
			IComponentModel componentModel = (IComponentModel)ServiceProvider.GlobalProvider.GetService(typeof(SComponentModel));
			componentModel.DefaultCompositionService.SatisfyImportsOnce(this.ViewModel);
			this.ViewModel.Initialize();
			//this.ViewModel.SettingsCoordinator.ReportSettingsPageOpen(this.ViewModel);

			this.DataContext = this.ViewModel;

			this.fileTemplateTextBox.AddHandler(UIElementDialogPage.DialogKeyPendingEvent, new RoutedEventHandler(this.OnDialogKeyPendingEvent));
			this.extraUsingNamespacesTextBox.AddHandler(UIElementDialogPage.DialogKeyPendingEvent, new RoutedEventHandler(this.OnDialogKeyPendingEvent));
			this.testMethodInvocationTextBox.AddHandler(UIElementDialogPage.DialogKeyPendingEvent, new RoutedEventHandler(this.OnDialogKeyPendingEvent));
			this.testMethodEmptyTextBox.AddHandler(UIElementDialogPage.DialogKeyPendingEvent, new RoutedEventHandler(this.OnDialogKeyPendingEvent));
		}

		void OnDialogKeyPendingEvent(object sender, RoutedEventArgs e)
		{
			var args = e as DialogKeyEventArgs;
			if (args != null && args.Key == Key.Enter)
			{
				e.Handled = true;
			}
		}
	}
}
