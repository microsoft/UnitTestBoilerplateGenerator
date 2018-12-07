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
		public async Task GetBoolTaskAsync_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = this.CreateClassWithMethods();
			IInterface3 interface3 = TODO;
			DateTime time = TODO;

			// Act
			var result = await unitUnderTest.GetBoolTaskAsync(
				interface3,
				time);

			// Assert
			Assert.True(false);
		}

		[Fact]
		public async Task GetBoolTaskNoAsync_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = this.CreateClassWithMethods();
			IInterface3 interface3 = TODO;
			DateTime time = TODO;

			// Act
			var result = await unitUnderTest.GetBoolTaskNoAsync(
				interface3,
				time);

			// Assert
			Assert.True(false);
		}

		[Fact]
		public async Task GetTaskNoAsync_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = this.CreateClassWithMethods();
			IInterface3 interface3 = TODO;
			DateTime time = TODO;

			// Act
			await unitUnderTest.GetTaskNoAsync(
				interface3,
				time);

			// Assert
			Assert.True(false);
		}

		[Fact]
		public void GetString_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = this.CreateClassWithMethods();

			// Act
			var result = unitUnderTest.GetString();

			// Assert
			Assert.True(false);
		}

		[Fact]
		public void GetIntMultipleSignatures_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = this.CreateClassWithMethods();
			string bla = TODO;

			// Act
			var result = unitUnderTest.GetIntMultipleSignatures(
				bla);

			// Assert
			Assert.True(false);
		}

		[Fact]
		public void GetIntMultipleSignatures_StateUnderTest_ExpectedBehavior1()
		{
			// Arrange
			var unitUnderTest = this.CreateClassWithMethods();
			IInterface4 interface4 = TODO;

			// Act
			var result = unitUnderTest.GetIntMultipleSignatures(
				interface4);

			// Assert
			Assert.True(false);
		}

		[Fact]
		public void GetOut_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = this.CreateClassWithMethods();
			bool fufu = TODO;
			int bubu = TODO;

			// Act
			var result = unitUnderTest.GetOut(
				fufu,
				out bubu);

			// Assert
			Assert.True(false);
		}

		[Fact]
		public void DoRef_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = this.CreateClassWithMethods();
			ClassWithMethods refArg = TODO;

			// Act
			unitUnderTest.DoRef(
				ref refArg);

			// Assert
			Assert.True(false);
		}

		[Fact]
		public void DoEnum_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = this.CreateClassWithMethods();
			Cucu cucuENum = TODO;

			// Act
			unitUnderTest.DoEnum(
				cucuENum);

			// Assert
			Assert.True(false);
		}

		[Fact]
		public async Task GetParams_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = this.CreateClassWithMethods();
			string[] values = TODO;

			// Act
			var result = await unitUnderTest.GetParams(
				values);

			// Assert
			Assert.True(false);
		}

		[Fact]
		public async Task GetParams2D_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = this.CreateClassWithMethods();
			DateTime[][] values = TODO;

			// Act
			var result = await unitUnderTest.GetParams2D(
				values);

			// Assert
			Assert.True(false);
		}

		[Fact]
		public async Task GetParamsClass_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = this.CreateClassWithMethods();
			ClassWithMethods[] values = TODO;

			// Act
			var result = await unitUnderTest.GetParamsClass(
				values);

			// Assert
			Assert.True(false);
		}

		[Fact]
		public async Task GetParamsClass2D_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = this.CreateClassWithMethods();
			ClassWithMethods[][] values = TODO;

			// Act
			var result = await unitUnderTest.GetParamsClass2D(
				values);

			// Assert
			Assert.True(false);
		}

		[Fact]
		public async Task GetWithClass4D_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = this.CreateClassWithMethods();
			ClassWithMethods[][][][] values = TODO;

			// Act
			var result = await unitUnderTest.GetWithClass4D(
				values);

			// Assert
			Assert.True(false);
		}

		[Fact]
		public void MethodWithNullableArgument_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = this.CreateClassWithMethods();
			int? argument = TODO;

			// Act
			var result = unitUnderTest.MethodWithNullableArgument(
				argument);

			// Assert
			Assert.True(false);
		}

		[Fact]
		public void MethodWithNamespaceQualifiedArgument_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = this.CreateClassWithMethods();
			Classes.IInterface3 myInterface = TODO;

			// Act
			var result = unitUnderTest.MethodWithNamespaceQualifiedArgument(
				myInterface);

			// Assert
			Assert.True(false);
		}
	}
}
