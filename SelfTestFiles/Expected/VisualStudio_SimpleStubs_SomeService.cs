using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class SomeServiceTests
	{
		private StubISomeInterface stubSomeInterface;
		private StubISomeOtherInterface stubSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.stubSomeInterface = new StubISomeInterface();
			this.stubSomeOtherInterface = new StubISomeOtherInterface();
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
