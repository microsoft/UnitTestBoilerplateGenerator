using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestBoilerplate.Model
{
	public class TypeDescriptor
	{
		private TypeDescriptor()
		{
		}

		public TypeDescriptor(string fullTypeName)
		{
			// At the bottom is this descriptor. On top is the current descriptor we're adding GenericTypeArguments to.
			Stack<TypeDescriptor> descriptorStack = new Stack<TypeDescriptor>();
			descriptorStack.Push(this);

			StringBuilder currentTypeNameBuilder = new StringBuilder();

			for (int i = 0; i < fullTypeName.Length; i++)
			{
				char c = fullTypeName[i];

				if (char.IsWhiteSpace(c))
				{
					continue;
				}

				if (c == '<')
				{
					SetTypeName(fullTypeName, descriptorStack, currentTypeNameBuilder);
					currentTypeNameBuilder = new StringBuilder();

					var newTypeDescriptor = new TypeDescriptor();
					descriptorStack.Peek().GenericTypeArguments.Add(newTypeDescriptor);
					descriptorStack.Push(newTypeDescriptor);
				}
				else if (c == '>')
				{
					if (currentTypeNameBuilder != null)
					{
						SetTypeName(fullTypeName, descriptorStack, currentTypeNameBuilder);
						currentTypeNameBuilder = null;
					}

					// Last type is done, pop off the stack
					descriptorStack.Pop();
				}
				else if (c == ',')
				{
					SetTypeName(fullTypeName, descriptorStack, currentTypeNameBuilder);
					currentTypeNameBuilder = new StringBuilder();

					// Previous type is done, pop off the stack
					descriptorStack.Pop();

					// Get new type ready to fill in
					var newTypeDescriptor = new TypeDescriptor();
					descriptorStack.Peek().GenericTypeArguments.Add(newTypeDescriptor);
					descriptorStack.Push(newTypeDescriptor);
				}
				else
				{
					currentTypeNameBuilder.Append(c);
				}
			}

			// If we have anything left at the end, it's for this type.
			if (currentTypeNameBuilder != null)
			{
				string currentTypeName = currentTypeNameBuilder.ToString();
				if (!string.IsNullOrEmpty(currentTypeName))
				{
					this.SetNameAndNamespaceFromFullName(currentTypeName);
				}
			}
		}

		public TypeDescriptor(SymbolInfo symbolInfo, TypeSyntax typeSyntax, SyntaxKind nodeKind)
		{
			this.TypeSymbol = symbolInfo.Symbol as ITypeSymbol;
			switch (nodeKind)
			{
				case SyntaxKind.IdentifierName:
				case SyntaxKind.GenericName:
				case SyntaxKind.QualifiedName:
					this.TypeName = symbolInfo.Symbol.Name;
					break;
				default:
					this.TypeName = typeSyntax.ToString();
					break;
			}

			this.TypeNamespace = symbolInfo.Symbol.ContainingNamespace == null ? null : symbolInfo.Symbol.ContainingNamespace.ToString();
		}

		public TypeDescriptor(string name, string ns)
		{
			this.TypeName = name;
			this.TypeNamespace = ns;
		}

		private static void SetTypeName(string fullTypeName, Stack<TypeDescriptor> descriptorStack, StringBuilder currentTypeNameBuilder)
		{
			string currentTypeName = currentTypeNameBuilder.ToString();
			if (string.IsNullOrWhiteSpace(currentTypeName))
			{
				throw new ArgumentException("Could not find expected type name in " + fullTypeName);
			}

			descriptorStack.Peek().SetNameAndNamespaceFromFullName(currentTypeName);
		}

		private void SetNameAndNamespaceFromFullName(string fullNameWithoutGenerics)
		{
			int lastDotIndex = fullNameWithoutGenerics.LastIndexOf(".");
			this.TypeName = fullNameWithoutGenerics.Substring(lastDotIndex + 1);

			if (lastDotIndex < 0)
			{
				this.TypeNamespace = null;
			}
			else
			{
				this.TypeNamespace = fullNameWithoutGenerics.Substring(0, lastDotIndex);
			}
		}

		public override string ToString()
		{
			return TypeName;
		}

		/// <summary>
		/// The name of the type, like string or ISomeInterface .
		/// </summary>
		public string TypeName { get; private set; }

		/// <summary>
		/// The namespace for the type, or null if none is needed.
		/// </summary>
		public string TypeNamespace { get; private set; }

		public ITypeSymbol TypeSymbol { get; private set; }

		/// <summary>
		/// The list of child generic type arguments.
		/// </summary>
		public List<TypeDescriptor> GenericTypeArguments { get; } = new List<TypeDescriptor>();
	}
}
