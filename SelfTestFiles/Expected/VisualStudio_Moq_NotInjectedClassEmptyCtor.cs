using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class NotInjectedClassEmptyCtorTests
	{
		private MockRepository mockRepository;



		[TestInitialize]
		public void TestInitialize()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);


		}

		private NotInjectedClassEmptyCtor CreateNotInjectedClassEmptyCtor()
		{
			return new NotInjectedClassEmptyCtor();
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var notInjectedClassEmptyCtor = this.CreateNotInjectedClassEmptyCtor();

			// Act


			// Assert
			Assert.Fail();
			this.mockRepository.VerifyAll();
		}
	}
}
