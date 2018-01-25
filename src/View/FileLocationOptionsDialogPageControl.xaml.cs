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
using UnitTestBoilerplate.ViewModel;

namespace UnitTestBoilerplate.View
{
	/// <summary>
	/// Interaction logic for FileLocationOptionsDialogPageControl.xaml
	/// </summary>
	public partial class FileLocationOptionsDialogPageControl : UserControl
	{
		public FileLocationOptionsDialogViewModel ViewModel { get; }

		public FileLocationOptionsDialogPageControl()
		{
			this.InitializeComponent();

			this.ViewModel = new FileLocationOptionsDialogViewModel();
			this.DataContext = this.ViewModel;
		}
	}
}
