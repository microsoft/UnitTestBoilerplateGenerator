using FakeItEasy;
using System;
using System.Collections.Generic;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;
using Xunit;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	public class ClassWithGenericInterfaceTests
	{
		private IInterface3 fakeInterface3;
		private IGenericInterface<List<int>> fakeGenericInterfaceListInt;
		private IGenericInterface<List<ISomeOtherInterface>> fakeGenericInterfaceListSomeOtherInterface;
		private IGenericInterface<bool> fakeGenericInterfaceBool;
		private IGenericInterface<List<string>> fakeGenericInterfaceListString;
		private ISomeInterface fakeSomeInterface;

		public ClassWithGenericInterfaceTests()
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

		[Fact]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = this.CreateClassWithGenericInterface();

			// Act

			// Assert
			Assert.True(false);
		}
	}
}
