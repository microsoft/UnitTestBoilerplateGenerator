using Microsoft.VisualStudio.TestTools.UnitTesting;
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

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange


			// Act
			NotInjectedClassEmptyCtor notInjectedClassEmptyCtor = this.CreateNotInjectedClassEmptyCtor();


			// Assert

		}

		private NotInjectedClassEmptyCtor CreateNotInjectedClassEmptyCtor()
		{
			return new NotInjectedClassEmptyCtor();
		}
	}
}
