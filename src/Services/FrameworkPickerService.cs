using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EnvDTE;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnitTestBoilerplate.Model;

namespace UnitTestBoilerplate.Services
{
	[Export(typeof(IFrameworkPickerService))]
	public class FrameworkPickerService : IFrameworkPickerService
	{
		[Import]
		internal IBoilerplateSettingsFactory SettingsFactory { get; set; }

		public List<TestFramework> FindTestFrameworks(Project project)
		{
			var matchingFrameworks = new List<TestFramework>();

			HashSet<string> references = GetProjectReferences(project);
			foreach (TestFramework framework in TestFrameworks.List)
			{
				foreach (string detectionReference in framework.DetectionReferenceMatches)
				{
					if (references.Contains(detectionReference) && !matchingFrameworks.Contains(framework))
					{
						matchingFrameworks.Add(framework);
					}
				}
			}

			return matchingFrameworks;
		}

		public TestFramework PickDefaultTestFramework(IList<TestFramework> frameworks, IBoilerplateSettings settings)
		{
			// If there's only one framework detected, that must be it
			if (frameworks.Count == 1)
			{
				return frameworks.First();
			}

			// If the preferred framework is included in the list, use it.
			TestFramework preferredFramework = settings.PreferredTestFramework;
			if (frameworks.Contains(preferredFramework))
			{
				return preferredFramework;
			}

			// If there's nothing in the list, pick our fallback
			if (frameworks.Count == 0)
			{
				if (preferredFramework == null)
				{
					return TestFrameworks.Default;
				}
				else
				{
					return preferredFramework;
				}
			}

			// If there are multiple frameworks and no preferred item is declared, pick the one stack ranked first.
			return frameworks.OrderBy(f => f.DetectionRank).First();
		}

		public TestFramework FindTestFramework(Project project, IBoilerplateSettings settings)
		{
			var matchingFrameworks = this.FindTestFrameworks(project);
			return this.PickDefaultTestFramework(matchingFrameworks, settings);
		}

		public List<MockFramework> FindMockFrameworks(Project project)
		{
			var matchingFrameworks = new List<MockFramework>();

			HashSet<string> references = GetProjectReferences(project);
			foreach (MockFramework framework in MockFrameworks.List)
			{
				foreach (string detectionReference in framework.DetectionReferenceMatches)
				{
					if (references.Contains(detectionReference) && !matchingFrameworks.Contains(framework))
					{
						matchingFrameworks.Add(framework);
					}
				}
			}

			return matchingFrameworks;
		}

		public MockFramework PickDefaultMockFramework(IList<MockFramework> frameworks, IBoilerplateSettings settings)
		{
			// If the preferred framework is None, use that
			MockFramework preferredFramework = settings.PreferredMockFramework;
			if (preferredFramework != null && preferredFramework.Name == MockFrameworks.NoneName)
			{
				return preferredFramework;
			}

			// If there's only one framework detected, that must be it
			if (frameworks.Count == 1)
			{
				return frameworks.First();
			}

			// If the preferred framework is included in the list, use it.
			if (frameworks.Contains(preferredFramework))
			{
				return preferredFramework;
			}

			// If there's nothing in the list, pick our fallback
			if (frameworks.Count == 0)
			{
				if (preferredFramework == null)
				{
					return MockFrameworks.Default;
				}
				else
				{
					return preferredFramework;
				}
			}

			// If there are multiple frameworks and no preferred item is declared, pick the one stack ranked first.
			return frameworks.OrderBy(f => f.DetectionRank).First();
		}

		public MockFramework FindMockFramework(Project project, IBoilerplateSettings settings)
		{
			var matchingFrameworks = this.FindMockFrameworks(project);
			return this.PickDefaultMockFramework(matchingFrameworks, settings);
		}

		private static HashSet<string> GetProjectReferences(Project project)
		{
			var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

			// First look for direct references from loaded project
			var vsProject = project.Object as VSLangProj.VSProject;
			foreach (VSLangProj.Reference reference in vsProject.References)
			{
				result.Add(reference.Name);
			}

			XDocument document = XDocument.Load(project.FileName);
			XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";

			// Check normal references in project file
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
			List<XElement> packageReferenceElements = document.Descendants("PackageReference").ToList();
			packageReferenceElements.AddRange(document.Descendants(ns + "PackageReference"));

			foreach (XElement element in packageReferenceElements)
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
	}
}
