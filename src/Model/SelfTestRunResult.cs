using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate.Model
{
	public class SelfTestRunResult
	{
		public int TotalFilesCount { get; set; }

		public int FilesSucceededCount { get; set; }

		public IList<SelfTestFileFailure> FileFailures { get; set; }

		public int TotalDetectionsCount { get; set; }

		public int DetectionsSucceededCount { get; set; }

		public IList<string> DetectionFailures { get; set; }
	}
}
