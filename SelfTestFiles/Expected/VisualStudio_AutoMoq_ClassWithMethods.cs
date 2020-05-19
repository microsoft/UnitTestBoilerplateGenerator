using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
		public void AttributedMethod_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var mocker = new AutoMoqer();
			var classWithMethods = mocker.Create<ClassWithMethods>();
			string test = null;

			// Act
			classWithMethods.AttributedMethod(
				test);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void ValueTypeParams_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var mocker = new AutoMoqer();
			var classWithMethods = mocker.Create<ClassWithMethods>();
			bool boolParam = false;
			byte byteParam = 0;
			sbyte sbyteParam = 0;
			short shortParam = 0;
			ushort ushortParam = 0;
			int intParam = 0;
			uint uintParam = 0;
			long longParam = 0;
			ulong ulongParam = 0;
			float floatParam = 0;
			double doubleParam = 0;
			char charParam = default(global::System.Char);
			decimal decimalParam = 0;

			// Act
			classWithMethods.ValueTypeParams(
				boolParam,
				byteParam,
				sbyteParam,
				shortParam,
				ushortParam,
				intParam,
				uintParam,
				longParam,
				ulongParam,
				floatParam,
				doubleParam,
				charParam,
				decimalParam);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public async Task GetBoolTaskAsync_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var mocker = new AutoMoqer();
			var classWithMethods = mocker.Create<ClassWithMethods>();
			IInterface3 interface3 = null;
			DateTime time = default(global::System.DateTime);

			// Act
			var result = await classWithMethods.GetBoolTaskAsync(
				interface3,
				time);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public async Task GetBoolTaskNoAsync_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var mocker = new AutoMoqer();
			var classWithMethods = mocker.Create<ClassWithMethods>();
			IInterface3 interface3 = null;
			DateTime time = default(global::System.DateTime);

			// Act
			var result = await classWithMethods.GetBoolTaskNoAsync(
				interface3,
				time);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public async Task GetTaskNoAsync_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var mocker = new AutoMoqer();
			var classWithMethods = mocker.Create<ClassWithMethods>();
			IInterface3 interface3 = null;
			DateTime time = default(global::System.DateTime);

			// Act
			await classWithMethods.GetTaskNoAsync(
				interface3,
				time);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void GetString_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var mocker = new AutoMoqer();
			var classWithMethods = mocker.Create<ClassWithMethods>();

			// Act
			var result = classWithMethods.GetString();

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void GetIntMultipleSignatures_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var mocker = new AutoMoqer();
			var classWithMethods = mocker.Create<ClassWithMethods>();
			string bla = null;

			// Act
			var result = classWithMethods.GetIntMultipleSignatures(
				bla);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void GetIntMultipleSignatures_StateUnderTest_ExpectedBehavior1()
		{
			// Arrange
			var mocker = new AutoMoqer();
			var classWithMethods = mocker.Create<ClassWithMethods>();
			IInterface4 interface4 = null;

			// Act
			var result = classWithMethods.GetIntMultipleSignatures(
				interface4);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void GetOut_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var mocker = new AutoMoqer();
			var classWithMethods = mocker.Create<ClassWithMethods>();
			bool fufu = false;
			int bubu = 0;

			// Act
			var result = classWithMethods.GetOut(
				fufu,
				out bubu);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void DoRef_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var mocker = new AutoMoqer();
			var classWithMethods = mocker.Create<ClassWithMethods>();
			ClassWithMethods refArg = null;

			// Act
			classWithMethods.DoRef(
				ref refArg);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void DoEnum_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var mocker = new AutoMoqer();
			var classWithMethods = mocker.Create<ClassWithMethods>();
			Cucu cucuENum = default(global::UnitBoilerplate.Sandbox.Classes.Cases.Cucu);

			// Act
			classWithMethods.DoEnum(
				cucuENum);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public async Task GetParams_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var mocker = new AutoMoqer();
			var classWithMethods = mocker.Create<ClassWithMethods>();
			string[] values = null;

			// Act
			var result = await classWithMethods.GetParams(
				values);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public async Task GetParams2D_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var mocker = new AutoMoqer();
			var classWithMethods = mocker.Create<ClassWithMethods>();
			DateTime[][] values = null;

			// Act
			var result = await classWithMethods.GetParams2D(
				values);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public async Task GetParamsClass_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var mocker = new AutoMoqer();
			var classWithMethods = mocker.Create<ClassWithMethods>();
			ClassWithMethods[] values = null;

			// Act
			var result = await classWithMethods.GetParamsClass(
				values);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public async Task GetParamsClass2D_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var mocker = new AutoMoqer();
			var classWithMethods = mocker.Create<ClassWithMethods>();
			ClassWithMethods[][] values = null;

			// Act
			var result = await classWithMethods.GetParamsClass2D(
				values);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public async Task GetWithClass4D_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var mocker = new AutoMoqer();
			var classWithMethods = mocker.Create<ClassWithMethods>();
			ClassWithMethods[][][][] values = null;

			// Act
			var result = await classWithMethods.GetWithClass4D(
				values);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void MethodWithNullableArgument_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var mocker = new AutoMoqer();
			var classWithMethods = mocker.Create<ClassWithMethods>();
			int? argument = null;

			// Act
			var result = classWithMethods.MethodWithNullableArgument(
				argument);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void MethodWithNamespaceQualifiedArgument_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var mocker = new AutoMoqer();
			var classWithMethods = mocker.Create<ClassWithMethods>();
			IInterface3 myInterface = null;

			// Act
			var result = classWithMethods.MethodWithNamespaceQualifiedArgument(
				myInterface);

			// Assert
			Assert.Fail();
		}
	}
}
