using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace UnitTestBoilerplate
{
	public static class Extensions
	{
		public static IEnumerable<ITypeSymbol> GetBaseTypesAndThis(this ITypeSymbol type)
		{
			var current = type;
			while (current != null)
			{
				yield return current;
				current = current.BaseType;
			}
		}
	}
}
