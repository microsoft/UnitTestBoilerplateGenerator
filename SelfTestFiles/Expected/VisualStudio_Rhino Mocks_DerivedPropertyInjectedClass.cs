using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class DerivedPropertyInjectedClassTests
	{
		private IInterface3 stubInterface3;
		private ISomeInterface stubSomeInterface;
		private ISomeOtherInterface stubSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.stubInterface3 = MockRepository.GenerateStub<IInterface3>();
			this.stubSomeInterface = MockRepository.GenerateStub<ISomeInterface>();
			this.stubSomeOtherInterface = MockRepository.GenerateStub<ISomeOtherInterface>();
		}

		private DerivedPropertyInjectedClass CreateDerivedPropertyInjectedClass()
		{
			return new DerivedPropertyInjectedClass
			{
				Interface3Property = this.stubInterface3,
				MyProperty = this.stubSomeInterface,
				Property2 = this.stubSomeOtherInterface,
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
