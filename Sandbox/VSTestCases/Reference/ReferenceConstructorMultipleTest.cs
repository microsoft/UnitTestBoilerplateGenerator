using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitBoilerplate.Sandbox.VSTestCases.Reference
{
    [TestClass]
    public class ReferenceConstructorMultipleTest
    {
        private MockRepository mockRepository;

        private Mock<ISomeInterface> mockSomeInterface;
        private Mock<ISomeOtherInterface> mockSomeOtherInterface;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockSomeInterface = this.mockRepository.Create<ISomeInterface>();
            this.mockSomeOtherInterface = this.mockRepository.Create<ISomeOtherInterface>();
        }

        [TestMethod]
        public void TestMethod1()
        {
            ConstructorInjectedClassMultiple viewModel = this.CreateViewModel();
        }

        private ConstructorInjectedClassMultiple CreateViewModel()
        {
            return new ConstructorInjectedClassMultiple(
                this.mockSomeInterface.Object,
                this.mockSomeOtherInterface.Object);
        }
    }
}
