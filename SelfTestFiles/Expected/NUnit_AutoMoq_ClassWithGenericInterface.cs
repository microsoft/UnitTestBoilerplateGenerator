using AutoMoq;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestFixture]
	public class ClassWithGenericInterfaceTests
	{
		[Test]
		public void TestMethod1()
		{
			// Arrange
			var mocker = new AutoMoqer();
			var unitUnderTest = mocker.Create<ClassWithGenericInterface>();

			// Act

			// Assert
			Assert.Fail();
		}
	}
}
