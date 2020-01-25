using Moq;
using NUnit.Framework;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestFixture]
	public class ClassWithOddCtorParamsTests
	{
		private MockRepository mockRepository;



		[SetUp]
		public void SetUp()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);


		}

		private ClassWithOddCtorParams CreateClassWithOddCtorParams()
		{
			return new ClassWithOddCtorParams(
				TODO,
				TODO);
		}

		[Test]
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
