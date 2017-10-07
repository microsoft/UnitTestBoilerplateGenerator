using System.Collections.Generic;
using System.Linq;

namespace UnitTestBoilerplate.Model
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
					testCleanupAttribute: "TestCleanup",
					testInitializeStyle: TestInitializeStyle.AttributedMethod,
					testCleanupStyle: TestCleanupStyle.AttributedMethod),

				new TestFramework(
					name: "NUnit",
					detectionReferenceMatches: new List<string> { "NUnit", "NUnit.Framework" },
					detectionRank: 0,
					usingString: "NUnit.Framework",
					testClassAttribute: "TestFixture",
					testMethodAttribute: "Test",
					testInitializeAttribute: "SetUp",
					testCleanupAttribute: "TearDown",
					testInitializeStyle: TestInitializeStyle.AttributedMethod,
					testCleanupStyle: TestCleanupStyle.AttributedMethod),

				new TestFramework(
					name: "xUnit",
					detectionReferenceMatches: new List<string> { "xunit" },
					detectionRank: 0,
					usingString: "Xunit",
					testClassAttribute: null,
					testMethodAttribute: "Fact",
					testInitializeAttribute: null,
					testCleanupAttribute: null,
					testInitializeStyle: TestInitializeStyle.Constructor,
					testCleanupStyle: TestCleanupStyle.Disposable),
			};
		}

		public static IList<TestFramework> List { get; }

		public static TestFramework Default => List[0];

		public static TestFramework Get(string name)
		{
			return List.First(f => f.Name == name);
		}
	}
}
