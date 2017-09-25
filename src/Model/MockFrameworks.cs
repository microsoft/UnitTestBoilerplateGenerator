using System.Collections.Generic;
using System.Linq;

namespace UnitTestBoilerplate.Model
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
					supportsGenerics: true,
					classStartCode: "private MockRepository mockRepository;",
					hasMockFields: true,
					initializeStartCode: "this.mockRepository = new MockRepository(MockBehavior.Strict);",
					mockFieldDeclarationCode: "private Mock<$InterfaceType$> mock$InterfaceMockName$;",
					mockFieldInitializationCode: "this.mock$InterfaceMockName$ = this.mockRepository.Create<$InterfaceType$>();",
					testCleanupCode: "this.mockRepository.VerifyAll();",
					testArrangeCode: null,
					testedObjectCreationStyle: TestedObjectCreationStyle.HelperMethod, 
					testedObjectCreationCode: null,
					mockObjectReferenceCode: "this.mock$InterfaceMockName$.Object"),
				new MockFramework(
					name: "AutoMoq",
					detectionReferenceMatches: new List<string> { "AutoMoq" },
					detectionRank: 0,
					usingNamespaces: new List<string> { "AutoMoq", "Moq" },
					supportsGenerics: true,
					classStartCode: null,
					hasMockFields: false,
					initializeStartCode: null,
					mockFieldDeclarationCode: null,
					mockFieldInitializationCode: null,
					testCleanupCode: null,
					testArrangeCode: "var mocker = new AutoMoqer();",
					testedObjectCreationStyle: TestedObjectCreationStyle.DirectCode, 
					testedObjectCreationCode: "var $ClassNameShortLower$ = mocker.Create<$ClassName$>();",
					mockObjectReferenceCode: null),
				new MockFramework(
					name: "SimpleStubs",
					detectionReferenceMatches: new List<string> { "Etg.SimpleStubs" },
					detectionRank: 1,
					usingNamespaces: new List<string>(),
					supportsGenerics: false,
					classStartCode: null,
					hasMockFields: true,
					initializeStartCode: null,
					mockFieldDeclarationCode: "private Stub$InterfaceName$ stub$InterfaceNameBase$;",
					mockFieldInitializationCode: "this.stub$InterfaceNameBase$ = new Stub$InterfaceName$();",
					testCleanupCode: null,
					testArrangeCode: null,
					testedObjectCreationStyle: TestedObjectCreationStyle.HelperMethod, 
					testedObjectCreationCode: null,
					mockObjectReferenceCode: "this.stub$InterfaceNameBase$"),
				new MockFramework(
					name: "NSubstitute",
					detectionReferenceMatches: new List<string> { "NSubstitute" },
					detectionRank: 1,
					usingNamespaces: new List<string> { "NSubstitute" },
					supportsGenerics: true,
					classStartCode: null,
					hasMockFields: true,
					initializeStartCode: null,
					mockFieldDeclarationCode: "private $InterfaceType$ sub$InterfaceMockName$;",
					mockFieldInitializationCode: "this.sub$InterfaceMockName$ = Substitute.For<$InterfaceType$>();",
					testCleanupCode: null,
					testArrangeCode: null,
					testedObjectCreationStyle: TestedObjectCreationStyle.HelperMethod, 
					testedObjectCreationCode: null,
					mockObjectReferenceCode: "this.sub$InterfaceMockName$"),
			};
		}

		public static IList<MockFramework> List { get; }

		public static MockFramework Default => List[0];

		public static MockFramework Get(string name)
		{
			return List.First(f => f.Name == name);
		}
	}
}
