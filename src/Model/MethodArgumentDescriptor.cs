namespace UnitTestBoilerplate.Model
{
	public class MethodArgumentDescriptor
	{
		public MethodArgumentDescriptor(TypeDescriptor typeInformation, string argumentName, ParameterModifier modifier)
		{
			TypeInformation = typeInformation;
			ArgumentName = argumentName;
			Modifier = modifier;
		}

		public TypeDescriptor TypeInformation { get; }

		public string ArgumentName { get; }

		public ParameterModifier Modifier { get; }
	}

	public enum ParameterModifier
	{
		None,

		Out,

		Ref
	}
}
