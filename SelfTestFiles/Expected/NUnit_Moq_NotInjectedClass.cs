using Moq;
using NUnit.Framework;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestFixture]
	public class NotInjectedClassTests
	{
		private MockRepository mockRepository;



		[SetUp]
		public void SetUp()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);


		}

		private NotInjectedClass CreateNotInjectedClass()
		{
			return new NotInjectedClass();
		}

		[Test]
		public void TestMethod1()
		{
			// Arrange
			var notInjectedClass = this.CreateNotInjectedClass();

			// Act


			// Assert
			Assert.Fail();
			this.mockRepository.VerifyAll();
		}
	}
}
