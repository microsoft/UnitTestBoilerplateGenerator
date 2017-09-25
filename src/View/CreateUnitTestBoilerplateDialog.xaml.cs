using Microsoft.VisualStudio.PlatformUI;
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

            var viewModel = new CreateUnitTestBoilerplateViewModel();
            viewModel.View = this;

            this.DataContext = viewModel;
        }
    }
}
