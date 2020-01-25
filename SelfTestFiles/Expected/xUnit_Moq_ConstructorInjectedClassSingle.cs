using Moq;
using System;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;
using Xunit;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	public class ConstructorInjectedClassSingleTests
	{
		private MockRepository mockRepository;

		private Mock<ISomeInterface> mockSomeInterface;

		public ConstructorInjectedClassSingleTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);

			this.mockSomeInterface = this.mockRepository.Create<ISomeInterface>();
		}

		private ConstructorInjectedClassSingle CreateConstructorInjectedClassSingle()
		{
			return new ConstructorInjectedClassSingle(
				this.mockSomeInterface.Object);
		}

		[Fact]
		public void TestMethod1()
		{
			// Arrange
			var constructorInjectedClassSingle = this.CreateConstructorInjectedClassSingle();

			// Act


			// Assert
			Assert.True(false);
			this.mockRepository.VerifyAll();
		}
	}
}
