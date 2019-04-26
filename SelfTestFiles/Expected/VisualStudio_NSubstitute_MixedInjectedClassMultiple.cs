using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class MixedInjectedClassMultipleTests
	{
		private IInterface3 subInterface3;
		private IInterface4 subInterface4;
		private ISomeInterface subSomeInterface;
		private ISomeOtherInterface subSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.subInterface3 = Substitute.For<IInterface3>();
			this.subInterface4 = Substitute.For<IInterface4>();
			this.subSomeInterface = Substitute.For<ISomeInterface>();
			this.subSomeOtherInterface = Substitute.For<ISomeOtherInterface>();
		}

		private MixedInjectedClassMultiple CreateMixedInjectedClassMultiple()
		{
			return new MixedInjectedClassMultiple(
				this.subSomeInterface,
				this.subSomeOtherInterface)
			{
				Interface3Property = this.subInterface3,
				Interface4Property = this.subInterface4,
			};
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = this.CreateMixedInjectedClassMultiple();

			// Act

			// Assert
			Assert.Fail();
		}
	}
}
