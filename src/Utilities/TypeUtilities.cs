using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using UnitTestBoilerplate.Model;

namespace UnitTestBoilerplate.Utilities
{
	public static class TypeUtilities
	{
		public static string GetTypeBaseName(string typeName)
		{
			if (IsInterfaceName(typeName))
			{
				return typeName.Substring(1);
			}

			return typeName;
		}

		/// <summary>
		/// Gets a component for a longer name given a type component.
		/// </summary>
		/// <param name="typeName">The name of a type, this might be an interface or type keyword that came as a generic
		/// type argument.</param>
		/// <returns>The PascalCased type name component.</returns>
		public static string GetTypeNameComponent(string typeName)
		{
			if (IsInterfaceName(typeName))
			{
				return typeName.Substring(1);
			}

			if (char.IsLower(typeName[0]))
			{
				return typeName.Substring(0, 1).ToUpperInvariant() + typeName.Substring(1);
			}

			return typeName;
		}

		private static bool IsInterfaceName(string typeName)
		{
			return typeName.Length >= 2 &&
			       typeName.StartsWith("I", StringComparison.Ordinal) &&
			       typeName[1] >= 'A' &&
			       typeName[1] <= 'Z';
		}

		public static bool TryGetParentSyntax<T>(SyntaxNode syntaxNode, out T result)
			where T : SyntaxNode
		{
			result = null;

			if (syntaxNode == null)
			{
				return false;
			}

			try
			{
				syntaxNode = syntaxNode.Parent;

				if (syntaxNode == null)
				{
					return false;
				}

				if (syntaxNode.GetType() == typeof(T))
				{
					result = syntaxNode as T;
					return true;
				}

				return TryGetParentSyntax<T>(syntaxNode, out result);
			}
			catch
			{
				return false;
			}
		}

		public static SymbolInfo? GetSymbolInfoFromParameterNode(SyntaxNode parameterNode, SemanticModel semanticModel, MockFramework mockFramework)
		{
			SyntaxNode node = parameterNode.ChildNodes().FirstOrDefault();
			SyntaxKind nodeKind = node.Kind();

			//if (nodeKind != SyntaxKind.IdentifierName && nodeKind != SyntaxKind.GenericName && nodeKind != SyntaxKind.PredefinedType && nodeKind != SyntaxKind.ArrayType && nodeKind != SyntaxKind.NullableType)
			//{
			//	return null;
			//}

			//if (nodeKind == SyntaxKind.GenericName && !mockFramework.SupportsGenerics)
			//{
			//	return null;
			//}

			return semanticModel.GetSymbolInfo(node);
		}
	}
}
