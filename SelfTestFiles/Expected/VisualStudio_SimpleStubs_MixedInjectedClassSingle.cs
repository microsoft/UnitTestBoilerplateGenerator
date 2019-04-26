using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class MixedInjectedClassSingleTests
	{
		private StubIInterface3 stubInterface3;
		private StubISomeInterface stubSomeInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.stubInterface3 = new StubIInterface3();
			this.stubSomeInterface = new StubISomeInterface();
		}

		private MixedInjectedClassSingle CreateMixedInjectedClassSingle()
		{
			return new MixedInjectedClassSingle(
				this.stubSomeInterface)
			{
				Interface3Property = this.stubInterface3,
			};
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = this.CreateMixedInjectedClassSingle();

			// Act

			// Assert
			Assert.Fail();
		}
	}
}
