using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class SomeServiceTests
	{
		private MockRepository mockRepository;

		private Mock<ISomeInterface> mockSomeInterface;
		private Mock<ISomeOtherInterface> mockSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);

			this.mockSomeInterface = this.mockRepository.Create<ISomeInterface>();
			this.mockSomeOtherInterface = this.mockRepository.Create<ISomeOtherInterface>();
		}

		private SomeService CreateService()
		{
			return new SomeService(
				this.mockSomeInterface.Object,
				this.mockSomeOtherInterface.Object);
		}

		[TestMethod]
		public void AddNumbers_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var service = this.CreateService();
			int a = 0;
			int b = 0;

			// Act
			var result = service.AddNumbers(
				a,
				b);

			// Assert
			Assert.Fail();
			this.mockRepository.VerifyAll();
		}
	}
}
