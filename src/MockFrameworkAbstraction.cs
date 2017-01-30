using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate
{
	public static class MockFrameworkAbstraction
	{
		public static IList<string> GetUsings(MockFramework framework)
		{
			switch (framework)
			{
				case MockFramework.Unknown:
				case MockFramework.Moq:
					return new List<string> { "Moq" };
				case MockFramework.AutoMoq:
					return new List<string> { "AutoMoq", "Moq" };
				case MockFramework.SimpleStubs:
					return new List<string>();
				default:
					throw new ArgumentOutOfRangeException(nameof(framework));
			}
		}
	}
}
