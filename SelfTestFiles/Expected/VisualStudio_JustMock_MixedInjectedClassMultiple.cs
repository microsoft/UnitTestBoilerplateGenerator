using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class MixedInjectedClassMultipleTests
	{
		private IInterface3 mockInterface3;
		private IInterface4 mockInterface4;
		private ISomeInterface mockSomeInterface;
		private ISomeOtherInterface mockSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.mockInterface3 = Mock.Create<IInterface3>();
			this.mockInterface4 = Mock.Create<IInterface4>();
			this.mockSomeInterface = Mock.Create<ISomeInterface>();
			this.mockSomeOtherInterface = Mock.Create<ISomeOtherInterface>();
		}

		private MixedInjectedClassMultiple CreateMixedInjectedClassMultiple()
		{
			return new MixedInjectedClassMultiple(
				this.mockSomeInterface,
				this.mockSomeOtherInterface)
			{
				Interface3Property = this.mockInterface3,
				Interface4Property = this.mockInterface4,
			};
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var mixedInjectedClassMultiple = this.CreateMixedInjectedClassMultiple();

			// Act


			// Assert
			Assert.Fail();
		}
	}
}
