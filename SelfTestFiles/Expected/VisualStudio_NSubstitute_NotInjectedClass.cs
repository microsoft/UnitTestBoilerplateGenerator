using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class NotInjectedClassTests
	{


		[TestInitialize]
		public void TestInitialize()
		{

		}

		[TestMethod]
		public void TestMethod1()
		{
			// Arrange


			// Act
			NotInjectedClass notInjectedClass = this.CreateNotInjectedClass();


			// Assert

		}

		private NotInjectedClass CreateNotInjectedClass()
		{
			return new NotInjectedClass();
		}
	}
}
