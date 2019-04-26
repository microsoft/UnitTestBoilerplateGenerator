using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class NotInjectedClassTests
	{


		[TestInitialize]
		public void TestInitialize()
		{

		}

		private NotInjectedClass CreateNotInjectedClass()
		{
			return new NotInjectedClass();
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = this.CreateNotInjectedClass();

			// Act

			// Assert
			Assert.Fail();
		}
	}
}
