using Moq;
using System;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;
using Xunit;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	public class PropertyInjectedClassSingleTests
	{
		private MockRepository mockRepository;

		private Mock<ISomeInterface> mockSomeInterface;

		public PropertyInjectedClassSingleTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);

			this.mockSomeInterface = this.mockRepository.Create<ISomeInterface>();
		}

		private PropertyInjectedClassSingle CreatePropertyInjectedClassSingle()
		{
			return new PropertyInjectedClassSingle
			{
				MyProperty = this.mockSomeInterface.Object,
			};
		}

		[Fact]
		public void TestMethod1()
		{
			// Arrange
			var propertyInjectedClassSingle = this.CreatePropertyInjectedClassSingle();

			// Act


			// Assert
			Assert.True(false);
			this.mockRepository.VerifyAll();
		}
	}
}
