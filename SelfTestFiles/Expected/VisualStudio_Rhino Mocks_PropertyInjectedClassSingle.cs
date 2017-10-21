using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class PropertyInjectedClassSingleTests
	{
		private ISomeInterface stubSomeInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.stubSomeInterface = MockRepository.GenerateStub<ISomeInterface>();
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange


			// Act
			PropertyInjectedClassSingle propertyInjectedClassSingle = this.CreatePropertyInjectedClassSingle();


			// Assert

		}

		private PropertyInjectedClassSingle CreatePropertyInjectedClassSingle()
		{
			return new PropertyInjectedClassSingle
			{
				MyProperty = this.stubSomeInterface,
			};
		}
	}
}
