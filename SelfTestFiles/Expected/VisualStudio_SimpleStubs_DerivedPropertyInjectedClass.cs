using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class DerivedPropertyInjectedClassTests
	{
		private StubIInterface3 stubInterface3;
		private StubISomeInterface stubSomeInterface;
		private StubISomeOtherInterface stubSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.stubInterface3 = new StubIInterface3();
			this.stubSomeInterface = new StubISomeInterface();
			this.stubSomeOtherInterface = new StubISomeOtherInterface();
		}

		private DerivedPropertyInjectedClass CreateDerivedPropertyInjectedClass()
		{
			return new DerivedPropertyInjectedClass
			{
				Interface3Property = this.stubInterface3,
				MyProperty = this.stubSomeInterface,
				Property2 = this.stubSomeOtherInterface,
			};
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = this.CreateDerivedPropertyInjectedClass();

			// Act

			// Assert
			Assert.Fail();
		}
	}
}
