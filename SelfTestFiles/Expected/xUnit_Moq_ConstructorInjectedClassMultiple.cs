using Moq;
using System;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;
using Xunit;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	public class ConstructorInjectedClassMultipleTests : IDisposable
	{
		private MockRepository mockRepository;

		private Mock<ISomeInterface> mockSomeInterface;
		private Mock<ISomeOtherInterface> mockSomeOtherInterface;

		public ConstructorInjectedClassMultipleTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);

			this.mockSomeInterface = this.mockRepository.Create<ISomeInterface>();
			this.mockSomeOtherInterface = this.mockRepository.Create<ISomeOtherInterface>();
		}

		public void Dispose()
		{
			this.mockRepository.VerifyAll();
		}

		[Fact]
		public void TestMethod1()
		{
			// Arrange


			// Act
			ConstructorInjectedClassMultiple constructorInjectedClassMultiple = this.CreateConstructorInjectedClassMultiple();


			// Assert

		}

		private ConstructorInjectedClassMultiple CreateConstructorInjectedClassMultiple()
		{
			return new ConstructorInjectedClassMultiple(
				this.mockSomeInterface.Object,
				this.mockSomeOtherInterface.Object);
		}
	}
}
