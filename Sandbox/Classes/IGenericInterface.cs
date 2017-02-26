using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitBoilerplate.Sandbox.Classes
{
	public interface IGenericInterface<T>
	{
		T GetAThing();
	}
}
