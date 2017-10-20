using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ClassWithNonInterfaceCtorParamTests
	{
		private StubSomeClass stubSomeClass;

		[TestInitialize]
		public void TestInitialize()
		{
			this.stubSomeClass = new StubSomeClass();
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange


			// Act
			ClassWithNonInterfaceCtorParam classWithNonInterfaceCtorParam = this.CreateClassWithNonInterfaceCtorParam();


			// Assert

		}

		private ClassWithNonInterfaceCtorParam CreateClassWithNonInterfaceCtorParam()
		{
			return new ClassWithNonInterfaceCtorParam(
				this.stubSomeClass);
		}
	}
}
