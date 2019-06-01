using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ClassWithNonInterfaceCtorParamTests
	{
		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var unitUnderTest = new ClassWithNonInterfaceCtorParam(TODO);

			// Act

			// Assert
			Assert.Fail();
		}
	}
}
