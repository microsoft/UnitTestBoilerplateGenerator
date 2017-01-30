using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UnitTestBoilerplate
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

			this.fileTemplateTextBox.AddHandler(UIElementDialogPage.DialogKeyPendingEvent, new RoutedEventHandler(OnDialogKeyPendingEvent));
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
