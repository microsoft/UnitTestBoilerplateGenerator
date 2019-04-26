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

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = this.CreateNotInjectedClassEmptyCtor();

			// Act

			// Assert
			Assert.Fail();
		}
	}
}
