namespace UnitTestBoilerplate.Model
{
	public class MethodDescriptor
	{
		public MethodDescriptor(string name, string[] methodParameterNames, bool isAsync, bool hasReturnType)
		{
			Name = name;
			MethodParameterNames = methodParameterNames;
			IsAsync = isAsync;
			HasReturnType = hasReturnType;
		}

		public string Name { get; }

		public string[] MethodParameterNames { get; }

		public bool IsAsync { get; }

		public bool HasReturnType { get; }
	}
}
