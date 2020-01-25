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
using EnvDTE80;
using EnvDTE;
using UnitTestBoilerplate.Services;
using System.ComponentModel.Composition;

namespace UnitTestBoilerplate.View
{
	[Guid("6e5e083f-c2d6-457e-b3e2-891f781208f1")]
	public class OtherOptionsDialogPage : UIElementDialogPage
	{
		private OtherOptionsDialogPageControl optionsDialogControl;

		protected override UIElement Child
		{
			get { return this.optionsDialogControl ?? (this.optionsDialogControl = new OtherOptionsDialogPageControl()); }
		}

		protected override void OnActivate(CancelEventArgs e)
		{
			base.OnActivate(e);

			this.optionsDialogControl.ViewModel.Refresh();
			this.optionsDialogControl.ViewModel.SettingsCoordinator.ReportSettingsPageOpen(this.optionsDialogControl.ViewModel);
		}

		protected override void OnApply(PageApplyEventArgs args)
		{
			if (args.ApplyBehavior == ApplyKind.Apply)
			{
				this.optionsDialogControl.ViewModel.Apply();
			}

			base.OnApply(args);
		}

		protected override void OnClosed(EventArgs e)
		{
			this.optionsDialogControl.ViewModel.SettingsCoordinator.ReportSettingsPageClosed(this.optionsDialogControl.ViewModel);

			base.OnClosed(e);
		}
	}
}
