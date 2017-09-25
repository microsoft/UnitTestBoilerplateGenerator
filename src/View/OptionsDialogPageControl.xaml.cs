using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.VisualStudio.Shell;
using UnitTestBoilerplate.ViewModel;

namespace UnitTestBoilerplate.View
{
	/// <summary>
	/// Interaction logic for OptionsDialogPageControl.xaml
	/// </summary>
	public partial class OptionsDialogPageControl : UserControl
	{
		public OptionsDialogViewModel ViewModel { get; }

		public OptionsDialogPageControl()
		{
			this.InitializeComponent();

			this.ViewModel = new OptionsDialogViewModel();
			this.DataContext = this.ViewModel;

			this.fileTemplateTextBox.AddHandler(UIElementDialogPage.DialogKeyPendingEvent, new RoutedEventHandler(this.OnDialogKeyPendingEvent));
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
