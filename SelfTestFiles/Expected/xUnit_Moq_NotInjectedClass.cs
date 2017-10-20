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

		[Fact]
		public void TestMethod1()
		{
			// Arrange


			// Act
			NotInjectedClass notInjectedClass = this.CreateNotInjectedClass();


			// Assert

		}

		private NotInjectedClass CreateNotInjectedClass()
		{
			return new NotInjectedClass();
		}
	}
}
