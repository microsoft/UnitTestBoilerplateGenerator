using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestBoilerplate.Services;

namespace UnitTestBoilerplate.Model
{
	public interface ISettingsPageViewModel
	{
		void Refresh();

		void SaveCurrentSettings();
	}
}
