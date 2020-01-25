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

		private ClassWithOddCtorParams CreateClassWithOddCtorParams()
		{
			return new ClassWithOddCtorParams(
				TODO,
				TODO);
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var classWithOddCtorParams = this.CreateClassWithOddCtorParams();

			// Act


			// Assert
			Assert.Fail();
			this.mockRepository.VerifyAll();
		}
	}
}
