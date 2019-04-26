using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ConstructorInjectedClassMultipleTests
	{
		private ISomeInterface stubSomeInterface;
		private ISomeOtherInterface stubSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.stubSomeInterface = MockRepository.GenerateStub<ISomeInterface>();
			this.stubSomeOtherInterface = MockRepository.GenerateStub<ISomeOtherInterface>();
		}

		private ConstructorInjectedClassMultiple CreateConstructorInjectedClassMultiple()
		{
			return new ConstructorInjectedClassMultiple(
				this.stubSomeInterface,
				this.stubSomeOtherInterface);
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
