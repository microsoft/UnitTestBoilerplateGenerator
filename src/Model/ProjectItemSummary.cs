using EnvDTE;

namespace UnitTestBoilerplate.Model
{
	public class ProjectItemSummary
	{
		public ProjectItemSummary(string filePath, string projectFilePath)
		{
			this.FilePath = filePath;
			this.ProjectFilePath = projectFilePath;
		}

		public ProjectItemSummary(ProjectItem projectItem)
		{
			this.FilePath = projectItem.FileNames[1];
			this.ProjectFilePath = projectItem.ContainingProject.FullName;
		}

		public string FilePath { get; }

		public string ProjectFilePath { get; }
	}
}
