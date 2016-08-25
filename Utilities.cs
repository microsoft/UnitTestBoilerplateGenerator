using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;

namespace UnitTestBoilerplate
{
    public static class Utilities
    {
        public static string GetTypeBaseName(string typeName)
        {
            if (typeName.Length >= 2 &&
                typeName.StartsWith("I", StringComparison.Ordinal) &&
                typeName[1] >= 'A' &&
                typeName[1] <= 'Z')
            {
                return typeName.Substring(1);
            }

            return typeName;
        }

        public static IEnumerable<ProjectItemSummary> GetSelectedFiles(DTE2 dte)
        {
            var items = (Array)dte.ToolWindows.SolutionExplorer.SelectedItems;

            return items.Cast<UIHierarchyItem>().Select(i =>
            {
                var projectItem = i.Object as ProjectItem;
                return new ProjectItemSummary(projectItem.FileNames[1], projectItem.ContainingProject.FileName);
            });
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
                    list.Add(project);
                }
            }

            return list;
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
