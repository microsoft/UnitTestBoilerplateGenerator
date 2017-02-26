using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate
{
    public class InjectableProperty : InjectableType
    {
        private InjectableProperty(string propertyName, string fullTypeString) : base(fullTypeString)
        {
            this.PropertyName = propertyName;
        }

        public string PropertyName { get; }

		public static InjectableProperty TryCreateInjectableProperty(string propertyName, string fullTypeString, MockFramework mockFramework)
		{
			if (!MockFrameworkAbstraction.SupportsGenerics(mockFramework) && fullTypeString.Contains("<"))
			{
				return null;
			}

			return new InjectableProperty(propertyName, fullTypeString);
		}
    }
}
