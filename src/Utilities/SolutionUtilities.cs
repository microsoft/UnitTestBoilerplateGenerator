using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EnvDTE;
using EnvDTE80;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnitTestBoilerplate.Model;

namespace UnitTestBoilerplate.Utilities
{
	public static class SolutionUtilities
	{
		public static IList<ProjectItemSummary> GetSelectedFiles(DTE2 dte)
		{
			var items = (Array)dte.ToolWindows.SolutionExplorer.SelectedItems;

			return items.Cast<UIHierarchyItem>()
				.Select(i =>
				{
					var projectItem = i.Object as ProjectItem;
					return new ProjectItemSummary(projectItem);
				})
				.ToList();
		}

		public static IList<Project> GetProjects(DTE2 dte)
		{
			Projects projects = dte.Solution.Projects;
			List<Project> list = new List<Project>();
			var item = projects.GetEnumerator();
			while (item.MoveNext())
			{
				var project = item.Current as Project;
				if (project == null)
				{
					continue;
				}

				if (project.Kind == ProjectKinds.vsProjectKindSolutionFolder)
				{
					list.AddRange(GetSolutionFolderProjects(project));
				}
				else
				{
					try
					{
						if (!string.IsNullOrEmpty(project.FullName))
						{
							list.Add(project);
						}
					}
					catch (Exception)
					{
					}
				}
			}

			return list;
		}

		public static string GetSelfTestDirectoryFromSandbox(DTE2 dte)
		{
			string solutionDirectory = Path.GetDirectoryName(dte.Solution.FileName);
			string rootDirectory = Path.GetDirectoryName(solutionDirectory);
			return Path.Combine(rootDirectory, "SelfTestFiles");
		}

		private static IEnumerable<Project> GetSolutionFolderProjects(Project solutionFolder)
		{
			List<Project> list = new List<Project>();
			for (var i = 1; i <= solutionFolder.ProjectItems.Count; i++)
			{
				var subProject = solutionFolder.ProjectItems.Item(i).SubProject;
				if (subProject == null)
				{
					continue;
				}

				// If this is another solution folder, do a recursive call, otherwise add
				if (subProject.Kind == ProjectKinds.vsProjectKindSolutionFolder)
				{
					list.AddRange(GetSolutionFolderProjects(subProject));
				}
				else
				{
					list.Add(subProject);
				}
			}

			return list;
		}
	}
}
