using Moq;
using System;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;
using Xunit;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	public class ConstructorInjectedClassSingleTests : IDisposable
	{
		private MockRepository mockRepository;

		private Mock<ISomeInterface> mockSomeInterface;

		public ConstructorInjectedClassSingleTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);

			this.mockSomeInterface = this.mockRepository.Create<ISomeInterface>();
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
			ConstructorInjectedClassSingle constructorInjectedClassSingle = this.CreateConstructorInjectedClassSingle();


			// Assert

		}

		private ConstructorInjectedClassSingle CreateConstructorInjectedClassSingle()
		{
			return new ConstructorInjectedClassSingle(
				this.mockSomeInterface.Object);
		}
	}
}
