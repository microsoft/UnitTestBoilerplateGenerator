using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ClassWithMethodsTests
	{
		private StubISomeInterface stubSomeInterface;
		private StubISomeOtherInterface stubSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.stubSomeInterface = new StubISomeInterface();
			this.stubSomeOtherInterface = new StubISomeOtherInterface();
		}


		private ClassWithMethods CreateClassWithMethods()
		{
			return new ClassWithMethods(
				this.stubSomeInterface,
				this.stubSomeOtherInterface);
		}

		[TestMethod]
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

		[TestMethod]
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

		[TestMethod]
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

		[TestMethod]
		public void GetString_Condition_Expectation()
		{
			// Arrange
			var unitUnderTest = CreateClassWithMethods();

			// Act
			unitUnderTest.GetString();

			// Assert
			Assert.Fail();
		}

		[TestMethod]
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

		[TestMethod]
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
