using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitBoilerplate.Sandbox.Classes.Cases
{
	public struct SomeStruct
	{
		private readonly int a;
		private readonly int b;

		public SomeStruct(int a, int b)
		{
			this.a = a;
			this.b = b;
		}

		public int GetValue(int c)
		{
			return a + b + c;
		}
	}
}
