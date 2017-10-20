using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualStudio.PlatformUI;
using UnitTestBoilerplate.ViewModel;

namespace UnitTestBoilerplate.View
{
	/// <summary>
	/// Interaction logic for SelfTestDialog.xaml
	/// </summary>
	public partial class SelfTestDialog : DialogWindow
	{
		public SelfTestDialog()
		{
			this.InitializeComponent();

			this.DataContext = new SelfTestViewModel();
		}

		private void ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			var scrollViewerToUpdate = sender == this.beforeDiff ?  this.afterDiff : this.beforeDiff;

			scrollViewerToUpdate.ScrollToVerticalOffset(e.VerticalOffset);
			scrollViewerToUpdate.ScrollToHorizontalOffset(e.HorizontalOffset);
		}
	}
}
