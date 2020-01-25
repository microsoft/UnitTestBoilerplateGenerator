using Moq;
using System;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;
using Xunit;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	public class DerivedPropertyInjectedClassTests
	{
		private MockRepository mockRepository;

		private Mock<IInterface3> mockInterface3;
		private Mock<ISomeInterface> mockSomeInterface;
		private Mock<ISomeOtherInterface> mockSomeOtherInterface;

		public DerivedPropertyInjectedClassTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);

			this.mockInterface3 = this.mockRepository.Create<IInterface3>();
			this.mockSomeInterface = this.mockRepository.Create<ISomeInterface>();
			this.mockSomeOtherInterface = this.mockRepository.Create<ISomeOtherInterface>();
		}

		private DerivedPropertyInjectedClass CreateDerivedPropertyInjectedClass()
		{
			return new DerivedPropertyInjectedClass
			{
				Interface3Property = this.mockInterface3.Object,
				MyProperty = this.mockSomeInterface.Object,
				Property2 = this.mockSomeOtherInterface.Object,
			};
		}

		[Fact]
		public void TestMethod1()
		{
			// Arrange
			var derivedPropertyInjectedClass = this.CreateDerivedPropertyInjectedClass();

			// Act


			// Assert
			Assert.True(false);
			this.mockRepository.VerifyAll();
		}
	}
}
