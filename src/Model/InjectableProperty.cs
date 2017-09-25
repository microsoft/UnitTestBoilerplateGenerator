namespace UnitTestBoilerplate.Model
{
	public class InjectableProperty : InjectableType
	{
		private InjectableProperty(string propertyName, string fullTypeString) : base(fullTypeString)
		{
			this.PropertyName = propertyName;
		}

		public string PropertyName { get; }

		public static InjectableProperty TryCreateInjectableProperty(string propertyName, string fullTypeString, MockFramework mockFramework)
		{
			if (!mockFramework.SupportsGenerics && fullTypeString.Contains("<"))
			{
				return null;
			}

			return new InjectableProperty(propertyName, fullTypeString);
		}
	}
}
