using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitBoilerplate.Sandbox.Classes.Cases
{
    public class ConstructorInjectedClassMultiple
    {
        private readonly ISomeInterface someInterface;
        private readonly ISomeOtherInterface someOtherInterface;

        public ConstructorInjectedClassMultiple(ISomeInterface someInterface, ISomeOtherInterface someOtherInterface)
        {
            this.someInterface = someInterface;
            this.someOtherInterface = someOtherInterface;
        }
    }
}
