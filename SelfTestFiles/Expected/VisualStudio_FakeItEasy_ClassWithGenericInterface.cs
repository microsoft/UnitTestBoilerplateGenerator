using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ClassWithGenericInterfaceTests
	{
		private IInterface3 fakeInterface3;
		private IGenericInterface<List<int>> fakeGenericInterfaceListInt;
		private IGenericInterface<List<ISomeOtherInterface>> fakeGenericInterfaceListSomeOtherInterface;
		private IGenericInterface<bool> fakeGenericInterfaceBool;
		private IGenericInterface<List<string>> fakeGenericInterfaceListString;
		private ISomeInterface fakeSomeInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.fakeInterface3 = A.Fake<IInterface3>();
			this.fakeGenericInterfaceListInt = A.Fake<IGenericInterface<List<int>>>();
			this.fakeGenericInterfaceListSomeOtherInterface = A.Fake<IGenericInterface<List<ISomeOtherInterface>>>();
			this.fakeGenericInterfaceBool = A.Fake<IGenericInterface<bool>>();
			this.fakeGenericInterfaceListString = A.Fake<IGenericInterface<List<string>>>();
			this.fakeSomeInterface = A.Fake<ISomeInterface>();
		}

		private ClassWithGenericInterface CreateClassWithGenericInterface()
		{
			return new ClassWithGenericInterface(
				this.fakeGenericInterfaceBool,
				this.fakeGenericInterfaceListString,
				this.fakeSomeInterface)
			{
				Interface2 = this.fakeInterface3,
				GenericInterface3 = this.fakeGenericInterfaceListInt,
				GenericInterface4 = this.fakeGenericInterfaceListSomeOtherInterface,
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
