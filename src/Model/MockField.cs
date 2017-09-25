namespace UnitTestBoilerplate.Model
{
	public class MockField
	{
		public MockField(string name, string typeName)
		{
			this.Name = name;
			this.TypeName = typeName;
		}

		public string Name { get; }

		public string TypeName { get; }
	}
}
