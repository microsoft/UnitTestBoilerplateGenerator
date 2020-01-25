using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.Win32;
using UnitTestBoilerplate.Services;
using UnitTestBoilerplate.Utilities;
using Task = System.Threading.Tasks.Task;

namespace UnitTestBoilerplate.Commands
{
	internal sealed class DetectCsprojCommand
	{
		/// <summary>
		/// Command ID.
		/// </summary>
		public const int CommandId = 0x0103;

		/// <summary>
		/// Command menu group (command set GUID).
		/// </summary>
		public static readonly Guid CommandSet = new Guid("542fa460-e966-445e-b2e2-3b82e1a75ca4");

		/// <summary>
		/// VS Package that provides this command, not null.
		/// </summary>
		private readonly AsyncPackage package;

		/// <summary>
		/// Initializes a new instance of the <see cref="SelfTestCommand"/> class.
		/// Adds our command handlers for menu (commands must exist in the command table file)
		/// </summary>
		/// <param name="package">Owner package, not null.</param>
		private DetectCsprojCommand(AsyncPackage package)
		{
			if (package == null)
			{
				throw new ArgumentNullException(nameof(package));
			}

			this.package = package;
		}

		/// <summary>
		/// Gets the instance of the command.
		/// </summary>
		public static DetectCsprojCommand Instance
		{
			get;
			private set;
		}

		/// <summary>
		/// Initializes the singleton instance of the command.
		/// </summary>
		/// <param name="package">Owner package, not null.</param>
		public static async Task InitializeAsync(AsyncPackage package)
		{
			Instance = new DetectCsprojCommand(package);

			await Instance.InitializeAsync();
		}

		private async Task InitializeAsync()
		{
			OleMenuCommandService commandService = await this.package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
			if (commandService != null)
			{
				var menuCommandID = new CommandID(CommandSet, CommandId);
				var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
				commandService.AddCommand(menuItem);
			}
		}

		/// <summary>
		/// This function is the callback used to execute the command when the menu item is clicked.
		/// See the constructor to see how the menu item is associated with this function using
		/// OleMenuCommandService service and MenuCommand class.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">Event args.</param>
		private async void MenuItemCallback(object sender, EventArgs e)
		{
			var dialog = new OpenFileDialog();
			dialog.Title = "Choose .csproj file to run detection on";
			dialog.Filter = "Project files|*.csproj";
			if (dialog.ShowDialog() == true)
			{
				IComponentModel componentModel = (IComponentModel)await this.package.GetServiceAsync(typeof(SComponentModel));
				var frameworkPickerService = componentModel.GetService<IFrameworkPickerService>();
				var settingsFactory = componentModel.GetService<IBoilerplateSettingsFactory>();
				IBoilerplateSettings settings = settingsFactory.Get();

				MessageBox.Show("Test framework: " + frameworkPickerService.FindTestFramework(dialog.FileName, settings) + Environment.NewLine + "Mock framework: " +
				                frameworkPickerService.FindMockFramework(dialog.FileName, settings));
			}
		}
	}
}
