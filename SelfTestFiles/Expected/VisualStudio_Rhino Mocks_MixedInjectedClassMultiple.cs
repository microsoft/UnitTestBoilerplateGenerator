using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class MixedInjectedClassMultipleTests
	{
		private IInterface3 stubInterface3;
		private IInterface4 stubInterface4;
		private ISomeInterface stubSomeInterface;
		private ISomeOtherInterface stubSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.stubInterface3 = MockRepository.GenerateStub<IInterface3>();
			this.stubInterface4 = MockRepository.GenerateStub<IInterface4>();
			this.stubSomeInterface = MockRepository.GenerateStub<ISomeInterface>();
			this.stubSomeOtherInterface = MockRepository.GenerateStub<ISomeOtherInterface>();
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
