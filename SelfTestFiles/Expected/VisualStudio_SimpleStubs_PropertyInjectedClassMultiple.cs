using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class PropertyInjectedClassMultipleTests
	{
		private StubISomeInterface stubSomeInterface;
		private StubISomeOtherInterface stubSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.stubSomeInterface = new StubISomeInterface();
			this.stubSomeOtherInterface = new StubISomeOtherInterface();
		}

		private PropertyInjectedClassMultiple CreatePropertyInjectedClassMultiple()
		{
			return new PropertyInjectedClassMultiple
			{
				MyProperty = this.stubSomeInterface,
				Property2 = this.stubSomeOtherInterface,
			};
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = this.CreatePropertyInjectedClassMultiple();

			// Act

			// Assert
			Assert.Fail();
		}
	}
}
