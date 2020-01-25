using Moq;
using System;
using UnitBoilerplate.Sandbox.Classes.Cases;
using Xunit;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	public class ClassWithOddCtorParamsTests
	{
		private MockRepository mockRepository;



		public ClassWithOddCtorParamsTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);


		}

		private ClassWithOddCtorParams CreateClassWithOddCtorParams()
		{
			return new ClassWithOddCtorParams(
				TODO,
				TODO);
		}

		[Fact]
		public void TestMethod1()
		{
			// Arrange
			var classWithOddCtorParams = this.CreateClassWithOddCtorParams();

			// Act


			// Assert
			Assert.True(false);
			this.mockRepository.VerifyAll();
		}
	}
}
