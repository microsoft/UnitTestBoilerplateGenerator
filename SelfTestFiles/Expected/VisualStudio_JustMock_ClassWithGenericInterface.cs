using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Telerik.JustMock;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ClassWithGenericInterfaceTests
	{
		private IInterface3 mockInterface3;
		private IGenericInterface<List<int>> mockGenericInterfaceListInt;
		private IGenericInterface<List<ISomeOtherInterface>> mockGenericInterfaceListSomeOtherInterface;
		private IGenericInterface<bool> mockGenericInterfaceBool;
		private IGenericInterface<List<string>> mockGenericInterfaceListString;
		private ISomeInterface mockSomeInterface;

		[TestInitialize]
		public void TestInitialize()
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

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var classWithGenericInterface = this.CreateClassWithGenericInterface();

			// Act


			// Assert
			Assert.Fail();
		}
	}
}
