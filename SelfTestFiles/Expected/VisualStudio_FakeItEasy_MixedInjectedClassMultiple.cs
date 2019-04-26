using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class MixedInjectedClassMultipleTests
	{
		private IInterface3 fakeInterface3;
		private IInterface4 fakeInterface4;
		private ISomeInterface fakeSomeInterface;
		private ISomeOtherInterface fakeSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.fakeInterface3 = A.Fake<IInterface3>();
			this.fakeInterface4 = A.Fake<IInterface4>();
			this.fakeSomeInterface = A.Fake<ISomeInterface>();
			this.fakeSomeOtherInterface = A.Fake<ISomeOtherInterface>();
		}

		private MixedInjectedClassMultiple CreateMixedInjectedClassMultiple()
		{
			return new MixedInjectedClassMultiple(
				this.fakeSomeInterface,
				this.fakeSomeOtherInterface)
			{
				Interface3Property = this.fakeInterface3,
				Interface4Property = this.fakeInterface4,
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
