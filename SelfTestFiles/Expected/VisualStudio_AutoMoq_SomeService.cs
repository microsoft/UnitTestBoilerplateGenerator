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
			var unitUnderTest = mocker.Create<SomeService>();
			int a = TODO;
			int b = TODO;

			// Act
			var result = unitUnderTest.AddNumbers(
				a,
				b);

			// Assert
			Assert.Fail();
		}
	}
}
