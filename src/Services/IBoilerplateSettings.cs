using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestBoilerplate.Model;

namespace UnitTestBoilerplate.Services
{
    public interface IBoilerplateSettings
    {
	    void SaveSelectedTestProject(string solutionPath, string testProjectPath);
	    string GetLastSelectedProject(string solutionPath);
	    TestFramework PreferredTestFramework { get; set; }
	    MockFramework PreferredMockFramework { get; set; }
	    string FileNameTemplate { get; set; }
	    string GetTemplate(TestFramework testFramework, MockFramework mockFramework, TemplateType templateType);
	    void SetTemplate(TestFramework testFramework, MockFramework mockFramework, TemplateType templateType, string template);
	    void SetTemplate(string testFrameworkString, string mockFrameworkString, string templateTypeString, string template);
	    void RevertTemplateToDefault(TestFramework testFramework, MockFramework mockFramework);
    }
}
