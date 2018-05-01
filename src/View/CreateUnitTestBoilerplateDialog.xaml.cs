using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell;
using System.ComponentModel.Composition;
using UnitTestBoilerplate.ViewModel;

namespace UnitTestBoilerplate.View
{
    /// <summary>
    /// Interaction logic for CreateUnitTestBoilerplateWindow.xaml
    /// </summary>
    public partial class CreateUnitTestBoilerplateDialog : DialogWindow, ICreateUnitTestBoilerplateView
    {
        public CreateUnitTestBoilerplateDialog()
        {
            this.InitializeComponent();

	        IComponentModel componentModel = (IComponentModel)ServiceProvider.GlobalProvider.GetService(typeof(SComponentModel));
			var viewModel = new CreateUnitTestBoilerplateViewModel();
			componentModel.DefaultCompositionService.SatisfyImportsOnce(viewModel);
			viewModel.Initialize();
            viewModel.View = this;

            this.DataContext = viewModel;
        }
    }
}
