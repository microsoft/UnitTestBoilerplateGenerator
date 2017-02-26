using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace UnitBoilerplate.Sandbox.Classes.Cases
{
	public class ClassWithGenericInterface
	{
		public ClassWithGenericInterface(IGenericInterface<bool> theGenericInterface, IGenericInterface<List<string>> theOtherGenericInterface, ISomeInterface someInterface)
		{
		}

		[Dependency]
		public IInterface3 Interface2 { get; set; }

		[Dependency]
		public IGenericInterface<List<int>> GenericInterface3 { get; set; }

		[Dependency]
		public IGenericInterface<List<ISomeOtherInterface>> GenericInterface4 { get; set; }
	}
}
