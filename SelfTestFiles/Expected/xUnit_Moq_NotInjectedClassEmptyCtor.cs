using Moq;
using System;
using UnitBoilerplate.Sandbox.Classes.Cases;
using Xunit;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	public class NotInjectedClassEmptyCtorTests : IDisposable
	{
		private MockRepository mockRepository;



		public NotInjectedClassEmptyCtorTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);


		}

		public void Dispose()
		{
			this.mockRepository.VerifyAll();
		}

		private NotInjectedClassEmptyCtor CreateNotInjectedClassEmptyCtor()
		{
			return new NotInjectedClassEmptyCtor();
		}

		[Fact]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = this.CreateNotInjectedClassEmptyCtor();

			// Act

			// Assert
			Assert.True(false);
		}
	}
}
