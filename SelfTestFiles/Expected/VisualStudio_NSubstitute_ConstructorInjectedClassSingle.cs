using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ConstructorInjectedClassSingleTests
	{
		private ISomeInterface subSomeInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.subSomeInterface = Substitute.For<ISomeInterface>();
		}

		private ConstructorInjectedClassSingle CreateConstructorInjectedClassSingle()
		{
			return new ConstructorInjectedClassSingle(
				this.subSomeInterface);
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
