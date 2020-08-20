using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class DerivedPropertyInjectedClassTests
	{
		private IInterface3 mockInterface3;
		private ISomeInterface mockSomeInterface;
		private ISomeOtherInterface mockSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.mockInterface3 = Mock.Create<IInterface3>();
			this.mockSomeInterface = Mock.Create<ISomeInterface>();
			this.mockSomeOtherInterface = Mock.Create<ISomeOtherInterface>();
		}

		private DerivedPropertyInjectedClass CreateDerivedPropertyInjectedClass()
		{
			return new DerivedPropertyInjectedClass
			{
				Interface3Property = this.mockInterface3,
				MyProperty = this.mockSomeInterface,
				Property2 = this.mockSomeOtherInterface,
			};
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var derivedPropertyInjectedClass = this.CreateDerivedPropertyInjectedClass();

			// Act


			// Assert
			Assert.Fail();
		}
	}
}
