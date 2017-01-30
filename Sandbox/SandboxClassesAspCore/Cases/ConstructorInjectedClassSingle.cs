using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitBoilerplate.Sandbox.Classes.AspCore.Cases
{
    public class ConstructorInjectedClassSingle
    {
        private readonly ISomeInterface2 someInterface2;

        public ConstructorInjectedClassSingle(ISomeInterface2 someInterface2)
        {
            this.someInterface2 = someInterface2;
        }
    }
}
