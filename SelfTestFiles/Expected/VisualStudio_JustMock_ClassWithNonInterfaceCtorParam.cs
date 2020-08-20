using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ClassWithNonInterfaceCtorParamTests
	{
		private SomeClass mockSomeClass;

		[TestInitialize]
		public void TestInitialize()
		{
			this.mockSomeClass = Mock.Create<SomeClass>();
		}

		private ClassWithNonInterfaceCtorParam CreateClassWithNonInterfaceCtorParam()
		{
			return new ClassWithNonInterfaceCtorParam(
				this.mockSomeClass);
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var classWithNonInterfaceCtorParam = this.CreateClassWithNonInterfaceCtorParam();

			// Act


			// Assert
			Assert.Fail();
		}
	}
}
