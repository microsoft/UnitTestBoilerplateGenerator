using Moq;
using System;
using UnitBoilerplate.Sandbox.Classes.Cases;
using Xunit;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	public class NotInjectedClassEmptyCtorTests
	{
		private MockRepository mockRepository;



		public NotInjectedClassEmptyCtorTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);


		}

		private NotInjectedClassEmptyCtor CreateNotInjectedClassEmptyCtor()
		{
			return new NotInjectedClassEmptyCtor();
		}

		[Fact]
		public void TestMethod1()
		{
			// Arrange
			var notInjectedClassEmptyCtor = this.CreateNotInjectedClassEmptyCtor();

			// Act


			// Assert
			Assert.True(false);
			this.mockRepository.VerifyAll();
		}
	}
}
