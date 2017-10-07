
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using System.Windows;
using System.ComponentModel;

namespace UnitTestBoilerplate.View
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

		// These are needed for VS setting import/export to work.

		public int Version { get; set; }

		public string Template_VisualStudio_Moq_File { get; set; }
		public string Template_VisualStudio_Moq_MockFieldDeclaration { get; set; }
		public string Template_VisualStudio_Moq_MockFieldInitialization { get; set; }
		public string Template_VisualStudio_Moq_MockObjectReference { get; set; }
		public string Template_NUnit_Moq_File { get; set; }
		public string Template_NUnit_Moq_MockFieldDeclaration { get; set; }
		public string Template_NUnit_Moq_MockFieldInitialization { get; set; }
		public string Template_NUnit_Moq_MockObjectReference { get; set; }
		public string Template_VisualStudio_AutoMoq_File { get; set; }
		public string Template_VisualStudio_AutoMoq_MockFieldDeclaration { get; set; }
		public string Template_VisualStudio_AutoMoq_MockFieldInitialization { get; set; }
		public string Template_VisualStudio_AutoMoq_MockObjectReference { get; set; }
		public string Template_NUnit_AutoMoq_File { get; set; }
		public string Template_NUnit_AutoMoq_MockFieldDeclaration { get; set; }
		public string Template_NUnit_AutoMoq_MockFieldInitialization { get; set; }
		public string Template_NUnit_AutoMoq_MockObjectReference { get; set; }
		public string Template_VisualStudio_SimpleStubs_File { get; set; }
		public string Template_VisualStudio_SimpleStubs_MockFieldDeclaration { get; set; }
		public string Template_VisualStudio_SimpleStubs_MockFieldInitialization { get; set; }
		public string Template_VisualStudio_SimpleStubs_MockObjectReference { get; set; }
		public string Template_NUnit_SimpleStubs_File { get; set; }
		public string Template_NUnit_SimpleStubs_MockFieldDeclaration { get; set; }
		public string Template_NUnit_SimpleStubs_MockFieldInitialization { get; set; }
		public string Template_NUnit_SimpleStubs_MockObjectReference { get; set; }
		public string Template_VisualStudio_NSubstitute_File { get; set; }
		public string Template_VisualStudio_NSubstitute_MockFieldDeclaration { get; set; }
		public string Template_VisualStudio_NSubstitute_MockFieldInitialization { get; set; }
		public string Template_VisualStudio_NSubstitute_MockObjectReference { get; set; }
		public string Template_NUnit_NSubstitute_File { get; set; }
		public string Template_NUnit_NSubstitute_MockFieldDeclaration { get; set; }
		public string Template_NUnit_NSubstitute_MockFieldInitialization { get; set; }
		public string Template_NUnit_NSubstitute_MockObjectReference { get; set; }
	}
}
