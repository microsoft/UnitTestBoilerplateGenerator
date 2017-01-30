using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBoilerplate
{
	public class OptionsDialogViewModel : ViewModelBase
	{
		private Dictionary<string, string> templateHoldingDictionary = new Dictionary<string, string>();

		public void Initialize()
		{
			this.templateHoldingDictionary.Clear();
		}

		public void Apply()
		{
			foreach (KeyValuePair<string, string> pair in this.templateHoldingDictionary)
			{
				string[] keyParts = pair.Key.Split('_');

				string mockFrameworkString = keyParts[0];
				string templateTypeString = keyParts[1];

				StaticBoilerplateSettings.SetTemplate(mockFrameworkString, templateTypeString, pair.Value);
			}
		}

		private MockFramework mockFramework;
		public MockFramework MockFramework
		{
			get { return this.mockFramework; }
			set { this.Set(ref this.mockFramework); }
		}

		public string FileTemplate
		{
			get
			{
				return this.GetTemplate(this.MockFramework, TemplateType.File);
			}

			set
			{
				this.SaveTemplateToDialogHolding(this.MockFramework, TemplateType.File, value);
				this.RaisePropertyChanged();
				this.RaisePropertyChanged(nameof(this.MockFieldDeclarationTemplateVisible));
				this.RaisePropertyChanged(nameof(this.MockFieldInitializationTemplateVisible));
				this.RaisePropertyChanged(nameof(this.MockObjectReferenceTemplateVisible));
			}
		}

		public string MockFieldDeclarationTemplate
		{
			get { return this.GetTemplate(this.MockFramework, TemplateType.MockFieldDeclaration); }

			set
			{
				this.SaveTemplateToDialogHolding(this.MockFramework, TemplateType.MockFieldDeclaration, value);
				this.RaisePropertyChanged();
			}
		}

		public bool MockFieldDeclarationTemplateVisible
		{
			get { return this.FileTemplate.Contains("$MockFieldDeclarations$"); }
		}

		public string MockFieldInitializationTemplate
		{
			get { return this.GetTemplate(this.MockFramework, TemplateType.MockFieldInitialization); }

			set
			{
				this.SaveTemplateToDialogHolding(this.MockFramework, TemplateType.MockFieldInitialization, value);
				this.RaisePropertyChanged();
			}
		}

		public bool MockFieldInitializationTemplateVisible
		{
			get { return this.FileTemplate.Contains("$MockFieldInitializations$"); }
		}

		public string MockObjectReferenceTemplate
		{
			get { return this.GetTemplate(this.MockFramework, TemplateType.MockObjectReference); }

			set
			{
				this.SaveTemplateToDialogHolding(this.MockFramework, TemplateType.MockObjectReference, value);
				this.RaisePropertyChanged();
			}
		}

		public bool MockObjectReferenceTemplateVisible
		{
			get { return this.FileTemplate.Contains("$ExplicitConstructor$"); }
		}

		/// <summary>
		/// Gets the working copy of the template on the options dialog.
		/// </summary>
		/// <param name="mockFramework">The mock framework the template applies to.</param>
		/// <param name="templateType">The template type.</param>
		/// <returns>The working copy of the template on the options dialog.</returns>
		private string GetTemplate(MockFramework mockFramework, TemplateType templateType)
		{
			string template;
			if (this.templateHoldingDictionary.TryGetValue(GetDictionaryKey(mockFramework, templateType), out template))
			{
				return template;
			}

			return StaticBoilerplateSettings.GetTemplate(mockFramework, templateType);
		}

		/// <summary>
		/// Saves the template to the dialog holding area. It will take effect when Apply() is called.
		/// </summary>
		/// <param name="mockFramework">The mock framework the template applies to.</param>
		/// <param name="templateType">The template type.</param>
		/// <param name="template">The template to save.</param>
		private void SaveTemplateToDialogHolding(MockFramework mockFramework, TemplateType templateType, string template)
		{
			this.templateHoldingDictionary[GetDictionaryKey(mockFramework, templateType)] = template;
		}

		private static string GetDictionaryKey(MockFramework mockFramework, TemplateType templateType)
		{
			return $"{mockFramework}_{templateType}";
		}
	}
}
