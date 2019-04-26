using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class PropertyInjectedClassSingleTests
	{
		private ISomeInterface fakeSomeInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.fakeSomeInterface = A.Fake<ISomeInterface>();
		}

		private PropertyInjectedClassSingle CreatePropertyInjectedClassSingle()
		{
			return new PropertyInjectedClassSingle
			{
				MyProperty = this.fakeSomeInterface,
			};
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = this.CreatePropertyInjectedClassSingle();

			// Act

			// Assert
			Assert.Fail();
		}
	}
}
