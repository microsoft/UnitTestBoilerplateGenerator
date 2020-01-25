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
	[Guid("97da351a-9f82-433d-b058-22c943b03989")]
	public class WorkspaceSettingsDialogPage : UIElementDialogPage
	{
		private WorkspaceSettingsDialogPageControl workspaceSettingsDialogControl;

		protected override UIElement Child
		{
			get { return this.workspaceSettingsDialogControl ?? (this.workspaceSettingsDialogControl = new WorkspaceSettingsDialogPageControl()); }
		}

		protected override void OnActivate(CancelEventArgs e)
		{
			base.OnActivate(e);

			this.workspaceSettingsDialogControl.ViewModel.Refresh();
			//this.workspaceSettingsDialogControl.ViewModel.Initialize();
		}

		protected override void OnApply(PageApplyEventArgs args)
		{
			//if (args.ApplyBehavior == ApplyKind.Apply)
			//{
			//	this.workspaceSettingsDialogControl.ViewModel.Apply();
			//}

			base.OnApply(args);
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
		}
	}
}
