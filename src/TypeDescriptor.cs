using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate
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

			//string fullNameWithoutGenerics;
			//if (fullTypeName.Contains("<"))
			//{
			//	int leftAngleIndex = fullTypeName.IndexOf("<");
			//	int rightAngleIndex = fullTypeName.IndexOf(">");

			//	if (rightAngleIndex < 0)
			//	{
			//		throw new ArgumentException("Could not find closing angle bracket for type name " + fullTypeName);
			//	}

			//	fullNameWithoutGenerics = fullTypeName.Substring(0, leftAngleIndex);


			//}
			//else
			//{
			//	fullNameWithoutGenerics = fullTypeName;
			//}

			//int lastDotIndex = fullNameWithoutGenerics.LastIndexOf(".");
			//this.Name = fullNameWithoutGenerics.Substring(lastDotIndex + 1);
			//this.Namespace = fullNameWithoutGenerics.Substring(0, lastDotIndex);
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

		public TypeDescriptor(string name, string ns)
		{
			this.TypeName = name;
			this.TypeNamespace = ns;
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

		/// <summary>
		/// The name of the type, like string or ISomeInterface .
		/// </summary>
		public string TypeName { get; set; }

		/// <summary>
		/// The namespace for the type, or null if none is needed.
		/// </summary>
		public string TypeNamespace { get; set; }

		/// <summary>
		/// The list of child generic type arguments.
		/// </summary>
		public List<TypeDescriptor> GenericTypeArguments { get; } = new List<TypeDescriptor>();
	}
}
