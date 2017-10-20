using Moq;
using System;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;
using Xunit;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	public class PropertyInjectedClassMultipleTests : IDisposable
	{
		private MockRepository mockRepository;

		private Mock<ISomeInterface> mockSomeInterface;
		private Mock<ISomeOtherInterface> mockSomeOtherInterface;

		public PropertyInjectedClassMultipleTests()
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
			PropertyInjectedClassMultiple propertyInjectedClassMultiple = this.CreatePropertyInjectedClassMultiple();


			// Assert

		}

		private PropertyInjectedClassMultiple CreatePropertyInjectedClassMultiple()
		{
			return new PropertyInjectedClassMultiple
			{
				MyProperty = this.mockSomeInterface.Object,
				Property2 = this.mockSomeOtherInterface.Object,
			};
		}
	}
}
