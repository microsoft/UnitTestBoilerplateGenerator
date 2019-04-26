using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class PropertyInjectedClassSingleTests
	{
		private StubISomeInterface stubSomeInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.stubSomeInterface = new StubISomeInterface();
		}

		private PropertyInjectedClassSingle CreatePropertyInjectedClassSingle()
		{
			return new PropertyInjectedClassSingle
			{
				MyProperty = this.stubSomeInterface,
			};
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = this.CreatePropertyInjectedClassSingle();

			// Act

			// Assert
			Assert.Fail();
		}
	}
}
