using Moq;
using System;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;
using Xunit;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	public class ClassWithNonInterfaceCtorParamTests : IDisposable
	{
		private MockRepository mockRepository;

		private Mock<SomeClass> mockSomeClass;

		public ClassWithNonInterfaceCtorParamTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);

			this.mockSomeClass = this.mockRepository.Create<SomeClass>();
		}

		public void Dispose()
		{
			this.mockRepository.VerifyAll();
		}

		private ClassWithNonInterfaceCtorParam CreateClassWithNonInterfaceCtorParam()
		{
			return new ClassWithNonInterfaceCtorParam(
				this.mockSomeClass.Object);
		}

		[Fact]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = this.CreateClassWithNonInterfaceCtorParam();

			// Act

			// Assert
			Assert.True(false);
		}
	}
}
