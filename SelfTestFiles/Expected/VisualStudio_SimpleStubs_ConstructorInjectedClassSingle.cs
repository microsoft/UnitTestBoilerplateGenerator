using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ConstructorInjectedClassSingleTests
	{
		private StubISomeInterface stubSomeInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.stubSomeInterface = new StubISomeInterface();
		}

		private ConstructorInjectedClassSingle CreateConstructorInjectedClassSingle()
		{
			return new ConstructorInjectedClassSingle(
				this.stubSomeInterface);
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = this.CreateConstructorInjectedClassSingle();

			// Act

			// Assert
			Assert.Fail();
		}
	}
}
