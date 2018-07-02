using UnitTestBoilerplate.Model;
using UnitTestBoilerplate.Model.Tokens;

namespace UnitTestBoilerplate.Services.TokenEvaluation
{
	internal class TestedMethodNameTokenEvaluator : TokenEvaluator
	{
		public override Token Key => Token.TestMethod.MethodName;

		private readonly MethodDescriptor _methodDescriptor;

		public TestedMethodNameTokenEvaluator(MethodDescriptor methodDescriptor)
		{
			_methodDescriptor = methodDescriptor;
		}

		public override string Evaluate()
		{
			return _methodDescriptor.Name;
		}
	}
}
