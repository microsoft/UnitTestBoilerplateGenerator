using System.Collections.Generic;

namespace UnitTestBoilerplate.BasicModels
{
	/// <summary>
	/// Defines a mock framework.
	/// </summary>
	/// <remarks>The "Code" properties can have placeholders for formatting, and can be later customized by the user.</remarks>
	public class MockFramework
	{
		public MockFramework(
			string name,
			IReadOnlyList<string> detectionReferenceMatches,
			int detectionRank,
			IReadOnlyList<string> usingNamespaces, 
			bool supportsGenerics,
			string classStartCode,
			bool hasMockFields,
			string initializeStartCode,
			string mockFieldDeclarationCode,
			string mockFieldInitializationCode,
			string testCleanupCode,
			string testArrangeCode,
			TestedObjectCreationStyle testedObjectCreationStyle,
			string testedObjectCreationCode,
			string mockObjectReferenceCode)
		{
			this.Name = name;
			this.DetectionReferenceMatches = detectionReferenceMatches;
			this.DetectionRank = detectionRank;
			this.UsingNamespaces = usingNamespaces;
			this.SupportsGenerics = supportsGenerics;
			this.ClassStartCode = classStartCode;
			this.HasMockFields = hasMockFields;
			this.InitializeStartCode = initializeStartCode;
			this.MockFieldDeclarationCode = mockFieldDeclarationCode;
			this.MockFieldInitializationCode = mockFieldInitializationCode;
			this.TestCleanupCode = testCleanupCode;
			this.TestArrangeCode = testArrangeCode;
			this.TestedObjectCreationStyle = testedObjectCreationStyle;
			this.TestedObjectCreationCode = testedObjectCreationCode;
			this.MockObjectReferenceCode = mockObjectReferenceCode;
		}

		public string Name { get; }

		/// <summary>
		/// The list of strings to match against when detecting references to this framework.
		/// </summary>
		public IReadOnlyList<string> DetectionReferenceMatches { get; }

		/// <summary>
		/// The detection ranking for this framework. If references to multiple frameworks are found, frameworks with a lower rank will win.
		/// </summary>
		public int DetectionRank { get; }

		public IReadOnlyList<string> UsingNamespaces { get; }

		public bool SupportsGenerics { get; }

		/// <summary>
		/// Code to put at the start of the class.
		/// </summary>
		public string ClassStartCode { get; }

		/// <summary>
		/// True if the framework approach needs mock fields on the test class.
		/// </summary>
		/// <remarks>AutoMoq, for example does not need mock fields since all the mocks are created dynamically.</remarks>
		public bool HasMockFields { get; }

		/// <summary>
		/// Code to put at the start of the test initialize method.
		/// </summary>
		/// <remarks>Only applicable if HasMockFields = true.</remarks>
		public string InitializeStartCode { get; }

		/// <summary>
		/// Code for declaring a mock field.
		/// </summary>
		/// <remarks>Only applicable if HasMockFields = true.</remarks>
		public string MockFieldDeclarationCode { get; }

		/// <summary>
		/// Code for initializing a mock field.
		/// </summary>
		/// <remarks>Only applicable if HasMockFields = true.</remarks>
		public string MockFieldInitializationCode { get; }

		/// <summary>
		/// Whether the mock framework has test cleanup.
		/// </summary>
		public bool HasTestCleanup => !string.IsNullOrEmpty(this.TestCleanupCode);

		/// <summary>
		/// Code for test cleanup, or null if there is no test cleanup step.
		/// </summary>
		public string TestCleanupCode { get; }

		/// <summary>
		/// Code to put in the arrange portion of a test.
		/// </summary>
		public string TestArrangeCode { get; }

		/// <summary>
		/// How the tested object is created.
		/// </summary>
		public TestedObjectCreationStyle TestedObjectCreationStyle { get; }

		/// <summary>
		/// Code for the tested object creation.
		/// </summary>
		/// <remarks>Only applicable for TestedObjectCreationStyle = DirectCode</remarks>
		public string TestedObjectCreationCode { get; }

		/// <summary>
		/// Code referring to the mocked object.
		/// </summary>
		public string MockObjectReferenceCode { get; }
	}
}
