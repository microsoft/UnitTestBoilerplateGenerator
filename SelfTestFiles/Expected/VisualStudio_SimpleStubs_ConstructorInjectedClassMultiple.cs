using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ConstructorInjectedClassMultipleTests
	{
		private StubISomeInterface stubSomeInterface;
		private StubISomeOtherInterface stubSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.stubSomeInterface = new StubISomeInterface();
			this.stubSomeOtherInterface = new StubISomeOtherInterface();
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange


			// Act
			ConstructorInjectedClassMultiple constructorInjectedClassMultiple = this.CreateConstructorInjectedClassMultiple();


			// Assert

		}

		private ConstructorInjectedClassMultiple CreateConstructorInjectedClassMultiple()
		{
			return new ConstructorInjectedClassMultiple(
				this.stubSomeInterface,
				this.stubSomeOtherInterface);
		}
	}
}
