using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate.Model
{
	public class SelfTestInputCombination
	{
		public TestFramework TestFramework { get; set; }

		public MockFramework MockFramework { get; set; }

		public string ClassName { get; set; }
	}
}
