using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ClassWithOddCtorParamsTests
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

		private ClassWithOddCtorParams CreateClassWithOddCtorParams()
		{
			return new ClassWithOddCtorParams(
				TODO,
				TODO);
		}


	}
}
