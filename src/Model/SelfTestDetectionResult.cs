using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate.Model
{
	public class SelfTestDetectionResult
	{
		public SelfTestDetectionResult(string testFramework, string mockFramework)
		{
			this.TestFramework = testFramework;
			this.MockFramework = mockFramework;
		}

		public string TestFramework { get; set; }

		public string MockFramework { get; set; }
	}
}
