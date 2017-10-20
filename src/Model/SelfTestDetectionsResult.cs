using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate.Model
{
	public class SelfTestDetectionsResult
	{
		public int TotalCount { get; set; }

		public int SucceededCount { get; set; }

		public IList<string> Failures { get; set; }
	}
}
