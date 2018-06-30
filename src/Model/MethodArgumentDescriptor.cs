namespace UnitTestBoilerplate.Model
{
	public class MethodArgumentDescriptor
	{
		public MethodArgumentDescriptor(TypeDescriptor typeInformation, string argumentName)
		{
			TypeInformation = typeInformation;
			ArgumentName = argumentName;
		}

		public TypeDescriptor TypeInformation { get; }

		public string ArgumentName { get;  }
	}
}
