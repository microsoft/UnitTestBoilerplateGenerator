using Moq;
using System;
using UnitBoilerplate.Sandbox.Classes.Cases;
using Xunit;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	public class NotInjectedClassTests
	{
		private MockRepository mockRepository;



		public NotInjectedClassTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);


		}

		private NotInjectedClass CreateNotInjectedClass()
		{
			return new NotInjectedClass();
		}

		[Fact]
		public void TestMethod1()
		{
			// Arrange
			var notInjectedClass = this.CreateNotInjectedClass();

			// Act


			// Assert
			Assert.True(false);
			this.mockRepository.VerifyAll();
		}
	}
}
