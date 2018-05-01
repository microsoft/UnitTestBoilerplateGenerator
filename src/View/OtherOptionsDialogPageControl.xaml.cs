using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using UnitTestBoilerplate.ViewModel;

namespace UnitTestBoilerplate.View
{
	/// <summary>
	/// Interaction logic for OtherOptionsDialogPageControl.xaml
	/// </summary>
	public partial class OtherOptionsDialogPageControl : UserControl
	{
		public OtherOptionsDialogViewModel ViewModel { get; }

		public OtherOptionsDialogPageControl()
		{
			this.InitializeComponent();

			this.ViewModel = new OtherOptionsDialogViewModel();
			IComponentModel componentModel = (IComponentModel)ServiceProvider.GlobalProvider.GetService(typeof(SComponentModel));
			componentModel.DefaultCompositionService.SatisfyImportsOnce(this.ViewModel);

			this.DataContext = this.ViewModel;
		}
	}
}
