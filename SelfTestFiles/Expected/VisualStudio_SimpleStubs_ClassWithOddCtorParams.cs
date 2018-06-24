using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	[TestClass]
	public class ClassWithOddCtorParamsTests
	{


		[TestInitialize]
		public void TestInitialize()
		{

		}


		private ClassWithOddCtorParams CreateClassWithOddCtorParams()
		{
			return new ClassWithOddCtorParams(
				TODO,
				TODO);
		}


	}
}
