using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace UnitBoilerplate.Sandbox.Classes.Cases
{
    public class MixedInjectedClassMultiple
    {
        private readonly ISomeInterface someInterface;
        private readonly ISomeOtherInterface someOtherInterface;

        public MixedInjectedClassMultiple(ISomeInterface someInterface, ISomeOtherInterface someOtherInterface)
        {
            this.someInterface = someInterface;
            this.someOtherInterface = someOtherInterface;
        }

        [Dependency]
        public IInterface3 Interface3Property { get; set; }

        [Dependency]
        public IInterface4 Interface4Property { get; set; }
    }
}
