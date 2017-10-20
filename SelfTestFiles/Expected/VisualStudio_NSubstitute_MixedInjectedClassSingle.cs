using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class MixedInjectedClassSingleTests
	{
		private IInterface3 subInterface3;
		private ISomeInterface subSomeInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.subInterface3 = Substitute.For<IInterface3>();
			this.subSomeInterface = Substitute.For<ISomeInterface>();
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange


			// Act
			MixedInjectedClassSingle mixedInjectedClassSingle = this.CreateMixedInjectedClassSingle();


			// Assert

		}

		private MixedInjectedClassSingle CreateMixedInjectedClassSingle()
		{
			return new MixedInjectedClassSingle(
				this.subSomeInterface)
			{
				Interface3Property = this.subInterface3,
			};
		}
	}
}
