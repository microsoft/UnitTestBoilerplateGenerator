using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitBoilerplate.Sandbox.Classes.Cases
{
    public class DerivedPropertyInjectedClass : PropertyInjectedClassMultiple
    {
        [Dependency]
        public IInterface3 Interface3Property { protected get; set; }
    }
}
