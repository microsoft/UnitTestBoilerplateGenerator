using System.Collections.Generic;
using System.ComponentModel.Composition;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using UnitTestBoilerplate.Model;
using UnitTestBoilerplate.Services;

namespace UnitTestBoilerplate.ViewModel
{
	public class FileContentsOptionsDialogViewModel : ViewModelBase, ISettingsPageViewModel
	{
		private Dictionary<string, string> templateHoldingDictionary = new Dictionary<string, string>();

		public FileContentsOptionsDialogViewModel()
		{
			this.TestFrameworkChoices = TestFrameworks.List;
			this.MockFrameworkChoices = MockFrameworks.List;

			this.selectedTestFramework = TestFrameworks.Default;
			this.selectedMockFramework = MockFrameworks.Default;
		}

		[Import]
		internal IBoilerplateSettingsFactory SettingsFactory { get; set; }

		[Import]
		internal ISettingsCoordinator SettingsCoordinator { get; set; }

		private IBoilerplateSettings settings;

		public void Initialize()
		{
			this.settings = this.SettingsFactory.Get();
		}

		public void Refresh()
		{
			this.settings = this.SettingsFactory.Get();
			this.templateHoldingDictionary.Clear();
			this.RaiseAllChanged();
		}

		public void Apply()
		{
			this.SaveCurrentSettings();
			this.settings.Apply();
		}

		public void SaveCurrentSettings()
		{
			foreach (KeyValuePair<string, string> pair in this.templateHoldingDictionary)
			{
				string[] keyParts = pair.Key.Split('_');

				string testFrameworkString = keyParts[0];
				string mockFrameworkString = keyParts[1];
				string templateTypeString = keyParts[2];

				this.settings.SetTemplate(testFrameworkString, mockFrameworkString, templateTypeString, pair.Value);
			}
		}

		public IList<TestFramework> TestFrameworkChoices { get; }

		private TestFramework selectedTestFramework;
		public TestFramework SelectedTestFramework
		{
			get { return this.selectedTestFramework; }

			set
			{
				if (value == null)
				{
					return;
				}

				this.Set(ref this.selectedTestFramework, value);
				this.RaiseAllChanged();
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
			this.RaisePropertyChanged(nameof(this.ExtraUsingNamespaces));
			this.RaisePropertyChanged(nameof(this.MockFieldDeclarationTemplate));
			this.RaisePropertyChanged(nameof(this.MockFieldInitializationTemplate));
			this.RaisePropertyChanged(nameof(this.MockObjectReferenceTemplate));
			this.RaisePropertyChanged(nameof(this.MockFieldDeclarationTemplateVisible));
			this.RaisePropertyChanged(nameof(this.MockFieldInitializationTemplateVisible));
			this.RaisePropertyChanged(nameof(this.MockObjectReferenceTemplateVisible));
			this.RaisePropertyChanged(nameof(this.TestMethodNameTemplate));
			this.RaisePropertyChanged(nameof(this.TestMethodInvocationTemplate));
			this.RaisePropertyChanged(nameof(this.TestMethodEmptyTemplate));
			this.RaisePropertyChanged(nameof(this.TestMethodsVisible));
		}

		public string FileTemplate
		{
			get
			{
				return this.GetTemplate(TemplateType.File);
			}

			set
			{
				this.SaveTemplateToDialogHolding(this.SelectedTestFramework, this.SelectedMockFramework, TemplateType.File, value);
				this.RaisePropertyChanged();
				this.RaisePropertyChanged(nameof(this.MockFieldDeclarationTemplateVisible));
				this.RaisePropertyChanged(nameof(this.MockFieldInitializationTemplateVisible));
				this.RaisePropertyChanged(nameof(this.MockObjectReferenceTemplateVisible));
				this.RaisePropertyChanged(nameof(this.TestMethodsVisible));
			}
		}

		public string ExtraUsingNamespaces
		{
			get { return this.GetTemplate(TemplateType.ExtraUsingNamespaces); }

			set
			{
				this.SaveTemplateToDialogHolding(TemplateType.ExtraUsingNamespaces, value);
				this.RaisePropertyChanged();
			}
		}

		public string MockFieldDeclarationTemplate
		{
			get { return this.GetTemplate(TemplateType.MockFieldDeclaration); }

			set
			{
				this.SaveTemplateToDialogHolding(TemplateType.MockFieldDeclaration, value);
				this.RaisePropertyChanged();
			}
		}

		public bool MockFieldDeclarationTemplateVisible
		{
			get { return this.FileTemplate.Contains("$MockFieldDeclarations$"); }
		}

		public string MockFieldInitializationTemplate
		{
			get { return this.GetTemplate(TemplateType.MockFieldInitialization); }

			set
			{
				this.SaveTemplateToDialogHolding(TemplateType.MockFieldInitialization, value);
				this.RaisePropertyChanged();
			}
		}

		public bool MockFieldInitializationTemplateVisible
		{
			get { return this.FileTemplate.Contains("$MockFieldInitializations$"); }
		}

		public string MockObjectReferenceTemplate
		{
			get { return this.GetTemplate(TemplateType.MockObjectReference); }

			set
			{
				this.SaveTemplateToDialogHolding(TemplateType.MockObjectReference, value);
				this.RaisePropertyChanged();
			}
		}

		public bool MockObjectReferenceTemplateVisible
		{
			get { return this.FileTemplate.Contains("$ExplicitConstructor$"); }
		}

		public bool TestMethodsVisible
		{
			get { return this.FileTemplate.Contains("$TestMethods$"); }
		}

		public string TestMethodNameTemplate
		{
			get { return this.GetTemplate(TemplateType.TestMethodName); }

			set
			{
				this.SaveTemplateToDialogHolding(TemplateType.TestMethodName, value);
				this.RaisePropertyChanged();
			}
		}

		public string TestMethodInvocationTemplate
		{
			get { return this.GetTemplate(TemplateType.TestMethodInvocation); }

			set
			{
				this.SaveTemplateToDialogHolding(TemplateType.TestMethodInvocation, value);
				this.RaisePropertyChanged();
			}
		}

		public string TestMethodEmptyTemplate
		{
			get { return this.GetTemplate( TemplateType.TestMethodEmpty); }

			set
			{
				this.SaveTemplateToDialogHolding(TemplateType.TestMethodEmpty, value);
				this.RaisePropertyChanged();
			}
		}

		private RelayCommand resetCommand;
		public RelayCommand ResetCommand
		{
			get
			{
				return this.resetCommand ?? (this.resetCommand = new RelayCommand(
					() =>
					{
						TestFramework testFramework = this.SelectedTestFramework;
						MockFramework mockFramework = this.SelectedMockFramework;

						this.FileTemplate = new DefaultTemplateGenerator().Get(testFramework, mockFramework);
						this.ExtraUsingNamespaces = string.Empty;
						this.MockFieldDeclarationTemplate = mockFramework.MockFieldDeclarationCode;
						this.MockFieldInitializationTemplate = mockFramework.MockFieldInitializationCode;
						this.MockObjectReferenceTemplate = mockFramework.MockObjectReferenceCode;
						this.TestMethodNameTemplate = DefaultTemplateGenerator.TestMethodName;
						this.TestMethodInvocationTemplate = new DefaultTemplateGenerator().GetTestMethod(testFramework, mockFramework, invokeMethod: true);
						this.TestMethodEmptyTemplate = new DefaultTemplateGenerator().GetTestMethod(testFramework, mockFramework, invokeMethod: false);
					}));
			}
		}

		private RelayCommand openTokenListCommand;
		public RelayCommand OpenTokenListCommand
		{
			get
			{
				return this.openTokenListCommand ?? (this.openTokenListCommand = new RelayCommand(
					() =>
					{
						System.Diagnostics.Process.Start("https://github.com/Microsoft/UnitTestBoilerplateGenerator/wiki/Custom-Format-Tokens");
					}));
			}
		}

		/// <summary>
		/// Gets the working copy of the template on the options dialog.
		/// </summary>
		/// <param name="templateType">The template type.</param>
		/// <returns>The working copy of the template on the options dialog.</returns>
		private string GetTemplate(TemplateType templateType)
		{
			return this.GetTemplate(this.SelectedTestFramework, this.SelectedMockFramework, templateType);
		}

		/// <summary>
		/// Gets the working copy of the template on the options dialog.
		/// </summary>
		/// <param name="testFramework">The test framework the template applies to.</param>
		/// <param name="mockFramework">The mock framework the template applies to.</param>
		/// <param name="templateType">The template type.</param>
		/// <returns>The working copy of the template on the options dialog.</returns>
		private string GetTemplate(TestFramework testFramework, MockFramework mockFramework, TemplateType templateType)
		{
			string template;
			if (this.templateHoldingDictionary.TryGetValue(GetDictionaryKey(testFramework, mockFramework, templateType), out template))
			{
				return template;
			}

			return this.settings.GetTemplate(testFramework, mockFramework, templateType);
		}

		/// <summary>
		/// Saves the template to the dialog holding area. It will take effect when Apply() is called.
		/// </summary>
		/// <param name="templateType">The template type.</param>
		/// <param name="template">The template to save.</param>
		private void SaveTemplateToDialogHolding(TemplateType templateType, string template)
		{
			this.SaveTemplateToDialogHolding(this.SelectedTestFramework, this.SelectedMockFramework, templateType, template);
		}

		/// <summary>
		/// Saves the template to the dialog holding area. It will take effect when Apply() is called.
		/// </summary>
		/// <param name="testFramework">The test framework the template applies to.</param>
		/// <param name="mockFramework">The mock framework the template applies to.</param>
		/// <param name="templateType">The template type.</param>
		/// <param name="template">The template to save.</param>
		private void SaveTemplateToDialogHolding(TestFramework testFramework, MockFramework mockFramework, TemplateType templateType, string template)
		{
			this.templateHoldingDictionary[GetDictionaryKey(testFramework, mockFramework, templateType)] = template;
		}

		private static string GetDictionaryKey(TestFramework testFramework, MockFramework mockFramework, TemplateType templateType)
		{
			return $"{testFramework.Name}_{mockFramework.Name}_{templateType}";
		}
	}
}
