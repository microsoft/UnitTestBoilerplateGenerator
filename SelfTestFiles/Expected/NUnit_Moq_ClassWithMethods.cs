using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestFixture]
	public class ClassWithMethodsTests
	{
		private MockRepository mockRepository;

		private Mock<ISomeInterface> mockSomeInterface;
		private Mock<ISomeOtherInterface> mockSomeOtherInterface;

		[SetUp]
		public void SetUp()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);

			this.mockSomeInterface = this.mockRepository.Create<ISomeInterface>();
			this.mockSomeOtherInterface = this.mockRepository.Create<ISomeOtherInterface>();
		}

		[TearDown]
		public void TearDown()
		{
			this.mockRepository.VerifyAll();
		}

		private ClassWithMethods CreateClassWithMethods()
		{
			return new ClassWithMethods(
				this.mockSomeInterface.Object,
				this.mockSomeOtherInterface.Object);
		}

		[Test]
		public async Task GetBoolTaskAsync_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var classWithMethods = this.CreateClassWithMethods();
			IInterface3 interface3 = null;
			DateTime time = default(global::System.DateTime);

			// Act
			var result = await classWithMethods.GetBoolTaskAsync(
				interface3,
				time);

			// Assert
			Assert.Fail();
		}

		[Test]
		public async Task GetBoolTaskNoAsync_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var classWithMethods = this.CreateClassWithMethods();
			IInterface3 interface3 = null;
			DateTime time = default(global::System.DateTime);

			// Act
			var result = await classWithMethods.GetBoolTaskNoAsync(
				interface3,
				time);

			// Assert
			Assert.Fail();
		}

		[Test]
		public async Task GetTaskNoAsync_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var classWithMethods = this.CreateClassWithMethods();
			IInterface3 interface3 = null;
			DateTime time = default(global::System.DateTime);

			// Act
			await classWithMethods.GetTaskNoAsync(
				interface3,
				time);

			// Assert
			Assert.Fail();
		}

		[Test]
		public void GetString_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var classWithMethods = this.CreateClassWithMethods();

			// Act
			var result = classWithMethods.GetString();

			// Assert
			Assert.Fail();
		}

		[Test]
		public void GetIntMultipleSignatures_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var classWithMethods = this.CreateClassWithMethods();
			string bla = null;

			// Act
			var result = classWithMethods.GetIntMultipleSignatures(
				bla);

			// Assert
			Assert.Fail();
		}

		[Test]
		public void GetIntMultipleSignatures_StateUnderTest_ExpectedBehavior1()
		{
			// Arrange
			var classWithMethods = this.CreateClassWithMethods();
			IInterface4 interface4 = null;

			// Act
			var result = classWithMethods.GetIntMultipleSignatures(
				interface4);

			// Assert
			Assert.Fail();
		}

		[Test]
		public void GetOut_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var classWithMethods = this.CreateClassWithMethods();
			bool fufu = false;
			int bubu = 0;

			// Act
			var result = classWithMethods.GetOut(
				fufu,
				out bubu);

			// Assert
			Assert.Fail();
		}

		[Test]
		public void DoRef_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var classWithMethods = this.CreateClassWithMethods();
			ClassWithMethods refArg = null;

			// Act
			classWithMethods.DoRef(
				ref refArg);

			// Assert
			Assert.Fail();
		}

		[Test]
		public void DoEnum_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var classWithMethods = this.CreateClassWithMethods();
			Cucu cucuENum = default(global::UnitBoilerplate.Sandbox.Classes.Cases.Cucu);

			// Act
			classWithMethods.DoEnum(
				cucuENum);

			// Assert
			Assert.Fail();
		}

		[Test]
		public async Task GetParams_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var classWithMethods = this.CreateClassWithMethods();
			string[] values = null;

			// Act
			var result = await classWithMethods.GetParams(
				values);

			// Assert
			Assert.Fail();
		}

		[Test]
		public async Task GetParams2D_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var classWithMethods = this.CreateClassWithMethods();
			DateTime[][] values = null;

			// Act
			var result = await classWithMethods.GetParams2D(
				values);

			// Assert
			Assert.Fail();
		}

		[Test]
		public async Task GetParamsClass_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var classWithMethods = this.CreateClassWithMethods();
			ClassWithMethods[] values = null;

			// Act
			var result = await classWithMethods.GetParamsClass(
				values);

			// Assert
			Assert.Fail();
		}

		[Test]
		public async Task GetParamsClass2D_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var classWithMethods = this.CreateClassWithMethods();
			ClassWithMethods[][] values = null;

			// Act
			var result = await classWithMethods.GetParamsClass2D(
				values);

			// Assert
			Assert.Fail();
		}

		[Test]
		public async Task GetWithClass4D_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var classWithMethods = this.CreateClassWithMethods();
			ClassWithMethods[][][][] values = null;

			// Act
			var result = await classWithMethods.GetWithClass4D(
				values);

			// Assert
			Assert.Fail();
		}

		[Test]
		public void MethodWithNullableArgument_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var classWithMethods = this.CreateClassWithMethods();
			int? argument = null;

			// Act
			var result = classWithMethods.MethodWithNullableArgument(
				argument);

			// Assert
			Assert.Fail();
		}

		[Test]
		public void MethodWithNamespaceQualifiedArgument_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var classWithMethods = this.CreateClassWithMethods();
			IInterface3 myInterface = null;

			// Act
			var result = classWithMethods.MethodWithNamespaceQualifiedArgument(
				myInterface);

			// Assert
			Assert.Fail();
		}
	}
}
