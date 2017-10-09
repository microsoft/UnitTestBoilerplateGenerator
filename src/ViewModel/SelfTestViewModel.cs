using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using GalaSoft.MvvmLight;
using Microsoft.VisualStudio.Shell;
using UnitTestBoilerplate.Commands;
using UnitTestBoilerplate.Model;
using UnitTestBoilerplate.Utilities;

namespace UnitTestBoilerplate.ViewModel
{
	public class SelfTestViewModel : ViewModelBase
	{
		private readonly DTE2 dte;
		private IList<Project> projects;

		public SelfTestViewModel()
		{
			this.dte = (DTE2)ServiceProvider.GlobalProvider.GetService(typeof(DTE));
			this.projects = SolutionUtilities.GetProjects(this.dte);
			this.status = "Hello";

			var createTestService = new CreateTestService();

			createTestService.Clean(this.projects);

			this.GenerateTestFilesAsync();
		}

		private string status;
		public string Status
		{
			get { return this.status; }
			set { this.Set(ref this.status, value); }
		}

		public async System.Threading.Tasks.Task GenerateTestFilesAsync()
		{
			Project classesProject = this.projects.Single(p => p.Name == "SandboxClasses");
			ProjectItem casesFolder = classesProject.ProjectItems.Item("Cases");
			var createViewModel = new CreateUnitTestBoilerplateViewModel();
			foreach (var testProject in createViewModel.TestProjects)
			{
				if (testProject.Name.Contains("TestCases"))
				{
					createViewModel.SelectedProject = testProject;

					foreach (ProjectItem classToTest in casesFolder.ProjectItems)
					{
						await createViewModel.GenerateUnitTestFromProjectItemSummaryAsync(new ProjectItemSummary(classToTest), openFile: false);
					}
				}
			}
		}
	}
}
