using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class SomeStructTests
	{


		[TestInitialize]
		public void TestInitialize()
		{

		}

		private SomeStruct CreateSomeStruct()
		{
			return new SomeStruct(
				TODO,
				TODO);
		}

		[TestMethod]
		public void GetValue_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var someStruct = this.CreateSomeStruct();
			int c = 0;

			// Act
			var result = someStruct.GetValue(
				c);

			// Assert
			Assert.Fail();
		}
	}
}
