namespace UnitTestBoilerplate.Model
{
	public class MethodDescriptor
	{
		public MethodDescriptor(string name, string[] methodParameterNames, bool isAsync)
		{
			Name = name;
			MethodParameterNames = methodParameterNames;
			IsAsync = isAsync;
		}

		public string Name { get; }

		public string[] MethodParameterNames { get; }

		public bool IsAsync { get; }
	}
}
