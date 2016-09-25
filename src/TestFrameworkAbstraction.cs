using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate
{
	public static class TestFrameworkAbstraction
	{
		private static readonly Dictionary<TestFramework, string> UsingStrings = new Dictionary<TestFramework, string>
		{
			{ TestFramework.VisualStudio, "Microsoft.VisualStudio.TestTools.UnitTesting" },
			{ TestFramework.NUnit, "NUnit.Framework" },
		};

		private static readonly Dictionary<TestFramework, string> TestClassStrings = new Dictionary<TestFramework, string>
		{
			{ TestFramework.VisualStudio, "TestClass" },
			{ TestFramework.NUnit, "TestFixture" },
		};

		private static readonly Dictionary<TestFramework, string> TestMethodStrings = new Dictionary<TestFramework, string>
		{
			{ TestFramework.VisualStudio, "TestMethod" },
			{ TestFramework.NUnit, "Test" },
		};

		private static readonly Dictionary<TestFramework, string> TestInitializeStrings = new Dictionary<TestFramework, string>
		{
			{ TestFramework.VisualStudio, "TestInitialize" },
			{ TestFramework.NUnit, "SetUp" },
		};

		private static readonly Dictionary<TestFramework, string> TestCleanupStrings = new Dictionary<TestFramework, string>
		{
			{ TestFramework.VisualStudio, "TestCleanup" },
			{ TestFramework.NUnit, "TearDown" },
		};

		public static string GetUsing(TestFramework framework)
		{
			return GetStringFromDictionary(framework, UsingStrings);
		}

		public static string GetTestClassAttribute(TestFramework framework)
		{
			return GetStringFromDictionary(framework, TestClassStrings);
		}

		public static string GetTestMethodAttribute(TestFramework framework)
		{
			return GetStringFromDictionary(framework, TestMethodStrings);
		}

		public static string GetTestInitializeAttribute(TestFramework framework)
		{
			return GetStringFromDictionary(framework, TestInitializeStrings);
		}

		public static string GetTestCleanupAttribute(TestFramework framework)
		{
			return GetStringFromDictionary(framework, TestCleanupStrings);
		}

		private static string GetStringFromDictionary(TestFramework framework, Dictionary<TestFramework, string> dictionary)
		{
			if (framework == TestFramework.Unknown)
			{
				framework = TestFramework.VisualStudio;
			}

			string value;
			if (dictionary.TryGetValue(framework, out value))
			{
				return value;
			}

			throw new ArgumentOutOfRangeException(nameof(framework), "Value could not be found for framework " + framework);
		}
	}
}
