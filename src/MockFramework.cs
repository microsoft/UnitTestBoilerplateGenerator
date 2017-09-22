using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate
{
	public class MockFramework
	{
		public MockFramework(
			string name,
			IReadOnlyList<string> detectionReferenceMatches,
			int detectionRank,
			IReadOnlyList<string> usingNamespaces, 
			bool supportsGenerics)
		{
			this.Name = name;
			this.DetectionReferenceMatches = detectionReferenceMatches;
			this.DetectionRank = detectionRank;
			this.UsingNamespaces = usingNamespaces;
			this.SupportsGenerics = supportsGenerics;
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

		public IReadOnlyList<string> UsingNamespaces { get; }

		public bool SupportsGenerics { get; }
	}
}
