using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitBoilerplate.Sandbox.SimpleStubsTestCases.Reference
{
	[TestClass]
	public class ReferenceMixedMultipleTest
	{
		private StubISomeInterface stubSomeInterface;
		private StubISomeOtherInterface stubSomeOtherInterface;
		private StubIInterface3 stubInterface3;
		private StubIInterface4 stubInterface4;

		[TestInitialize]
		public void TestInitialize()
		{
			this.stubSomeInterface = new StubISomeInterface();
			this.stubSomeOtherInterface = new StubISomeOtherInterface();
			this.stubInterface3 = new StubIInterface3();
			this.stubInterface4 = new StubIInterface4();
		}

		private MixedInjectedClassMultiple CreateViewModel()
		{
			return new MixedInjectedClassMultiple(
				this.stubSomeInterface,
				this.stubSomeOtherInterface)
			{
				Interface3Property = this.stubInterface3,
				Interface4Property = this.stubInterface4,
			};
		}
	}
}
