using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class SomeServiceTests
	{
		private StubISomeInterface stubSomeInterface;
		private StubISomeOtherInterface stubSomeOtherInterface;

		[TestInitialize]
		public void TestInitialize()
		{
			this.stubSomeInterface = new StubISomeInterface();
			this.stubSomeOtherInterface = new StubISomeOtherInterface();
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
