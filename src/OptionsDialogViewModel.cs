using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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

		public OptionsDialogViewModel()
		{
			this.MockFrameworkChoices = MockFrameworks.List;

			this.selectedMockFramework = MockFrameworks.Default;
		}

		public void Initialize()
		{
			this.templateHoldingDictionary.Clear();
			this.RaiseAllChanged();
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

		public IList<MockFramework> MockFrameworkChoices { get; }

		private MockFramework selectedMockFramework;
		public MockFramework SelectedMockFramework
		{
			get { return this.selectedMockFramework; }

			set
			{
				if (value == null)
				{
					return;
				}

				this.Set(ref this.selectedMockFramework, value);
				this.RaiseAllChanged();
			}
		}

		private void RaiseAllChanged()
		{
			this.RaisePropertyChanged(nameof(this.FileTemplate));
			this.RaisePropertyChanged(nameof(this.MockFieldDeclarationTemplate));
			this.RaisePropertyChanged(nameof(this.MockFieldInitializationTemplate));
			this.RaisePropertyChanged(nameof(this.MockObjectReferenceTemplate));
			this.RaisePropertyChanged(nameof(this.MockFieldDeclarationTemplateVisible));
			this.RaisePropertyChanged(nameof(this.MockFieldInitializationTemplateVisible));
			this.RaisePropertyChanged(nameof(this.MockObjectReferenceTemplateVisible));
		}

		public string FileTemplate
		{
			get
			{
				return this.GetTemplate(this.SelectedMockFramework, TemplateType.File);
			}

			set
			{
				this.SaveTemplateToDialogHolding(this.SelectedMockFramework, TemplateType.File, value);
				this.RaisePropertyChanged();
				this.RaisePropertyChanged(nameof(this.MockFieldDeclarationTemplateVisible));
				this.RaisePropertyChanged(nameof(this.MockFieldInitializationTemplateVisible));
				this.RaisePropertyChanged(nameof(this.MockObjectReferenceTemplateVisible));
			}
		}

		public string MockFieldDeclarationTemplate
		{
			get { return this.GetTemplate(this.SelectedMockFramework, TemplateType.MockFieldDeclaration); }

			set
			{
				this.SaveTemplateToDialogHolding(this.SelectedMockFramework, TemplateType.MockFieldDeclaration, value);
				this.RaisePropertyChanged();
			}
		}

		public bool MockFieldDeclarationTemplateVisible
		{
			get { return this.FileTemplate.Contains("$MockFieldDeclarations$"); }
		}

		public string MockFieldInitializationTemplate
		{
			get { return this.GetTemplate(this.SelectedMockFramework, TemplateType.MockFieldInitialization); }

			set
			{
				this.SaveTemplateToDialogHolding(this.SelectedMockFramework, TemplateType.MockFieldInitialization, value);
				this.RaisePropertyChanged();
			}
		}

		public bool MockFieldInitializationTemplateVisible
		{
			get { return this.FileTemplate.Contains("$MockFieldInitializations$"); }
		}

		public string MockObjectReferenceTemplate
		{
			get { return this.GetTemplate(this.SelectedMockFramework, TemplateType.MockObjectReference); }

			set
			{
				this.SaveTemplateToDialogHolding(this.SelectedMockFramework, TemplateType.MockObjectReference, value);
				this.RaisePropertyChanged();
			}
		}

		public bool MockObjectReferenceTemplateVisible
		{
			get { return this.FileTemplate.Contains("$ExplicitConstructor$"); }
		}

		private RelayCommand resetCommand;
		public RelayCommand ResetCommand
		{
			get
			{
				return this.resetCommand ?? (this.resetCommand = new RelayCommand(
					() =>
					{
						MockFramework mockFramework = this.SelectedMockFramework;

						this.FileTemplate = StaticBoilerplateSettings.GetDefaultTemplate(mockFramework, TemplateType.File);
						this.MockFieldDeclarationTemplate = StaticBoilerplateSettings.GetDefaultTemplate(mockFramework, TemplateType.MockFieldDeclaration);
						this.MockFieldInitializationTemplate = StaticBoilerplateSettings.GetDefaultTemplate(mockFramework, TemplateType.MockFieldInitialization);
						this.MockObjectReferenceTemplate = StaticBoilerplateSettings.GetDefaultTemplate(mockFramework, TemplateType.MockObjectReference);
					}));
			}
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
			return $"{mockFramework.Name}_{templateType}";
		}
	}
}
