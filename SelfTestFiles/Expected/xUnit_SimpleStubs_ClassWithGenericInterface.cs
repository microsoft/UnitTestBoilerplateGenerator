using System;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;
using Xunit;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	public class ClassWithGenericInterfaceTests
	{
		private StubIInterface3 stubInterface3;
		private StubISomeInterface stubSomeInterface;

		public ClassWithGenericInterfaceTests()
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

		[Fact]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = this.CreateClassWithGenericInterface();

			// Act

			// Assert
			Assert.True(false);
		}
	}
}
