namespace UnitTestBoilerplate.Model
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
