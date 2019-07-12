using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class SomeServiceTests
	{
		[TestMethod]
		public void AddNumbers_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var mocker = new AutoMoqer();
			var service = mocker.Create<SomeService>();
			int a = 0;
			int b = 0;

			// Act
			var result = service.AddNumbers(
				a,
				b);

			// Assert
			Assert.Fail();
		}
	}
}
