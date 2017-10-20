using AutoMoq;
using Moq;
using System;
using System.Collections.Generic;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;
using Xunit;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	public class ClassWithGenericInterfaceTests
	{
		[Fact]
		public void TestMethod1()
		{
			// Arrange
			var mocker = new AutoMoqer();


			// Act
			var classWithGenericInterface = mocker.Create<ClassWithGenericInterface>();


			// Assert

		}
	}
}
