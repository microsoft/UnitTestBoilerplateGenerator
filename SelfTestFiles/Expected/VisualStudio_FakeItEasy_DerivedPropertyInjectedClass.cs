using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class DerivedPropertyInjectedClassTests
	{
		private IInterface3 fakeInterface3;
		private ISomeInterface fakeSomeInterface;
		private ISomeOtherInterface fakeSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.fakeInterface3 = A.Fake<IInterface3>();
			this.fakeSomeInterface = A.Fake<ISomeInterface>();
			this.fakeSomeOtherInterface = A.Fake<ISomeOtherInterface>();
		}

		private DerivedPropertyInjectedClass CreateDerivedPropertyInjectedClass()
		{
			return new DerivedPropertyInjectedClass
			{
				Interface3Property = this.fakeInterface3,
				MyProperty = this.fakeSomeInterface,
				Property2 = this.fakeSomeOtherInterface,
			};
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = this.CreateDerivedPropertyInjectedClass();

			// Act

			// Assert
			Assert.Fail();
		}
	}
}
