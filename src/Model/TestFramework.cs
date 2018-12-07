using System.Collections.Generic;

namespace UnitTestBoilerplate.Model
{
    public class TestFramework
    {
	    public TestFramework(string name)
	    {
		    this.Name = name;
	    }

	    public TestFramework(
			string name,
			IReadOnlyList<string> detectionReferenceMatches,
			int detectionRank,
			string usingString, 
			string testClassAttribute, 
			string testMethodAttribute,
			TestInitializeStyle testInitializeStyle,
			string testInitializeAttribute, 
			TestCleanupStyle testCleanupStyle,
			string testCleanupAttribute,
			string assertFailStatement)
	    {
		    this.Name = name;
			this.DetectionReferenceMatches = detectionReferenceMatches;
			this.DetectionRank = detectionRank;
			this.UsingNamespace = usingString;
		    this.TestClassAttribute = testClassAttribute;
			this.TestMethodAttribute = testMethodAttribute;
			this.TestInitializeStyle = testInitializeStyle;
			this.TestInitializeAttribute = testInitializeAttribute;
			this.TestCleanupStyle = testCleanupStyle;
			this.TestCleanupAttribute = testCleanupAttribute;
		    this.AssertFailStatement = assertFailStatement;
	    }

	    public string Name { get; }

		/// <summary>
		/// Gets the list of strings to match against when detecting references to this framework.
		/// </summary>
		public IReadOnlyList<string> DetectionReferenceMatches { get; }

		/// <summary>
		/// Gets the detection ranking for this framework. If references to multiple frameworks are found, frameworks with a lower rank will win.
		/// </summary>
		public int DetectionRank { get; }

		public string UsingNamespace { get; }

		/// <summary>
		/// The attribute to apply to the test class, or null if the test framework does not use a test class attribute.
		/// </summary>
		public string TestClassAttribute { get; }

		public string TestMethodAttribute { get; }

		public TestInitializeStyle TestInitializeStyle { get; }

		public string TestInitializeAttribute { get; }

		public TestCleanupStyle TestCleanupStyle { get; }

		public string TestCleanupAttribute { get; }

		public string AssertFailStatement { get; }

	    public override string ToString()
	    {
		    return this.Name;
	    }
    }
}
