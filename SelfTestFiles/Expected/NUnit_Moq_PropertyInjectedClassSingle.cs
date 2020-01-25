using Moq;
using NUnit.Framework;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestFixture]
	public class PropertyInjectedClassSingleTests
	{
		private MockRepository mockRepository;

		private Mock<ISomeInterface> mockSomeInterface;

		[SetUp]
		public void SetUp()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);

			this.mockSomeInterface = this.mockRepository.Create<ISomeInterface>();
		}

		private PropertyInjectedClassSingle CreatePropertyInjectedClassSingle()
		{
			return new PropertyInjectedClassSingle
			{
				MyProperty = this.mockSomeInterface.Object,
			};
		}

		[Test]
		public void TestMethod1()
		{
			// Arrange
			var propertyInjectedClassSingle = this.CreatePropertyInjectedClassSingle();

			// Act


			// Assert
			Assert.Fail();
			this.mockRepository.VerifyAll();
		}
	}
}
