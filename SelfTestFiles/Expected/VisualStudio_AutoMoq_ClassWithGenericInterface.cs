using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ClassWithGenericInterfaceTests
	{
		[TestMethod]
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
