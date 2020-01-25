using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestBoilerplate.Model;

namespace UnitTestBoilerplate.Services
{
    public class MockBoilerplateSettings : IBoilerplateSettings
    {
	    public void SaveSelectedTestProject(string solutionPath, string testProjectPath)
	    {
	    }

	    public string GetLastSelectedProject(string solutionPath)
	    {
		    return null;
	    }

	    public TestFramework PreferredTestFramework { get; set; }
	    public MockFramework PreferredMockFramework { get; set; }
	    public string FileNameTemplate { get; set; }
	    public string GetTemplate(TestFramework testFramework, MockFramework mockFramework, TemplateType templateType)
	    {
		    return null;
	    }

	    public void SetTemplate(TestFramework testFramework, MockFramework mockFramework, TemplateType templateType, string template)
	    {
	    }

	    public void SetTemplate(string testFrameworkString, string mockFrameworkString, string templateTypeString, string template)
	    {
	    }

	    public void RevertTemplateToDefault(TestFramework testFramework, MockFramework mockFramework)
	    {
	    }

		public void Apply()
		{
		}
	}
}
