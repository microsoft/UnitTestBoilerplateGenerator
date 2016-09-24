using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitBoilerplate.Sandbox.Classes.Cases
{
    public class ConstructorInjectedClassSingle
    {
        private readonly ISomeInterface someInterface;

        public ConstructorInjectedClassSingle(ISomeInterface someInterface)
        {
            this.someInterface = someInterface;
        }
    }
}
