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


		private ConstructorInjectedClassMultiple CreateConstructorInjectedClassMultiple()
		{
			return new ConstructorInjectedClassMultiple(
				this.stubSomeInterface,
				this.stubSomeOtherInterface);
		}


	}
}
