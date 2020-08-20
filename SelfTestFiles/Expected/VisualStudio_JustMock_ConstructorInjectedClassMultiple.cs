using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ConstructorInjectedClassMultipleTests
	{
		private ISomeInterface mockSomeInterface;
		private ISomeOtherInterface mockSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.mockSomeInterface = Mock.Create<ISomeInterface>();
			this.mockSomeOtherInterface = Mock.Create<ISomeOtherInterface>();
		}

		private ConstructorInjectedClassMultiple CreateConstructorInjectedClassMultiple()
		{
			return new ConstructorInjectedClassMultiple(
				this.mockSomeInterface,
				this.mockSomeOtherInterface);
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var constructorInjectedClassMultiple = this.CreateConstructorInjectedClassMultiple();

			// Act


			// Assert
			Assert.Fail();
		}
	}
}
