using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class PropertyInjectedClassSingleTests
	{
		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var mocker = new AutoMoqer();
			var unitUnderTest = mocker.Create<PropertyInjectedClassSingle>();

			// Act

			// Assert
			Assert.Fail();
		}
	}
}
