using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using UnitBoilerplate.Sandbox.Classes;
using UnitBoilerplate.Sandbox.Classes.Cases;

namespace UnitBoilerplate.Sandbox.VSTestCases.Reference
{
    [TestClass]
    public class ReferencePropertyMultipleTest
    {
        private MockRepository mockRepository;

        private Mock<ISomeInterface> mockSomeInterface;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockSomeInterface = this.mockRepository.Create<ISomeInterface>();
        }

        [TestMethod]
        public void TestMethod1()
        {
            PropertyInjectedClassMultiple viewModel = this.CreateViewModel();
        }

        private PropertyInjectedClassMultiple CreateViewModel()
        {
            return new PropertyInjectedClassMultiple
            {
                MyProperty = this.mockSomeInterface.Object
            };
        }
    }
}
