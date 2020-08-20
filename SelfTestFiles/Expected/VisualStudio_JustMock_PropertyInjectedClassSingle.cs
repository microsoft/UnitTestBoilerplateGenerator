using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class PropertyInjectedClassSingleTests
	{
		private ISomeInterface mockSomeInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.mockSomeInterface = Mock.Create<ISomeInterface>();
		}

		private PropertyInjectedClassSingle CreatePropertyInjectedClassSingle()
		{
			return new PropertyInjectedClassSingle
			{
				MyProperty = this.mockSomeInterface,
			};
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var propertyInjectedClassSingle = this.CreatePropertyInjectedClassSingle();

			// Act


			// Assert
			Assert.Fail();
		}
	}
}
