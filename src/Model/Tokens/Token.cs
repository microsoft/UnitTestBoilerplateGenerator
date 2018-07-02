using UnitTestBoilerplate.Model.Tokens.TestMethod;

namespace UnitTestBoilerplate.Model.Tokens
{
	internal abstract class Token
	{
		public static readonly TestMethodToken TestMethod = new TestMethodToken();

		public abstract string Identifier { get; }

		public override string ToString()
		{
			return Identifier;
		}

		public string ToWrappedToken()
		{
			return $"${this}$";
		}
	}
}
