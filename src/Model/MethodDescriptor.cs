namespace UnitTestBoilerplate.Model
{
	public class MethodDescriptor
	{
		public MethodDescriptor(string name, string[] methodParameterNames)
		{
			Name = name;
			MethodParameterNames = methodParameterNames;
		}

		public string Name { get; }

		public string[] MethodParameterNames { get; }
	}
}
