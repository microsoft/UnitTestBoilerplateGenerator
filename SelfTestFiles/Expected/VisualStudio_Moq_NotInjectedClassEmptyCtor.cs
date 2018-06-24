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

		[TestCleanup]
		public void TestCleanup()
		{
			this.mockRepository.VerifyAll();
		}

		private NotInjectedClassEmptyCtor CreateNotInjectedClassEmptyCtor()
		{
			return new NotInjectedClassEmptyCtor();
		}


	}
}
