using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using Microsoft.VisualStudio.ComponentModelHost;

namespace UnitTestBoilerplate.View
{
	[Guid("6e5e083f-c2d6-457e-b3e2-891f781208f1")]
	public class FileLocationOptionsDialogPage : UIElementDialogPage
	{
		private FileLocationOptionsDialogPageControl optionsDialogControl;

		protected override UIElement Child
		{
			get { return this.optionsDialogControl ?? (this.optionsDialogControl = new FileLocationOptionsDialogPageControl()); }
		}

		protected override void OnActivate(CancelEventArgs e)
		{
			base.OnActivate(e);

			this.optionsDialogControl.ViewModel.Initialize();
		}

		protected override void OnApply(PageApplyEventArgs args)
		{
			if (args.ApplyBehavior == ApplyKind.Apply)
			{
				this.optionsDialogControl.ViewModel.Apply();
			}

			base.OnApply(args);
		}
	}
}
