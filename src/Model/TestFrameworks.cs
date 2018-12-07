using System.Collections.Generic;
using System.Linq;

namespace UnitTestBoilerplate.Model
{
	public static class TestFrameworks
	{
		public const string VisualStudioName = "VisualStudio";
		public const string NUnitName = "NUnit";
		public const string XUnitName = "xUnit";

		static TestFrameworks()
		{
			List = new List<TestFramework>
			{
				new TestFramework(
					name: VisualStudioName,
					detectionReferenceMatches: new List<string> { "Microsoft.VisualStudio.QualityTools.UnitTestFramework", "Microsoft.VisualStudio.TestPlatform.TestFramework" },
					detectionRank: 1,
					usingString: "Microsoft.VisualStudio.TestTools.UnitTesting",
					testClassAttribute: "TestClass",
					testMethodAttribute: "TestMethod",
					testInitializeAttribute: "TestInitialize",
					testCleanupAttribute: "TestCleanup",
					testInitializeStyle: TestInitializeStyle.AttributedMethod,
					testCleanupStyle: TestCleanupStyle.AttributedMethod,
					assertFailStatement: "Assert.Fail();"),

				new TestFramework(
					name: NUnitName,
					detectionReferenceMatches: new List<string> { "NUnit", "NUnit.Framework" },
					detectionRank: 0,
					usingString: "NUnit.Framework",
					testClassAttribute: "TestFixture",
					testMethodAttribute: "Test",
					testInitializeAttribute: "SetUp",
					testCleanupAttribute: "TearDown",
					testInitializeStyle: TestInitializeStyle.AttributedMethod,
					testCleanupStyle: TestCleanupStyle.AttributedMethod,
					assertFailStatement: "Assert.Fail();"),

				new TestFramework(
					name: XUnitName,
					detectionReferenceMatches: new List<string> { "xunit", "xunit.core" },
					detectionRank: 0,
					usingString: "Xunit",
					testClassAttribute: null,
					testMethodAttribute: "Fact",
					testInitializeAttribute: null,
					testCleanupAttribute: null,
					testInitializeStyle: TestInitializeStyle.Constructor,
					testCleanupStyle: TestCleanupStyle.Disposable,
					assertFailStatement: "Assert.True(false);"),
			}.AsReadOnly();
		}

		public static IList<TestFramework> List { get; }

		public static TestFramework Default => List[0];

		public static TestFramework Get(string name)
		{
			return List.First(f => f.Name == name);
		}
	}
}
