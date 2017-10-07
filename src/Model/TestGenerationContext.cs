using System.Collections.Generic;
using UnitTestBoilerplate.BasicModels;

namespace UnitTestBoilerplate.Model
{
	public class TestGenerationContext
	{
		public TestGenerationContext(
			MockFramework mockFramework,
			TestFramework testFramework,
			string unitTestNamespace, 
			string className, 
			string classNamespace,
			IList<InjectableProperty> properties,
			IList<InjectableType> constructorTypes,
			IList<InjectableType> injectedTypes)
		{
			this.MockFramework = mockFramework;
			this.TestFramework = testFramework;
			this.UnitTestNamespace = unitTestNamespace;
			this.ClassName = className;
			this.ClassNamespace = classNamespace;
			this.Properties = properties;
			this.ConstructorTypes = constructorTypes;
			this.InjectedTypes = injectedTypes;
		}

		public MockFramework MockFramework { get; }

		public TestFramework TestFramework { get; }

		public string UnitTestNamespace { get; }

		public string ClassName { get; }

		public string ClassNamespace { get; }

		public IList<InjectableProperty> Properties { get; }

		public IList<InjectableType> ConstructorTypes { get; }

		public IList<InjectableType> InjectedTypes { get; }
	}
}
