using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class MixedInjectedClassSingleTests
	{
		private IInterface3 fakeInterface3;
		private ISomeInterface fakeSomeInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.fakeInterface3 = A.Fake<IInterface3>();
			this.fakeSomeInterface = A.Fake<ISomeInterface>();
		}

		private MixedInjectedClassSingle CreateMixedInjectedClassSingle()
		{
			return new MixedInjectedClassSingle(
				this.fakeSomeInterface)
			{
				Interface3Property = this.fakeInterface3,
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
