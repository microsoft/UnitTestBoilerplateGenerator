using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace UnitBoilerplate.Sandbox.Classes.Cases
{
    public class PropertyInjectedClassSingle
    {
        [Dependency]
        public ISomeInterface MyProperty { protected get; set; }
    }
}
