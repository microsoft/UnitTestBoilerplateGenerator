using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate
{
	/// <summary>
	/// Determines how code that runs after every test is executed.
	/// </summary>
	public enum TestCleanupStyle
	{
		Disposable,
		AttributedMethod
	}
}
