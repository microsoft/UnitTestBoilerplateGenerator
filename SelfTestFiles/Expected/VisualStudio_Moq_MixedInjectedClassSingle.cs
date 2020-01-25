using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class MixedInjectedClassSingleTests
	{
		private MockRepository mockRepository;

		private Mock<IInterface3> mockInterface3;
		private Mock<ISomeInterface> mockSomeInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);

			this.mockInterface3 = this.mockRepository.Create<IInterface3>();
			this.mockSomeInterface = this.mockRepository.Create<ISomeInterface>();
		}

		private MixedInjectedClassSingle CreateMixedInjectedClassSingle()
		{
			return new MixedInjectedClassSingle(
				this.mockSomeInterface.Object)
			{
				Interface3Property = this.mockInterface3.Object,
			};
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var mixedInjectedClassSingle = this.CreateMixedInjectedClassSingle();

			// Act


			// Assert
			Assert.Fail();
			this.mockRepository.VerifyAll();
		}
	}
}
