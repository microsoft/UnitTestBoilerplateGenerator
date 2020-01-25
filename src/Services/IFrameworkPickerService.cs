using System.Collections.Generic;
using UnitTestBoilerplate.Model;

namespace UnitTestBoilerplate.Services
{
	public interface IFrameworkPickerService
	{
		List<TestFramework> FindTestFrameworks(string projectFileName);
		TestFramework PickDefaultTestFramework(IList<TestFramework> frameworks, IBoilerplateSettings settings);
		TestFramework FindTestFramework(string projectFileName, IBoilerplateSettings settings);
		List<MockFramework> FindMockFrameworks(string projectFileName);
		MockFramework PickDefaultMockFramework(IList<MockFramework> frameworks, IBoilerplateSettings settings);
		MockFramework FindMockFramework(string projectFileName, IBoilerplateSettings settings);
	}
}