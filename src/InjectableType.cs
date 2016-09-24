using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate
{
    public class InjectableType
    {
        public InjectableType(string typeName, string typeNamespace)
        {
            this.TypeName = typeName;
            this.TypeNamespace = typeNamespace;
        }

        public string TypeName { get; }

        public string TypeNamespace { get; }

        public string TypeBaseName => Utilities.GetTypeBaseName(this.TypeName);
    }
}
