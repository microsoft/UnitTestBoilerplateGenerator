using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class MixedInjectedClassSingleTests
	{
		private IInterface3 mockInterface3;
		private ISomeInterface mockSomeInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.mockInterface3 = Mock.Create<IInterface3>();
			this.mockSomeInterface = Mock.Create<ISomeInterface>();
		}

		private MixedInjectedClassSingle CreateMixedInjectedClassSingle()
		{
			return new MixedInjectedClassSingle(
				this.mockSomeInterface)
			{
				Interface3Property = this.mockInterface3,
			};
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var mixedInjectedClassSingle = this.CreateMixedInjectedClassSingle();

			// Act


			// Assert
			Assert.Fail();
		}
	}
}
