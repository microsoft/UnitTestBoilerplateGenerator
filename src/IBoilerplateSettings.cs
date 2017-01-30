namespace UnitTestBoilerplate
{
	// TODO: Migrate from StaticBoilerplateSettings to this
	public interface IBoilerplateSettings
	{
		void SaveSelectedTestProject(string solutionPath, string testProjectPath);

		string GetLastSelectedProject(string solutionPath);
	}
}