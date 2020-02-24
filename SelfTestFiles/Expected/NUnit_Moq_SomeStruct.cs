using Moq;
using NUnit.Framework;
using System;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestFixture]
	public class SomeStructTests
	{
		private MockRepository mockRepository;



		[SetUp]
		public void SetUp()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);


		}

		private SomeStruct CreateSomeStruct()
		{
			return new SomeStruct(
				TODO,
				TODO);
		}

		[Test]
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
