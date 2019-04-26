using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class PropertyInjectedClassMultipleTests
	{
		private ISomeInterface fakeSomeInterface;
		private ISomeOtherInterface fakeSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.fakeSomeInterface = A.Fake<ISomeInterface>();
			this.fakeSomeOtherInterface = A.Fake<ISomeOtherInterface>();
		}

		private PropertyInjectedClassMultiple CreatePropertyInjectedClassMultiple()
		{
			return new PropertyInjectedClassMultiple
			{
				MyProperty = this.fakeSomeInterface,
				Property2 = this.fakeSomeOtherInterface,
			};
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = this.CreatePropertyInjectedClassMultiple();

			// Act

			// Assert
			Assert.Fail();
		}
	}
}
