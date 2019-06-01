using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ClassWithMethodsTests
	{
		[TestMethod]
		public async Task GetBoolTaskAsync_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = new ClassWithMethods(TODO, TODO);
			IInterface3 interface3 = TODO;
			DateTime time = TODO;

			// Act
			var result = await unitUnderTest.GetBoolTaskAsync(
				interface3,
				time);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public async Task GetBoolTaskNoAsync_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = new ClassWithMethods(TODO, TODO);
			IInterface3 interface3 = TODO;
			DateTime time = TODO;

			// Act
			var result = await unitUnderTest.GetBoolTaskNoAsync(
				interface3,
				time);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public async Task GetTaskNoAsync_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = new ClassWithMethods(TODO, TODO);
			IInterface3 interface3 = TODO;
			DateTime time = TODO;

			// Act
			await unitUnderTest.GetTaskNoAsync(
				interface3,
				time);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void GetString_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = new ClassWithMethods(TODO, TODO);

			// Act
			var result = unitUnderTest.GetString();

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void GetIntMultipleSignatures_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = new ClassWithMethods(TODO, TODO);
			string bla = TODO;

			// Act
			var result = unitUnderTest.GetIntMultipleSignatures(
				bla);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void GetIntMultipleSignatures_StateUnderTest_ExpectedBehavior1()
		{
			// Arrange
			var unitUnderTest = new ClassWithMethods(TODO, TODO);
			IInterface4 interface4 = TODO;

			// Act
			var result = unitUnderTest.GetIntMultipleSignatures(
				interface4);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void GetOut_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = new ClassWithMethods(TODO, TODO);
			bool fufu = TODO;
			int bubu = TODO;

			// Act
			var result = unitUnderTest.GetOut(
				fufu,
				out bubu);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void DoRef_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = new ClassWithMethods(TODO, TODO);
			ClassWithMethods refArg = TODO;

			// Act
			unitUnderTest.DoRef(
				ref refArg);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void DoEnum_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = new ClassWithMethods(TODO, TODO);
			Cucu cucuENum = TODO;

			// Act
			unitUnderTest.DoEnum(
				cucuENum);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public async Task GetParams_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = new ClassWithMethods(TODO, TODO);
			string[] values = TODO;

			// Act
			var result = await unitUnderTest.GetParams(
				values);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public async Task GetParams2D_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = new ClassWithMethods(TODO, TODO);
			DateTime[][] values = TODO;

			// Act
			var result = await unitUnderTest.GetParams2D(
				values);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public async Task GetParamsClass_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = new ClassWithMethods(TODO, TODO);
			ClassWithMethods[] values = TODO;

			// Act
			var result = await unitUnderTest.GetParamsClass(
				values);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public async Task GetParamsClass2D_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = new ClassWithMethods(TODO, TODO);
			ClassWithMethods[][] values = TODO;

			// Act
			var result = await unitUnderTest.GetParamsClass2D(
				values);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public async Task GetWithClass4D_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = new ClassWithMethods(TODO, TODO);
			ClassWithMethods[][][][] values = TODO;

			// Act
			var result = await unitUnderTest.GetWithClass4D(
				values);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void MethodWithNullableArgument_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = new ClassWithMethods(TODO, TODO);
			int? argument = TODO;

			// Act
			var result = unitUnderTest.MethodWithNullableArgument(
				argument);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void MethodWithNamespaceQualifiedArgument_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = new ClassWithMethods(TODO, TODO);
			Classes.IInterface3 myInterface = TODO;

			// Act
			var result = unitUnderTest.MethodWithNamespaceQualifiedArgument(
				myInterface);

			// Assert
			Assert.Fail();
		}
	}
}
