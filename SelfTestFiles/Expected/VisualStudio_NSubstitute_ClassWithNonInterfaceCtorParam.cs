using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ClassWithNonInterfaceCtorParamTests
	{
		private SomeClass subSomeClass;

		[TestInitialize]
		public void TestInitialize()
		{
			this.subSomeClass = Substitute.For<SomeClass>();
		}

		private ClassWithNonInterfaceCtorParam CreateClassWithNonInterfaceCtorParam()
		{
			return new ClassWithNonInterfaceCtorParam(
				this.subSomeClass);
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
