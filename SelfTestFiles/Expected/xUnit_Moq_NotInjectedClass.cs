using Moq;
using System;
using UnitBoilerplate.Sandbox.Classes.Cases;
using Xunit;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	public class NotInjectedClassTests : IDisposable
	{
		private MockRepository mockRepository;



		public NotInjectedClassTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);


		}

		public void Dispose()
		{
			this.mockRepository.VerifyAll();
		}

		private NotInjectedClass CreateNotInjectedClass()
		{
			return new NotInjectedClass();
		}

		[Fact]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = this.CreateNotInjectedClass();

			// Act

			// Assert
			Assert.True(false);
		}
	}
}
