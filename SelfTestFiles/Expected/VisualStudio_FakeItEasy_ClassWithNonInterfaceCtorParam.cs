using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ClassWithNonInterfaceCtorParamTests
	{
		private SomeClass fakeSomeClass;

		[TestInitialize]
		public void TestInitialize()
		{
			this.fakeSomeClass = A.Fake<SomeClass>();
		}

		private ClassWithNonInterfaceCtorParam CreateClassWithNonInterfaceCtorParam()
		{
			return new ClassWithNonInterfaceCtorParam(
				this.fakeSomeClass);
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = this.CreateClassWithNonInterfaceCtorParam();

			// Act

			// Assert
			Assert.Fail();
		}
	}
}
