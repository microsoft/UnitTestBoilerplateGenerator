using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate
{
    public class TestFramework
    {
	    public TestFramework(
			string name,
			IReadOnlyList<string> detectionReferenceMatches,
			int detectionRank,
			string usingString, 
			string testClassAttribute, 
			string testMethodAttribute, 
			string testInitializeAttribute, 
			string testCleanupAttribute)
	    {
		    this.Name = name;
			this.DetectionReferenceMatches = detectionReferenceMatches;
			this.DetectionRank = detectionRank;
			this.UsingNamespace = usingString;
		    this.TestClassAttribute = testClassAttribute;
			this.TestMethodAttribute = testMethodAttribute;
			this.TestInitializeAttribute = testInitializeAttribute;
			this.TestCleanupAttribute = testCleanupAttribute;
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

		public string TestClassAttribute { get; }

		public string TestMethodAttribute { get; }

		public string TestInitializeAttribute { get; }

		public string TestCleanupAttribute { get; }
	}
}
