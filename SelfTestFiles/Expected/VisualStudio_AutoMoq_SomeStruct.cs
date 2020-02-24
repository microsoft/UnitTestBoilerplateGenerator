using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class SomeStructTests
	{
		[TestMethod]
		public void GetValue_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var mocker = new AutoMoqer();
			var someStruct = mocker.Create<SomeStruct>();
			int c = 0;

			// Act
			var result = someStruct.GetValue(
				c);

			// Assert
			Assert.Fail();
		}
	}
}
