using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class NotInjectedClassEmptyCtorTests
	{


		[TestInitialize]
		public void TestInitialize()
		{

		}


		private NotInjectedClassEmptyCtor CreateNotInjectedClassEmptyCtor()
		{
			return new NotInjectedClassEmptyCtor();
		}


	}
}
