using System.IO;
using EnvDTE;

namespace UnitTestBoilerplate.Model
{
	public class TestProject
	{
		public string Name { get; set; }

		public Project Project { get; set; }

		public string ProjectDirectory => Path.GetDirectoryName(this.Project.FullName);
	}
}
