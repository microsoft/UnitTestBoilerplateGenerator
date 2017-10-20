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

		[TearDown]
		public void TearDown()
		{
			this.mockRepository.VerifyAll();
		}

		[Test]
		public void TestMethod1()
		{
			// Arrange


			// Act
			PropertyInjectedClassSingle propertyInjectedClassSingle = this.CreatePropertyInjectedClassSingle();


			// Assert

		}

		private PropertyInjectedClassSingle CreatePropertyInjectedClassSingle()
		{
			return new PropertyInjectedClassSingle
			{
				MyProperty = this.mockSomeInterface.Object,
			};
		}
	}
}
