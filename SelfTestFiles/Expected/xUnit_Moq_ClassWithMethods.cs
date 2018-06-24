using Moq;
using System;
using System.Threading.Tasks;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;
using Xunit;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	public class ClassWithMethodsTests : IDisposable
	{
		private MockRepository mockRepository;

		private Mock<ISomeInterface> mockSomeInterface;
		private Mock<ISomeOtherInterface> mockSomeOtherInterface;

		public ClassWithMethodsTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);

			this.mockSomeInterface = this.mockRepository.Create<ISomeInterface>();
			this.mockSomeOtherInterface = this.mockRepository.Create<ISomeOtherInterface>();
		}

		public void Dispose()
		{
			this.mockRepository.VerifyAll();
		}

		private ClassWithMethods CreateClassWithMethods()
		{
			return new ClassWithMethods(
				this.mockSomeInterface.Object,
				this.mockSomeOtherInterface.Object);
		}

		[Fact]
		public async Task GetBoolTaskAsync_Condition_Expectation()
		{
			// Arrange
			var unitUnderTest = CreateClassWithMethods();

			// Act
			await unitUnderTest.GetBoolTaskAsync(
				default(IInterface3),
				default(DateTime));

			// Assert
			Assert.Fail();
		}

		[Fact]
		public async Task GetBoolTaskNoAsync_Condition_Expectation()
		{
			// Arrange
			var unitUnderTest = CreateClassWithMethods();

			// Act
			await unitUnderTest.GetBoolTaskNoAsync(
				default(IInterface3),
				default(DateTime));

			// Assert
			Assert.Fail();
		}

		[Fact]
		public async Task GetTaskNoAsync_Condition_Expectation()
		{
			// Arrange
			var unitUnderTest = CreateClassWithMethods();

			// Act
			await unitUnderTest.GetTaskNoAsync(
				default(IInterface3),
				default(DateTime));

			// Assert
			Assert.Fail();
		}

		[Fact]
		public void GetString_Condition_Expectation()
		{
			// Arrange
			var unitUnderTest = CreateClassWithMethods();

			// Act
			unitUnderTest.GetString();

			// Assert
			Assert.Fail();
		}

		[Fact]
		public void GetIntMultipleSignatures_Condition_Expectation()
		{
			// Arrange
			var unitUnderTest = CreateClassWithMethods();

			// Act
			unitUnderTest.GetIntMultipleSignatures(
				default(string));

			// Assert
			Assert.Fail();
		}

		[Fact]
		public void GetIntMultipleSignatures1_Condition_Expectation()
		{
			// Arrange
			var unitUnderTest = CreateClassWithMethods();

			// Act
			unitUnderTest.GetIntMultipleSignatures(
				default(IInterface4));

			// Assert
			Assert.Fail();
		}

	}
}
