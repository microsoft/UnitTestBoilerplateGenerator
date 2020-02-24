using Microsoft.VisualStudio.TestTools.UnitTesting;
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
			var someStruct = new SomeStruct(TODO, TODO);
			int c = 0;

			// Act
			var result = someStruct.GetValue(
				c);

			// Assert
			Assert.Fail();
		}
	}
}
