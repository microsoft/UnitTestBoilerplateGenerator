using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using UnitTestBoilerplate.Utilities;

namespace UnitTestBoilerplate.Model
{
	/// <summary>
	/// Represents a type being injected into a class that we need to create mocks for.
	/// </summary>
	public class InjectableType : TypeDescriptor
	{
		public InjectableType(string fullTypeString)
			: base(fullTypeString)
		{
		}

		private InjectableType(string typeName, string ns)
			: base(typeName, ns)
		{

		}

		public static InjectableType TryCreateInjectableTypeFromParameterNode(SyntaxNode parameterNode, SemanticModel semanticModel, MockFramework mockFramework)
		{
			SyntaxNode node = parameterNode.ChildNodes().FirstOrDefault();
			SyntaxKind nodeKind = node.Kind();

			if (nodeKind != SyntaxKind.IdentifierName && nodeKind != SyntaxKind.GenericName)
			{
				return null;
			}

			if (nodeKind == SyntaxKind.GenericName && !mockFramework.SupportsGenerics)
			{
				return null;
			}

			SymbolInfo symbolInfo = semanticModel.GetSymbolInfo(node);
		    var namedSymbol = symbolInfo.Symbol as INamedTypeSymbol;
		    if (namedSymbol != null && !namedSymbol.IsReferenceType)
		    {
				// We can only mock reference types
				return null;
		    }

		    return CreateInjectableType(node, semanticModel, symbolInfo);
		}

		private static InjectableType CreateInjectableType(SyntaxNode node, SemanticModel semanticModel, SymbolInfo? symbolInfo = null)
		{
			switch (node.Kind())
			{
				case SyntaxKind.IdentifierName:
			        if (symbolInfo == null)
			        {
						symbolInfo = semanticModel.GetSymbolInfo(node);
			        }

					return new InjectableType(symbolInfo.Value.Symbol.Name, symbolInfo.Value.Symbol.ContainingNamespace.ToString());
				case SyntaxKind.GenericName:
					if (symbolInfo == null)
					{
						symbolInfo = semanticModel.GetSymbolInfo(node);
					}

					var injectableType = new InjectableType(symbolInfo.Value.Symbol.Name, symbolInfo.Value.Symbol.ContainingNamespace.ToString());

					SyntaxNode typeArgumentNode = node.ChildNodes().FirstOrDefault(n => n.Kind() == SyntaxKind.TypeArgumentList);
					if (typeArgumentNode == null)
					{
						throw new ArgumentException("Could not find type argument node for GenericName node.");
					}

					foreach (SyntaxNode genericArgumentNode in typeArgumentNode.ChildNodes())
					{
						injectableType.GenericTypeArguments.Add(CreateInjectableType(genericArgumentNode, semanticModel));
					}

					return injectableType;
				case SyntaxKind.PredefinedType:
				default:
					return new InjectableType(node.ToString(), null);

			}
		}

		public string TypeBaseName => TypeUtilities.GetTypeBaseName(this.TypeName);

		public string LongMockName
		{
			get
			{
				var result = new StringBuilder();
				AddComponentsToMockName(result, this);
				return result.ToString();
			}
		}

		private static void AddComponentsToMockName(StringBuilder builder, TypeDescriptor typeDescriptor)
		{
			builder.Append(TypeUtilities.GetTypeNameComponent(typeDescriptor.TypeName));

			foreach (TypeDescriptor genericArgumentTypeDescriptor in typeDescriptor.GenericTypeArguments)
			{
				AddComponentsToMockName(builder, genericArgumentTypeDescriptor);
			}
		}

		public IList<string> TypeNamespaces
		{
			get
			{
				var result = new List<string>();
				AddNamespacesToList(result, this);
				return result;
			}
		}

		private static void AddNamespacesToList(IList<string> namespaceList, TypeDescriptor typeDescriptor)
		{
			if (!string.IsNullOrEmpty(typeDescriptor.TypeNamespace))
			{
				namespaceList.Add(typeDescriptor.TypeNamespace);
			}

			foreach (TypeDescriptor genericTypeArgument in typeDescriptor.GenericTypeArguments)
			{
				AddNamespacesToList(namespaceList, genericTypeArgument);
			}
		}

		/// <summary>
		/// Gets or sets the PascalCased, unique mock name for this type.
		/// </summary>
		public string MockName { get; set; }

		public override string ToString()
		{
			var result = new StringBuilder();
			WriteToStringBuilder(result, this);
			return result.ToString();
		}

		private static void WriteToStringBuilder(StringBuilder builder, TypeDescriptor typeDescriptor)
		{
			builder.Append(typeDescriptor.TypeName);

			if (typeDescriptor.GenericTypeArguments.Any())
			{
				builder.Append("<");

				for (int i = 0; i < typeDescriptor.GenericTypeArguments.Count; i++)
				{
					TypeDescriptor genericTypeArgument = typeDescriptor.GenericTypeArguments[i];

					WriteToStringBuilder(builder, genericTypeArgument);
					if (i < typeDescriptor.GenericTypeArguments.Count - 1)
					{
						builder.Append(", ");
					}
				}

				builder.Append(">");
			}
		}
	}
}
