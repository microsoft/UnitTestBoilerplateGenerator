using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class SomeServiceTests
	{
		private ISomeInterface stubSomeInterface;
		private ISomeOtherInterface stubSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.stubSomeInterface = MockRepository.GenerateStub<ISomeInterface>();
			this.stubSomeOtherInterface = MockRepository.GenerateStub<ISomeOtherInterface>();
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange


			// Act
			SomeService service = this.CreateService();


			// Assert

		}

		private SomeService CreateService()
		{
			return new SomeService(
				this.stubSomeInterface,
				this.stubSomeOtherInterface);
		}
	}
}
