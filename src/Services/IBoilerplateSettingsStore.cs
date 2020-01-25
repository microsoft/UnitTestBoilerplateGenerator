using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestBoilerplate.Model;

namespace UnitTestBoilerplate.Services
{
	/// <summary>
	/// A store for UTBG settings. Does not provide defaults, just abstracts storage of user-specified values.
	/// </summary>
	public interface IBoilerplateSettingsStore
	{
		string PreferredTestFrameworkName { get; set; }
		string PreferredMockFrameworkName { get; set; }
		string FileNameTemplate { get; set; }
		string GetTemplate(string templateSettingsKey);
		void SetTemplate(string templateSettingsKey, string template);
		void RevertTemplateToDefault(TestFramework testFramework, MockFramework mockFramework);
		void Apply();
	}
}
