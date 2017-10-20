using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using UnitTestBoilerplate.Model;
using UnitTestBoilerplate.Utilities;
using UnitTestBoilerplate.ViewModel;

namespace UnitTestBoilerplate.Services
{
	public class SelfTestService
	{
		public void Clean(IList<Project> projects = null, bool save = false)
		{
			if (projects == null)
			{
				var dte = (DTE2)ServiceProvider.GlobalProvider.GetService(typeof(DTE));
				projects = SolutionUtilities.GetProjects(dte);
			}

			foreach (var project in projects)
			{
				if (project.Name.Contains("TestCases"))
				{
					// Delete Cases folder from project if it exists.
					foreach (ProjectItem item in project.ProjectItems)
					{
						if (item.Name == "Cases")
						{
							item.Delete();
							project.Save();
							break;
						}
					}

					// Delete folder if it existed but was not added to project.
					string projectFolder = Path.GetDirectoryName(project.FileName);
					string casesFolder = Path.Combine(projectFolder, "Cases");

					if (Directory.Exists(casesFolder))
					{
						Directory.Delete(casesFolder, recursive: true);
					}
				}
			}
		}

		public async System.Threading.Tasks.Task GenerateTestFilesAsync(IList<Project> projects)
		{
			Project classesProject = projects.Single(p => p.Name == "Classes");
			ProjectItem casesFolder = classesProject.ProjectItems.Item("Cases");

			var createViewModel = new CreateUnitTestBoilerplateViewModel();
			foreach (var testProject in createViewModel.TestProjects)
			{
				if (testProject.Name.Contains("TestCases"))
				{
					createViewModel.SelectedProject = testProject;

					foreach (ProjectItem classToTest in casesFolder.ProjectItems)
					{
						await createViewModel.CreateUnitTestAsync(new List<ProjectItemSummary> { new ProjectItemSummary(classToTest) }, addToProject: false);
					}
				}
			}
		}
	}
}
