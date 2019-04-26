using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class SomeServiceTests
	{
		private ISomeInterface stubSomeInterface;
		private ISomeOtherInterface stubSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.stubSomeInterface = MockRepository.GenerateStub<ISomeInterface>();
			this.stubSomeOtherInterface = MockRepository.GenerateStub<ISomeOtherInterface>();
		}

		private SomeService CreateService()
		{
			return new SomeService(
				this.stubSomeInterface,
				this.stubSomeOtherInterface);
		}

		[TestMethod]
		public void AddNumbers_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var unitUnderTest = this.CreateService();
			int a = TODO;
			int b = TODO;

			// Act
			var result = unitUnderTest.AddNumbers(
				a,
				b);

			// Assert
			Assert.Fail();
		}
	}
}
