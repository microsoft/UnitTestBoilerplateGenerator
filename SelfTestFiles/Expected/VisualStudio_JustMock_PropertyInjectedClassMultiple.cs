using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class PropertyInjectedClassMultipleTests
	{
		private ISomeInterface mockSomeInterface;
		private ISomeOtherInterface mockSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.mockSomeInterface = Mock.Create<ISomeInterface>();
			this.mockSomeOtherInterface = Mock.Create<ISomeOtherInterface>();
		}

		private PropertyInjectedClassMultiple CreatePropertyInjectedClassMultiple()
		{
			return new PropertyInjectedClassMultiple
			{
				MyProperty = this.mockSomeInterface,
				Property2 = this.mockSomeOtherInterface,
			};
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var propertyInjectedClassMultiple = this.CreatePropertyInjectedClassMultiple();

			// Act


			// Assert
			Assert.Fail();
		}
	}
}
