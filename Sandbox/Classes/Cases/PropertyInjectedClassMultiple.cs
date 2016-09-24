using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace UnitBoilerplate.Sandbox.Classes.Cases
{
    public class PropertyInjectedClassMultiple
    {
        [Dependency]
        public ISomeInterface MyProperty { protected get; set; }

        [Dependency]
        public ISomeOtherInterface Property2 { protected get; set; }
    }
}
