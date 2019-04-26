using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class SomeServiceTests
	{
		private ISomeInterface fakeSomeInterface;
		private ISomeOtherInterface fakeSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.fakeSomeInterface = A.Fake<ISomeInterface>();
			this.fakeSomeOtherInterface = A.Fake<ISomeOtherInterface>();
		}

		private SomeService CreateService()
		{
			return new SomeService(
				this.fakeSomeInterface,
				this.fakeSomeOtherInterface);
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
