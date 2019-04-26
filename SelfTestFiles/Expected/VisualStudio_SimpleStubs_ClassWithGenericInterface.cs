using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ClassWithGenericInterfaceTests
	{
		private StubIInterface3 stubInterface3;
		private StubISomeInterface stubSomeInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.stubInterface3 = new StubIInterface3();
			this.stubSomeInterface = new StubISomeInterface();
		}

		private ClassWithGenericInterface CreateClassWithGenericInterface()
		{
			return new ClassWithGenericInterface(
				TODO,
				TODO,
				this.stubSomeInterface)
			{
				Interface2 = this.stubInterface3,
			};
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = this.CreateClassWithGenericInterface();

			// Act

			// Assert
			Assert.Fail();
		}
	}
}
