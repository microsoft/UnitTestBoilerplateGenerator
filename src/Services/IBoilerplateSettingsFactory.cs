using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate.Services
{
	public interface IBoilerplateSettingsFactory
	{
		IBoilerplateSettings Get();

		bool UsingWorkspaceSettings { get; }
	}
}
