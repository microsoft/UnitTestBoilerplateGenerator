using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class PropertyInjectedClassMultipleTests
	{
		private ISomeInterface stubSomeInterface;
		private ISomeOtherInterface stubSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.stubSomeInterface = MockRepository.GenerateStub<ISomeInterface>();
			this.stubSomeOtherInterface = MockRepository.GenerateStub<ISomeOtherInterface>();
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange


			// Act
			PropertyInjectedClassMultiple propertyInjectedClassMultiple = this.CreatePropertyInjectedClassMultiple();


			// Assert

		}

		private PropertyInjectedClassMultiple CreatePropertyInjectedClassMultiple()
		{
			return new PropertyInjectedClassMultiple
			{
				MyProperty = this.stubSomeInterface,
				Property2 = this.stubSomeOtherInterface,
			};
		}
	}
}
