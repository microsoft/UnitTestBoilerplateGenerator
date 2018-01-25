using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate.Utilities
{
	public static class FileUtilities
	{
		private static readonly List<string> DisallowedCharacters = new List<string> { "\\", "/", "\"", ":", "*", "?", "<", ">", "|" };

		public static string CleanFileName(string fileName)
		{
			string cleanName = fileName;
			foreach (string disallowedChar in DisallowedCharacters)
			{
				cleanName = cleanName.Replace(disallowedChar, "_");
			}

			return cleanName;
		}
	}
}
