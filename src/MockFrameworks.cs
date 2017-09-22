using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate
{
	public static class MockFrameworks
	{
		static MockFrameworks()
		{
			List = new List<MockFramework>
			{
				new MockFramework(
					name: "Moq",
					detectionReferenceMatches: new List<string> { "Moq" },
					detectionRank: 1,
					usingNamespaces: new List<string> { "Moq" },
					supportsGenerics: true),
				new MockFramework(
					name: "AutoMoq",
					detectionReferenceMatches: new List<string> { "AutoMoq" },
					detectionRank: 0,
					usingNamespaces: new List<string> { "AutoMoq", "Moq" },
					supportsGenerics: true),
				new MockFramework(
					name: "SimpleStubs",
					detectionReferenceMatches: new List<string> { "Etg.SimpleStubs" },
					detectionRank: 1,
					usingNamespaces: new List<string>(),
					supportsGenerics: false),
				new MockFramework(
					name: "NSubstitute",
					detectionReferenceMatches: new List<string> { "NSubstitute" },
					detectionRank: 1,
					usingNamespaces: new List<string> { "NSubstitute" },
					supportsGenerics: true),
			};
		}

		public static IList<MockFramework> List { get; }

		public static MockFramework Default => List[0];
	}
}
