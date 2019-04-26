using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ConstructorInjectedClassSingleTests
	{
		private ISomeInterface fakeSomeInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.fakeSomeInterface = A.Fake<ISomeInterface>();
		}

		private ConstructorInjectedClassSingle CreateConstructorInjectedClassSingle()
		{
			return new ConstructorInjectedClassSingle(
				this.fakeSomeInterface);
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = this.CreateConstructorInjectedClassSingle();

			// Act

			// Assert
			Assert.Fail();
		}
	}
}
