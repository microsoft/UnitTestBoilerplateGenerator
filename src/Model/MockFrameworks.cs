using System.Collections.Generic;
using System.Linq;

namespace UnitTestBoilerplate.Model
{
	public static class MockFrameworks
	{
		public const string NoneName = "None";
		public const string MoqName = "Moq";
		public const string AutoMoqName = "AutoMoq";
		public const string SimpleStubsName = "SimpleStubs";
		public const string NSubstituteName = "NSubstitute";
		public const string RhinoMocksName = "Rhino Mocks";
		public const string FakeItEasyName = "FakeItEasy";
		public const string JustMockName = "JustMock";

		static MockFrameworks()
		{
			List = new List<MockFramework>
			{
				new MockFramework(
					name: NoneName,
					detectionReferenceMatches: new List<string>(),
					detectionRank: 3,
					usingNamespaces: new List<string>(),
					supportsGenerics: true,
					classStartCode: null,
					hasMockFields: false,
					initializeStartCode: null,
					mockFieldDeclarationCode: null,
					mockFieldInitializationCode: null,
					testCleanupCode: null,
					testArrangeCode: null,
					testedObjectCreationStyle: TestedObjectCreationStyle.TodoStub,
					testedObjectCreationCode: null,
					mockObjectReferenceCode: null,
					assertStatement: null),
				new MockFramework(
					name: MoqName,
					detectionReferenceMatches: new List<string> { "Moq" },
					detectionRank: 2,
					usingNamespaces: new List<string> { "Moq" },
					supportsGenerics: true,
					classStartCode: "private MockRepository mockRepository;",
					hasMockFields: true,
					initializeStartCode: "this.mockRepository = new MockRepository(MockBehavior.Strict);",
					mockFieldDeclarationCode: "private Mock<$InterfaceType$> mock$InterfaceMockName$;",
					mockFieldInitializationCode: "this.mock$InterfaceMockName$ = this.mockRepository.Create<$InterfaceType$>();",
					testCleanupCode: null,
					testArrangeCode: null,
					testedObjectCreationStyle: TestedObjectCreationStyle.HelperMethod, 
					testedObjectCreationCode: null,
					mockObjectReferenceCode: "this.mock$InterfaceMockName$.Object",
					assertStatement: "this.mockRepository.VerifyAll();"),
				new MockFramework(
					name: AutoMoqName,
					detectionReferenceMatches: new List<string> { "AutoMoq" },
					detectionRank: 1,
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
					testedObjectCreationCode: "mocker.Create<$ClassName$>();",
					mockObjectReferenceCode: null,
					assertStatement: null),
				new MockFramework(
					name: SimpleStubsName,
					detectionReferenceMatches: new List<string> { "Etg.SimpleStubs" },
					detectionRank: 2,
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
					mockObjectReferenceCode: "this.stub$InterfaceNameBase$",
					assertStatement: null),
				new MockFramework(
					name: NSubstituteName,
					detectionReferenceMatches: new List<string> { "NSubstitute" },
					detectionRank: 0,
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
					mockObjectReferenceCode: "this.sub$InterfaceMockName$",
					assertStatement: null),
				new MockFramework(
					name: RhinoMocksName,
					detectionReferenceMatches: new List<string> { "Rhino.Mocks", "RhinoMocks" },
					detectionRank: 2,
					usingNamespaces: new List<string> { "Rhino.Mocks" },
					supportsGenerics: true,
					classStartCode: null,
					hasMockFields: true,
					initializeStartCode: null,
					mockFieldDeclarationCode: "private $InterfaceType$ stub$InterfaceMockName$;",
					mockFieldInitializationCode: "this.stub$InterfaceMockName$ = MockRepository.GenerateStub<$InterfaceType$>();",
					testCleanupCode: null,
					testArrangeCode: null,
					testedObjectCreationStyle: TestedObjectCreationStyle.HelperMethod,
					testedObjectCreationCode: null,
					mockObjectReferenceCode: "this.stub$InterfaceMockName$",
					assertStatement: null),
				new MockFramework(
					name: FakeItEasyName,
					detectionReferenceMatches: new List<string> { "FakeItEasy" },
					detectionRank: 2,
					usingNamespaces: new List<string> { "FakeItEasy" },
					supportsGenerics: true,
					classStartCode: null,
					hasMockFields: true,
					initializeStartCode: null,
					mockFieldDeclarationCode: "private $InterfaceType$ fake$InterfaceMockName$;",
					mockFieldInitializationCode: "this.fake$InterfaceMockName$ = A.Fake<$InterfaceType$>();",
					testCleanupCode: null,
					testArrangeCode: null,
					testedObjectCreationStyle: TestedObjectCreationStyle.HelperMethod,
					testedObjectCreationCode: null,
					mockObjectReferenceCode: "this.fake$InterfaceMockName$",
					assertStatement: null),
				new MockFramework(
					name: JustMockName,
					detectionReferenceMatches: new List<string> { "JustMock", "Telerik.JustMock" },
					detectionRank: 2,
					usingNamespaces: new List<string> { "Telerik.JustMock" },
					supportsGenerics: true,
					classStartCode: null,
					hasMockFields: true,
					initializeStartCode: null,
					mockFieldDeclarationCode: "private $InterfaceType$ mock$InterfaceMockName$;",
					mockFieldInitializationCode: "this.mock$InterfaceMockName$ = Mock.Create<$InterfaceType$>();",
					testCleanupCode: null,
					testArrangeCode: null,
					testedObjectCreationStyle: TestedObjectCreationStyle.HelperMethod,
					testedObjectCreationCode: null,
					mockObjectReferenceCode: "this.mock$InterfaceMockName$",
					assertStatement: null)
			}.AsReadOnly();
		}

		public static IList<MockFramework> List { get; }

		public static MockFramework Default => List[0];

		public static MockFramework Get(string name)
		{
			return List.First(f => f.Name == name);
		}
	}
}
