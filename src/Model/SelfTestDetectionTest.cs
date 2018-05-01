using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestBoilerplate.Services;

namespace UnitTestBoilerplate.Model
{
    public class SelfTestDetectionTest
    {
	    public string ProjectName { get; }

	    public IBoilerplateSettings Settings { get; }

	    public string ExpectedTestFramework { get; }

	    public string ExpectedMockFramework { get; }

	    public SelfTestDetectionTest(string projectName, IBoilerplateSettings settings, string expectedTestFramework, string expectedMockFramework)
	    {
		    this.ProjectName = projectName;
		    this.Settings = settings;
		    this.ExpectedTestFramework = expectedTestFramework;
		    this.ExpectedMockFramework = expectedMockFramework;
	    }
    }
}
