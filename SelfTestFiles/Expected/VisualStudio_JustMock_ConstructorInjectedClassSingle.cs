using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ConstructorInjectedClassSingleTests
	{
		private ISomeInterface mockSomeInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.mockSomeInterface = Mock.Create<ISomeInterface>();
		}

		private ConstructorInjectedClassSingle CreateConstructorInjectedClassSingle()
		{
			return new ConstructorInjectedClassSingle(
				this.mockSomeInterface);
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var constructorInjectedClassSingle = this.CreateConstructorInjectedClassSingle();

			// Act


			// Assert
			Assert.Fail();
		}
	}
}
