using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitBoilerplate.Sandbox.NUnitTestCases.Reference
{
	[TestFixture]
	public class ReferenceMixedMultipleTest
	{
		private MockRepository mockRepository;

		private Mock<ISomeInterface> mockSomeInterface;
		private Mock<ISomeOtherInterface> mockSomeOtherInterface;
		private Mock<IInterface3> mockInterface3;
		private Mock<IInterface4> mockInterface4;

		[SetUp]
		public void SetUp()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);

			this.mockSomeInterface = this.mockRepository.Create<ISomeInterface>();
			this.mockSomeOtherInterface = this.mockRepository.Create<ISomeOtherInterface>();
			this.mockInterface3 = this.mockRepository.Create<IInterface3>();
			this.mockInterface4 = this.mockRepository.Create<IInterface4>();
		}

		[TearDown]
		public void TearDown()
		{
			this.mockRepository.VerifyAll();
		}

		[Test]
		public void TestMethod1()
		{


			MixedInjectedClassMultiple viewModel = this.CreateViewModel();


		}

		private MixedInjectedClassMultiple CreateViewModel()
		{
			return new MixedInjectedClassMultiple(
				this.mockSomeInterface.Object,
				this.mockSomeOtherInterface.Object)
			{
				Interface3Property = this.mockInterface3.Object,
				Interface4Property = this.mockInterface4.Object,
			};
		}
	}
}
