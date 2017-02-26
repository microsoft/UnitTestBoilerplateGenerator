using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EnvDTE;
using EnvDTE80;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project = EnvDTE.Project;

namespace UnitTestBoilerplate
{
    public static class Utilities
    {
        public static string GetTypeBaseName(string typeName)
        {
            if (IsInterfaceName(typeName))
            {
                return typeName.Substring(1);
            }

            return typeName;
        }

		/// <summary>
		/// Gets a component for a longer name given a type component.
		/// </summary>
		/// <param name="typeName">The name of a type, this might be an interface or type keyword that came as a generic
		/// type argument.</param>
		/// <returns>The PascalCased type name component.</returns>
		public static string GetTypeNameComponent(string typeName)
		{
			if (IsInterfaceName(typeName))
			{
				return typeName.Substring(1);
			}

			if (char.IsLower(typeName[0]))
			{
				return typeName.Substring(0, 1).ToUpperInvariant() + typeName.Substring(1);
			}

			return typeName;
		}

		private static bool IsInterfaceName(string typeName)
		{
			return typeName.Length >= 2 &&
							typeName.StartsWith("I", StringComparison.Ordinal) &&
							typeName[1] >= 'A' &&
							typeName[1] <= 'Z';
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

        public static bool TryGetParentSyntax<T>(SyntaxNode syntaxNode, out T result)
            where T : SyntaxNode
        {
            result = null;

            if (syntaxNode == null)
            {
                return false;
            }

            try
            {
                syntaxNode = syntaxNode.Parent;

                if (syntaxNode == null)
                {
                    return false;
                }

                if (syntaxNode.GetType() == typeof(T))
                {
                    result = syntaxNode as T;
                    return true;
                }

                return TryGetParentSyntax<T>(syntaxNode, out result);
            }
            catch
            {
                return false;
            }
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

	    public static TestFramework FindTestFramework(Project project)
	    {
			IList<string> references = GetProjectReferences(project);
			foreach (string reference in references)
			{
				switch (reference.ToLowerInvariant())
				{
					case "nunit.framework":
					case "nunit":
						return TestFramework.NUnit;
					case "microsoft.visualstudio.qualitytools.unittestframework":
						return TestFramework.VisualStudio;
				}
			}

			return TestFramework.Unknown;
	    }

	    public static MockFramework FindMockFramework(Project project)
	    {
			IList<string> references = GetProjectReferences(project);
			bool hasMoqReference = false;
			foreach (string reference in references)
			{
			    switch (reference.ToLowerInvariant())
			    {
					case "automoq":
						return MockFramework.AutoMoq;
					case "moq":
						hasMoqReference = true;
						break;
					case "etg.simplestubs":
					    return MockFramework.SimpleStubs;
					case "nsubstitute":
						return MockFramework.NSubstitute;
				}
			}

			if (hasMoqReference)
			{
				return MockFramework.Moq;
			}

			return MockFramework.Unknown;
		}

	    private static IList<string> GetProjectReferences(Project project)
	    {
			XDocument document = XDocument.Load(project.FileName);
			XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";

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
