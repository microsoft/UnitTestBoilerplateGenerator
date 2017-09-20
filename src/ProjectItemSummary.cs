using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate
{
	public class ProjectItemSummary
	{
		public ProjectItemSummary(string filePath, string projectFilePath)
		{
			this.FilePath = filePath;
			this.ProjectFilePath = projectFilePath;
		}

		public string FilePath { get; }

		public string ProjectFilePath { get; }
	}
}
