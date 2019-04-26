using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class DerivedPropertyInjectedClassTests
	{
		private IInterface3 subInterface3;
		private ISomeInterface subSomeInterface;
		private ISomeOtherInterface subSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.subInterface3 = Substitute.For<IInterface3>();
			this.subSomeInterface = Substitute.For<ISomeInterface>();
			this.subSomeOtherInterface = Substitute.For<ISomeOtherInterface>();
		}

		private DerivedPropertyInjectedClass CreateDerivedPropertyInjectedClass()
		{
			return new DerivedPropertyInjectedClass
			{
				Interface3Property = this.subInterface3,
				MyProperty = this.subSomeInterface,
				Property2 = this.subSomeOtherInterface,
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
