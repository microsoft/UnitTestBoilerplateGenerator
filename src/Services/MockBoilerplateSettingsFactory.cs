using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate.Services
{
	public class MockBoilerplateSettingsFactory : IBoilerplateSettingsFactory
	{
		private readonly IBoilerplateSettings settings;

		public MockBoilerplateSettingsFactory(IBoilerplateSettings settings)
		{
			this.settings = settings;
		}

		public IBoilerplateSettings Get()
		{
			return this.settings;
		}

		public void ClearWorkspaceStore()
		{
		}

		public bool UsingWorkspaceSettings => false;

		public string UserCreatedSettingsPath { get; set; }

		public bool LoadUserCreatedSettings { get; set; }
	}
}
