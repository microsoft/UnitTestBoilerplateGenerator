using System.Collections.Generic;
using UnitTestBoilerplate.Model;

namespace UnitTestBoilerplate.Services
{
	public interface IFrameworkPickerService
	{
		List<TestFramework> FindTestFrameworks(string projectFileName);
		TestFramework PickDefaultTestFramework(IList<TestFramework> frameworks);
		TestFramework FindTestFramework(string projectFileName);
		List<MockFramework> FindMockFrameworks(string projectFileName);
		MockFramework PickDefaultMockFramework(IList<MockFramework> frameworks);
		MockFramework FindMockFramework(string projectFileName);
	}
}