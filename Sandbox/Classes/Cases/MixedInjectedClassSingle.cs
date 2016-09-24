using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace UnitBoilerplate.Sandbox.Classes.Cases
{
    public class MixedInjectedClassSingle
    {
        private readonly ISomeInterface someInterface;

        public MixedInjectedClassSingle(ISomeInterface someInterface)
        {
            this.someInterface = someInterface;
        }

        [Dependency]
        public IInterface3 Interface3Property { get; set; }
    }
}
