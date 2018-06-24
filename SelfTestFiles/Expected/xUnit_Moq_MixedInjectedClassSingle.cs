using Moq;
using System;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;
using Xunit;

namespace UnitTestBoilerplate.SelfTest.Cases
{
	public class MixedInjectedClassSingleTests : IDisposable
	{
		private MockRepository mockRepository;

		private Mock<IInterface3> mockInterface3;
		private Mock<ISomeInterface> mockSomeInterface;

		public MixedInjectedClassSingleTests()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);

			this.mockInterface3 = this.mockRepository.Create<IInterface3>();
			this.mockSomeInterface = this.mockRepository.Create<ISomeInterface>();
		}

		public void Dispose()
		{
			this.mockRepository.VerifyAll();
		}

		private MixedInjectedClassSingle CreateMixedInjectedClassSingle()
		{
			return new MixedInjectedClassSingle(
				this.mockSomeInterface.Object)
			{
				Interface3Property = this.mockInterface3.Object,
			};
		}


	}
}
