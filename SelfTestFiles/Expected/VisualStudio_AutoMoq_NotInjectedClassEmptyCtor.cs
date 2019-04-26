using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
			var mocker = new AutoMoqer();
			var unitUnderTest = mocker.Create<NotInjectedClassEmptyCtor>();

			// Act

			// Assert
			Assert.Fail();
		}
	}
}
