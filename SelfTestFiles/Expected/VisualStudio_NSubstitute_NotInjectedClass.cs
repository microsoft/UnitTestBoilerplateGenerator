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


		private NotInjectedClass CreateNotInjectedClass()
		{
			return new NotInjectedClass();
		}


	}
}
