using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class PropertyInjectedClassSingleTests
	{
		private ISomeInterface subSomeInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.subSomeInterface = Substitute.For<ISomeInterface>();
		}

		private PropertyInjectedClassSingle CreatePropertyInjectedClassSingle()
		{
			return new PropertyInjectedClassSingle
			{
				MyProperty = this.subSomeInterface,
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
