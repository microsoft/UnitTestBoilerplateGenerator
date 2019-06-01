using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class NotInjectedClassEmptyCtorTests
	{
		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = new NotInjectedClassEmptyCtor();

			// Act

			// Assert
			Assert.Fail();
		}
	}
}
