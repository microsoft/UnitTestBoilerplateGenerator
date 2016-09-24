using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate
{
    public class InjectableProperty : InjectableType
    {
        public InjectableProperty(string name, string typeName, string typeNamespace) : base(typeName, typeNamespace)
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}
