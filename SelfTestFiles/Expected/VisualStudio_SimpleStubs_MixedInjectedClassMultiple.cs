using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class MixedInjectedClassMultipleTests
	{
		private StubIInterface3 stubInterface3;
		private StubIInterface4 stubInterface4;
		private StubISomeInterface stubSomeInterface;
		private StubISomeOtherInterface stubSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.stubInterface3 = new StubIInterface3();
			this.stubInterface4 = new StubIInterface4();
			this.stubSomeInterface = new StubISomeInterface();
			this.stubSomeOtherInterface = new StubISomeOtherInterface();
		}

		private MixedInjectedClassMultiple CreateMixedInjectedClassMultiple()
		{
			return new MixedInjectedClassMultiple(
				this.stubSomeInterface,
				this.stubSomeOtherInterface)
			{
				Interface3Property = this.stubInterface3,
				Interface4Property = this.stubInterface4,
			};
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = this.CreateMixedInjectedClassMultiple();

			// Act

			// Assert
			Assert.Fail();
		}
	}
}
