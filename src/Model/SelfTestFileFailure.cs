using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate.Model
{
	public class SelfTestFileFailure
	{
		public string RelativeFilePath { get; set; }

		public string ExpectedFilePath { get; set; }

		public string ActualFilePath { get; set; }

		public string ExpectedContents { get; set; }

		public string ActualContents { get; set; }
	}
}
