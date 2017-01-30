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

namespace UnitTestBoilerplate
{
	[Guid("5d7016f4-8aa2-4b43-85f9-1145814471ba")]
	public class OptionsDialogPage : UIElementDialogPage
	{
		private OptionsDialogPageControl optionsDialogControl;

		protected override UIElement Child
        {
            get { return this.optionsDialogControl ?? (this.optionsDialogControl = new OptionsDialogPageControl()); }
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

		//private IBoilerplateSettings GetSettings()
  //      {
  //          var componentModel = (IComponentModel)(Site.GetService(typeof(SComponentModel)));
  //          return componentModel.DefaultExportProvider.GetExportedValue<IBoilerplateSettings>();
  //      }
	}
}
