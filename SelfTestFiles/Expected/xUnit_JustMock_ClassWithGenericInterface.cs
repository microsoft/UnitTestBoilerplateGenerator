using System;
using System.Collections.Generic;
using Telerik.JustMock;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;
using Xunit;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	public class ClassWithGenericInterfaceTests
	{
		private IInterface3 mockInterface3;
		private IGenericInterface<List<int>> mockGenericInterfaceListInt;
		private IGenericInterface<List<ISomeOtherInterface>> mockGenericInterfaceListSomeOtherInterface;
		private IGenericInterface<bool> mockGenericInterfaceBool;
		private IGenericInterface<List<string>> mockGenericInterfaceListString;
		private ISomeInterface mockSomeInterface;

		public ClassWithGenericInterfaceTests()
		{
			this.mockInterface3 = Mock.Create<IInterface3>();
			this.mockGenericInterfaceListInt = Mock.Create<IGenericInterface<List<int>>>();
			this.mockGenericInterfaceListSomeOtherInterface = Mock.Create<IGenericInterface<List<ISomeOtherInterface>>>();
			this.mockGenericInterfaceBool = Mock.Create<IGenericInterface<bool>>();
			this.mockGenericInterfaceListString = Mock.Create<IGenericInterface<List<string>>>();
			this.mockSomeInterface = Mock.Create<ISomeInterface>();
		}

		private ClassWithGenericInterface CreateClassWithGenericInterface()
		{
			return new ClassWithGenericInterface(
				this.mockGenericInterfaceBool,
				this.mockGenericInterfaceListString,
				this.mockSomeInterface)
			{
				Interface2 = this.mockInterface3,
				GenericInterface3 = this.mockGenericInterfaceListInt,
				GenericInterface4 = this.mockGenericInterfaceListSomeOtherInterface,
			};
		}

		[Fact]
		public void TestMethod1()
		{
			// Arrange
			var classWithGenericInterface = this.CreateClassWithGenericInterface();

			// Act


			// Assert
			Assert.True(false);
		}
	}
}
