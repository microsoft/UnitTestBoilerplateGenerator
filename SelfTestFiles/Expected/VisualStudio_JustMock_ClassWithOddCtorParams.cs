using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ClassWithOddCtorParamsTests
	{


		[TestInitialize]
		public void TestInitialize()
		{

		}

		private ClassWithOddCtorParams CreateClassWithOddCtorParams()
		{
			return new ClassWithOddCtorParams(
				TODO,
				TODO);
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var classWithOddCtorParams = this.CreateClassWithOddCtorParams();

			// Act


			// Assert
			Assert.Fail();
		}
	}
}
