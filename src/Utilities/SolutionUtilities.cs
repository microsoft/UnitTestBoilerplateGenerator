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
					list.Add(project);
				}
			}

			return list;
		}

		private static IList<string> GetProjectReferences(Project project)
		{
			XDocument document = XDocument.Load(project.FileName);
			XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";

			// Check normal references in project file
			var result = new List<string>();
			foreach (XElement element in document.Descendants(ns + "Reference"))
			{
				XAttribute includeAttribute = element.Attribute("Include");
				string fullAssemblyString = includeAttribute?.Value;

				if (!string.IsNullOrEmpty(fullAssemblyString))
				{
					string assemblyName;
					if (fullAssemblyString.Contains(','))
					{
						int commaIndex = fullAssemblyString.IndexOf(",", StringComparison.Ordinal);
						assemblyName = fullAssemblyString.Substring(0, commaIndex);
					}
					else
					{
						assemblyName = fullAssemblyString;
					}

					result.Add(assemblyName);
				}
			}

			// Check package references in project file
			foreach (XElement element in document.Descendants("PackageReference"))
			{
				XAttribute includeAttribute = element.Attribute("Include");
				if (includeAttribute != null)
				{
					result.Add(includeAttribute.Value);
				}
			}

			// Check package references in project.json
			string projectJsonPath = Path.Combine(Path.GetDirectoryName(project.FileName), "project.json");

			if (File.Exists(projectJsonPath))
			{
				try
				{
					JObject projectObject = JObject.Parse(File.ReadAllText(projectJsonPath));
					var dependenciesObject = projectObject["dependencies"] as JObject;
					if (dependenciesObject != null)
					{
						foreach (var property in dependenciesObject)
						{
							if (!result.Contains(property.Key))
							{
								result.Add(property.Key);
							}
						}
					}
				}
				catch (JsonException exception)
				{
					throw new InvalidOperationException("Could not parse project.json to search for references.", exception);
				}
			}

			return result;
		}

		public static TestFramework FindTestFramework(Project project)
		{
			var matchingFrameworks = new List<TestFramework>();

			IList<string> references = GetProjectReferences(project);
			foreach (string reference in references)
			{
				foreach (TestFramework framework in TestFrameworks.List)
				{
					foreach (string detectionReference in framework.DetectionReferenceMatches)
					{
						if (string.Compare(detectionReference, reference, StringComparison.OrdinalIgnoreCase) == 0 && !matchingFrameworks.Contains(framework))
						{
							matchingFrameworks.Add(framework);
						}
					}
				}
			}

			if (matchingFrameworks.Count == 0)
			{
				return TestFrameworks.Default;
			}

			return matchingFrameworks.OrderBy(f => f.DetectionRank).First();
		}

		public static MockFramework FindMockFramework(Project project)
		{
			var matchingFrameworks = new List<MockFramework>();

			IList<string> references = GetProjectReferences(project);
			foreach (string reference in references)
			{
				foreach (MockFramework framework in MockFrameworks.List)
				{
					foreach (string detectionReference in framework.DetectionReferenceMatches)
					{
						if (string.Compare(detectionReference, reference, StringComparison.OrdinalIgnoreCase) == 0 && !matchingFrameworks.Contains(framework))
						{
							matchingFrameworks.Add(framework);
						}
					}
				}
			}

			if (matchingFrameworks.Count == 0)
			{
				return MockFrameworks.Default;
			}

			return matchingFrameworks.OrderBy(f => f.DetectionRank).First();
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
