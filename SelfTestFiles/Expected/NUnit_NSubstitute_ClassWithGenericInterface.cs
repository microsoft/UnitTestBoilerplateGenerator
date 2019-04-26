using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestFixture]
	public class ClassWithGenericInterfaceTests
	{
		private IInterface3 subInterface3;
		private IGenericInterface<List<int>> subGenericInterfaceListInt;
		private IGenericInterface<List<ISomeOtherInterface>> subGenericInterfaceListSomeOtherInterface;
		private IGenericInterface<bool> subGenericInterfaceBool;
		private IGenericInterface<List<string>> subGenericInterfaceListString;
		private ISomeInterface subSomeInterface;

		[SetUp]
		public void SetUp()
		{
			this.subInterface3 = Substitute.For<IInterface3>();
			this.subGenericInterfaceListInt = Substitute.For<IGenericInterface<List<int>>>();
			this.subGenericInterfaceListSomeOtherInterface = Substitute.For<IGenericInterface<List<ISomeOtherInterface>>>();
			this.subGenericInterfaceBool = Substitute.For<IGenericInterface<bool>>();
			this.subGenericInterfaceListString = Substitute.For<IGenericInterface<List<string>>>();
			this.subSomeInterface = Substitute.For<ISomeInterface>();
		}

		private ClassWithGenericInterface CreateClassWithGenericInterface()
		{
			return new ClassWithGenericInterface(
				this.subGenericInterfaceBool,
				this.subGenericInterfaceListString,
				this.subSomeInterface)
			{
				Interface2 = this.subInterface3,
				GenericInterface3 = this.subGenericInterfaceListInt,
				GenericInterface4 = this.subGenericInterfaceListSomeOtherInterface,
			};
		}

		[Test]
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
