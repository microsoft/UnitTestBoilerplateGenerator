using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitBoilerplate.Sandbox.VSTestCases.Reference
{
    [TestClass]
    public class ReferenceMixedMultipleTest
    {
        private MockRepository mockRepository;

        private Mock<ISomeInterface> mockSomeInterface;
        private Mock<ISomeOtherInterface> mockSomeOtherInterface;
        private Mock<IInterface3> mockInterface3;
        private Mock<IInterface4> mockInterface4;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockSomeInterface = this.mockRepository.Create<ISomeInterface>();
            this.mockSomeOtherInterface = this.mockRepository.Create<ISomeOtherInterface>();
            this.mockInterface3 = this.mockRepository.Create<IInterface3>();
            this.mockInterface4 = this.mockRepository.Create<IInterface4>();
        }

        [TestMethod]
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
