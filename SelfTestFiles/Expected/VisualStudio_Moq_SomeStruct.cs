using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class SomeStructTests
	{
		private MockRepository mockRepository;



		[TestInitialize]
		public void TestInitialize()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);


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
			this.mockRepository.VerifyAll();
		}
	}
}
