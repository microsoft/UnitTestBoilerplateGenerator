using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate.Services
{
	public interface IBoilerplateCache
	{
		void SaveSelectedTestProject(string solutionPath, string testProjectPath);
		string GetLastSelectedProject(string solutionPath);
	}
}
