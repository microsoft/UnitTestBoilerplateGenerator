using System;
using System.Collections.Generic;
using System.IO;
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
using EnvDTE;
using EnvDTE80;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell.Settings;

namespace UnitTestBoilerplate
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
