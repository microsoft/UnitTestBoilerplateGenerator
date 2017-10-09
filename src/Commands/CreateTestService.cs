using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using UnitTestBoilerplate.Utilities;

namespace UnitTestBoilerplate.Commands
{
	public class CreateTestService
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
					foreach (ProjectItem item in project.ProjectItems)
					{
						if (item.Name == "Cases")
						{
							item.Delete();
						}
					}

					project.Save();
				}
			}
		}
	}
}
