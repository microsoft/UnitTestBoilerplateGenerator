using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ConstructorInjectedClassMultipleTests
	{
		private ISomeInterface fakeSomeInterface;
		private ISomeOtherInterface fakeSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.fakeSomeInterface = A.Fake<ISomeInterface>();
			this.fakeSomeOtherInterface = A.Fake<ISomeOtherInterface>();
		}

		private ConstructorInjectedClassMultiple CreateConstructorInjectedClassMultiple()
		{
			return new ConstructorInjectedClassMultiple(
				this.fakeSomeInterface,
				this.fakeSomeOtherInterface);
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = this.CreateConstructorInjectedClassMultiple();

			// Act

			// Assert
			Assert.Fail();
		}
	}
}
