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

		void ClearSettingsFileStore();

		bool UsingWorkspaceSettings { get; }

		string UserCreatedSettingsPath { get; set; }

		bool LoadUserCreatedSettings { get; }
	}
}
