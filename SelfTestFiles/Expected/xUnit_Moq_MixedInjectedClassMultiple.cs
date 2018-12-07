using Moq;
using System;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;
using Xunit;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	public class MixedInjectedClassMultipleTests : IDisposable
	{
		private MockRepository mockRepository;

		private Mock<IInterface3> mockInterface3;
		private Mock<IInterface4> mockInterface4;
		private Mock<ISomeInterface> mockSomeInterface;
		private Mock<ISomeOtherInterface> mockSomeOtherInterface;

		public MixedInjectedClassMultipleTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);

			this.mockInterface3 = this.mockRepository.Create<IInterface3>();
			this.mockInterface4 = this.mockRepository.Create<IInterface4>();
			this.mockSomeInterface = this.mockRepository.Create<ISomeInterface>();
			this.mockSomeOtherInterface = this.mockRepository.Create<ISomeOtherInterface>();
		}

		public void Dispose()
		{
			this.mockRepository.VerifyAll();
		}

		private MixedInjectedClassMultiple CreateMixedInjectedClassMultiple()
		{
			return new MixedInjectedClassMultiple(
				this.mockSomeInterface.Object,
				this.mockSomeOtherInterface.Object)
			{
				Interface3Property = this.mockInterface3.Object,
				Interface4Property = this.mockInterface4.Object,
			};
		}

		[Fact]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = this.CreateMixedInjectedClassMultiple();

			// Act

			// Assert
			Assert.True(false);
		}
	}
}
