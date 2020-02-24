using Moq;
using System;
using UnitBoilerplate.Sandbox.Classes.Cases;
using Xunit;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	public class SomeStructTests
	{
		private MockRepository mockRepository;



		public SomeStructTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);


		}

		private SomeStruct CreateSomeStruct()
		{
			return new SomeStruct(
				TODO,
				TODO);
		}

		[Fact]
		public void GetValue_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var someStruct = this.CreateSomeStruct();
			int c = 0;

			// Act
			var result = someStruct.GetValue(
				c);

			// Assert
			Assert.True(false);
			this.mockRepository.VerifyAll();
		}
	}
}
