using EnvDTE;
using System.Collections.Generic;
using UnitTestBoilerplate.Model;

namespace UnitTestBoilerplate.Services
{
	public interface IFrameworkPickerService
	{
		List<TestFramework> FindTestFrameworks(Project project);
		TestFramework PickDefaultTestFramework(IList<TestFramework> frameworks, IBoilerplateSettings settings);
		TestFramework FindTestFramework(Project project, IBoilerplateSettings settings);
		List<MockFramework> FindMockFrameworks(Project project);
		MockFramework PickDefaultMockFramework(IList<MockFramework> frameworks, IBoilerplateSettings settings);
		MockFramework FindMockFramework(Project project, IBoilerplateSettings settings);
	}
}