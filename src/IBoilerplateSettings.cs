namespace UnitTestBoilerplate
{
	// TODO: Migrate from StaticBoilerplateSettings to this
	public interface IBoilerplateSettings
	{
		void SaveSelectedTestProject(string solutionPath, string testProjectPath);

		string GetLastSelectedProject(string solutionPath);

		string GetTemplate(MockFramework mockFramework, TemplateType templateType);

		string GetDefaultTemplate(MockFramework mockFramework, TemplateType templateType);

		void SetTemplate(MockFramework mockFramework, TemplateType templateType, string template);

		void SetTemplate(string mockFrameworkString, string templateTypeString, string template);

		void RevertTemplateToDefault(MockFramework mockFramework);
	}
}