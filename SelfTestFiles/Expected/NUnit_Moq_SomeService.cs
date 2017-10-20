using Moq;
using NUnit.Framework;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestFixture]
	public class SomeServiceTests
	{
		private MockRepository mockRepository;

		private Mock<ISomeInterface> mockSomeInterface;
		private Mock<ISomeOtherInterface> mockSomeOtherInterface;

		[SetUp]
		public void SetUp()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);

			this.mockSomeInterface = this.mockRepository.Create<ISomeInterface>();
			this.mockSomeOtherInterface = this.mockRepository.Create<ISomeOtherInterface>();
		}

		[TearDown]
		public void TearDown()
		{
			this.mockRepository.VerifyAll();
		}

		[Test]
		public void TestMethod1()
		{
			// Arrange


			// Act
			SomeService service = this.CreateService();


			// Assert

		}

		private SomeService CreateService()
		{
			return new SomeService(
				this.mockSomeInterface.Object,
				this.mockSomeOtherInterface.Object);
		}
	}
}
