using UnitTestBoilerplate.Model.Tokens;

namespace UnitTestBoilerplate.Services.TokenEvaluation
{
	internal abstract class TokenEvaluator
	{
		public abstract Token Key { get; }

		public abstract string Evaluate();

		public bool CanExecute(string token)
		{
			return token == Key.Identifier;
		}
	}
}
