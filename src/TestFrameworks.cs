using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate
{
	public static class TestFrameworks
	{
		static TestFrameworks()
		{
			List = new List<TestFramework>
			{
				new TestFramework(
					name: "VisualStudio",
					detectionReferenceMatches: new List<string> { "Microsoft.VisualStudio.QualityTools.UnitTestFramework" },
					detectionRank: 1,
					usingString: "Microsoft.VisualStudio.TestTools.UnitTesting",
					testClassAttribute: "TestClass",
					testMethodAttribute: "TestMethod",
					testInitializeAttribute: "TestInitialize",
					testCleanupAttribute: "TestCleanup"),

				new TestFramework(
					name: "NUnit",
					detectionReferenceMatches: new List<string> { "NUnit", "NUnit.Framework" },
					detectionRank: 0,
					usingString: "NUnit.Framework",
					testClassAttribute: "TestFixture",
					testMethodAttribute: "Test",
					testInitializeAttribute: "SetUp",
					testCleanupAttribute: "TearDown"),
			};
		}

		public static IList<TestFramework> List { get; }

		public static TestFramework Default => List[0];
	}
}
