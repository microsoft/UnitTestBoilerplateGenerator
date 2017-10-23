using System;
using System.Text;

namespace UnitTestBoilerplate.Utilities
{
	public static class StringUtilities
	{
		public static string ReplaceTokens(string template, Action<string, int, StringBuilder> tokenReplacementAction)
		{
			var builder = new StringBuilder();

			for (int i = 0; i < template.Length; i++)
			{
				char c = template[i];
				if (c == '$')
				{
					int endIndex = -1;
					for (int j = i + 1; j < template.Length; j++)
					{
						if (template[j] == '$')
						{
							endIndex = j;
							break;
						}
					}

					if (endIndex < 0)
					{
						// We couldn't find the end index for the replacement property name. Continue.
						builder.Append(c);
					}
					else
					{
						// Calculate values on demand from switch statement. Some are preset values, some need a bit of calc like base name,
						// some are dependent on the test framework (attributes), some need to pull down other templates and loop through mock fields
						string tokenText = template.Substring(i + 1, endIndex - i - 1);

						int periodIndex = tokenText.IndexOf(".", StringComparison.Ordinal);
						if (periodIndex > 0)
						{
							string modifier = tokenText.Substring(periodIndex + 1);
							string tokenName = tokenText.Substring(0, periodIndex);

							switch (modifier)
							{
								case "CamelCase":
									RunCamelCaseReplacement(tokenName, tokenReplacementAction, i, builder);
									break;

								default:
									// Ignore the modifier
									tokenReplacementAction(tokenText, i, builder);
									break;
							}
						}
						else
						{
							tokenReplacementAction(tokenText, i, builder);
						}

						i = endIndex;
					}
				}
				else
				{
					builder.Append(c);
				}
			}

			return builder.ToString();
		}

		private static void RunCamelCaseReplacement(string tokenName, Action<string, int, StringBuilder> tokenReplacementAction, int propertyIndex, StringBuilder builder)
		{
			var tokenValueBuilder = new StringBuilder();

			tokenReplacementAction(tokenName, propertyIndex, tokenValueBuilder);
			string tokenValue = tokenValueBuilder.ToString();

			builder.Append(tokenValue.Substring(0, 1).ToLowerInvariant() + tokenValue.Substring(1));
		}
	}
}
