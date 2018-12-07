using System.Collections.Generic;

namespace UnitTestBoilerplate.Model
{
	/// <summary>
	/// Holds all the data required to generate the test file. Includes information about the class to generate tests for, and the unit test project the test will be added to.
	/// </summary>
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
			IList<InjectableType> injectedTypes,
			IList<MethodDescriptor> methodDeclarations)
		{
			this.MockFramework = mockFramework;
			this.TestFramework = testFramework;
			this.UnitTestNamespace = unitTestNamespace;
			this.ClassName = className;
			this.ClassNamespace = classNamespace;
			this.Properties = properties;
			this.ConstructorTypes = constructorTypes;
			this.InjectedTypes = injectedTypes;
			this.MethodDeclarations = methodDeclarations;
		}

		public MockFramework MockFramework { get; }

		public TestFramework TestFramework { get; }

		public string UnitTestNamespace { get; }

		public string ClassName { get; }

		public string ClassNamespace { get; }

		public IList<InjectableProperty> Properties { get; }

		public IList<InjectableType> ConstructorTypes { get; }

		public IList<InjectableType> InjectedTypes { get; }

		public IList<MethodDescriptor> MethodDeclarations { get; }
	}
}
