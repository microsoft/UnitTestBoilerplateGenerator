using NUnit.Framework;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestFixture]
	public class ClassWithGenericInterfaceTests
	{
		private StubIInterface3 stubInterface3;
		private StubISomeInterface stubSomeInterface;

		[SetUp]
		public void SetUp()
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

		[Test]
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
