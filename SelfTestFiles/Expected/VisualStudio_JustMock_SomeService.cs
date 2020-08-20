using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Telerik.JustMock;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class SomeServiceTests
	{
		private ISomeInterface mockSomeInterface;
		private ISomeOtherInterface mockSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.mockSomeInterface = Mock.Create<ISomeInterface>();
			this.mockSomeOtherInterface = Mock.Create<ISomeOtherInterface>();
		}

		private SomeService CreateService()
		{
			return new SomeService(
				this.mockSomeInterface,
				this.mockSomeOtherInterface);
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
		}
	}
}
