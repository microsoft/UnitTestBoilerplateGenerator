using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System.Collections.Generic;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ClassWithGenericInterfaceTests
	{
		private IInterface3 stubInterface3;
		private IGenericInterface<List<int>> stubGenericInterfaceListInt;
		private IGenericInterface<List<ISomeOtherInterface>> stubGenericInterfaceListSomeOtherInterface;
		private IGenericInterface<bool> stubGenericInterfaceBool;
		private IGenericInterface<List<string>> stubGenericInterfaceListString;
		private ISomeInterface stubSomeInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.stubInterface3 = MockRepository.GenerateStub<IInterface3>();
			this.stubGenericInterfaceListInt = MockRepository.GenerateStub<IGenericInterface<List<int>>>();
			this.stubGenericInterfaceListSomeOtherInterface = MockRepository.GenerateStub<IGenericInterface<List<ISomeOtherInterface>>>();
			this.stubGenericInterfaceBool = MockRepository.GenerateStub<IGenericInterface<bool>>();
			this.stubGenericInterfaceListString = MockRepository.GenerateStub<IGenericInterface<List<string>>>();
			this.stubSomeInterface = MockRepository.GenerateStub<ISomeInterface>();
		}

		private ClassWithGenericInterface CreateClassWithGenericInterface()
		{
			return new ClassWithGenericInterface(
				this.stubGenericInterfaceBool,
				this.stubGenericInterfaceListString,
				this.stubSomeInterface)
			{
				Interface2 = this.stubInterface3,
				GenericInterface3 = this.stubGenericInterfaceListInt,
				GenericInterface4 = this.stubGenericInterfaceListSomeOtherInterface,
			};
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = this.CreateClassWithGenericInterface();

			// Act

			// Assert
			Assert.Fail();
		}
	}
}
