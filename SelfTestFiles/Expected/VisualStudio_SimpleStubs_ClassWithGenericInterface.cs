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

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange


			// Act
			ClassWithGenericInterface classWithGenericInterface = this.CreateClassWithGenericInterface();


			// Assert

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
	}
}
