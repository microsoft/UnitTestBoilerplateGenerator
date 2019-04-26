using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class PropertyInjectedClassMultipleTests
	{
		private ISomeInterface subSomeInterface;
		private ISomeOtherInterface subSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.subSomeInterface = Substitute.For<ISomeInterface>();
			this.subSomeOtherInterface = Substitute.For<ISomeOtherInterface>();
		}

		private PropertyInjectedClassMultiple CreatePropertyInjectedClassMultiple()
		{
			return new PropertyInjectedClassMultiple
			{
				MyProperty = this.subSomeInterface,
				Property2 = this.subSomeOtherInterface,
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
