using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate.Model
{
	public class BoilerplateSettingsJson
	{
		public int Version { get; set; }

		public string PreferredTestFrameworkName { get; set; }

		public string PreferredMockFrameworkName { get; set; }

		public string FileNameTemplate { get; set; }

		public IDictionary<string, string> Templates { get; set; } = new Dictionary<string, string>();
	}
}
