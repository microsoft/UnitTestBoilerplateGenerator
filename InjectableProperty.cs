using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate
{
    public class InjectableProperty
    {
        public InjectableProperty(string name, string typeName)
        {
            this.Name = name;
            this.TypeName = typeName;
        }

        public string Name { get; }

        public string TypeName { get; }

        public string TypeBaseName => Utilities.GetTypeBaseName(this.TypeName);
    }
}
