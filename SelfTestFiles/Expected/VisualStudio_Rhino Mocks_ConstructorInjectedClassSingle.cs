using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ConstructorInjectedClassSingleTests
	{
		private ISomeInterface stubSomeInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.stubSomeInterface = MockRepository.GenerateStub<ISomeInterface>();
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange


			// Act
			ConstructorInjectedClassSingle constructorInjectedClassSingle = this.CreateConstructorInjectedClassSingle();


			// Assert

		}

		private ConstructorInjectedClassSingle CreateConstructorInjectedClassSingle()
		{
			return new ConstructorInjectedClassSingle(
				this.stubSomeInterface);
		}
	}
}
