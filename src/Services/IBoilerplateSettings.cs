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
	    TestFramework PreferredTestFramework { get; set; }
	    MockFramework PreferredMockFramework { get; set; }
	    string FileNameTemplate { get; set; }
		IDictionary<string, string> CustomMocks { get; set; }
		string CustomMockFieldDeclarationTemplate { get; set; }
		string CustomMockFieldInitializationTemplate { get; set; }
		string CustomMockObjectReferenceTemplate { get; set; }
		string GetTemplate(TestFramework testFramework, MockFramework mockFramework, TemplateType templateType);
	    void SetTemplate(string testFrameworkString, string mockFrameworkString, string templateTypeString, string template);
	    void RevertTemplateToDefault(TestFramework testFramework, MockFramework mockFramework);
		void Apply();
    }
}
