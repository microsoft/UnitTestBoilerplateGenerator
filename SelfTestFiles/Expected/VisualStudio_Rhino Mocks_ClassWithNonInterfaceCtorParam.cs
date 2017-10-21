using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ClassWithNonInterfaceCtorParamTests
	{
		private SomeClass stubSomeClass;

		[TestInitialize]
		public void TestInitialize()
		{
			this.stubSomeClass = MockRepository.GenerateStub<SomeClass>();
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
