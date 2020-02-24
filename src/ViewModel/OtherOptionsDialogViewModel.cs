using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using UnitTestBoilerplate.Model;
using UnitTestBoilerplate.Services;

namespace UnitTestBoilerplate.ViewModel
{
	public class OtherOptionsDialogViewModel : ViewModelBase, ISettingsPageViewModel
	{
		private const string AutoName = "Auto";

		public OtherOptionsDialogViewModel()
		{
			this.TestFrameworkChoices = new List<TestFramework>(TestFrameworks.List);
			this.MockFrameworkChoices = new List<MockFramework>(MockFrameworks.List);

			this.TestFrameworkChoices.Insert(0, new TestFramework(AutoName));
			this.MockFrameworkChoices.Insert(0, new MockFramework(AutoName));
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

			this.TestFileNameFormat = this.settings.FileNameTemplate;
			TestFramework settingsPreferredTestFramework = this.settings.PreferredTestFramework;

			if (settingsPreferredTestFramework == null)
			{
				this.PreferredTestFramework = this.TestFrameworkChoices[0];
			}
			else
			{
				this.PreferredTestFramework = settingsPreferredTestFramework;
			}

			MockFramework settingsPreferredMockFramework = this.settings.PreferredMockFramework;

			if (settingsPreferredMockFramework == null)
			{
				this.PreferredMockFramework = this.MockFrameworkChoices[0];
			}
			else
			{
				this.PreferredMockFramework = settingsPreferredMockFramework;
			}

			this.CustomMocks.Clear();
			IDictionary<string, string> customMocks = this.settings.CustomMocks;
			if (customMocks != null)
			{
				foreach (var pair in customMocks)
				{
					this.CustomMocks.Add(new CustomMockViewModel { Interface = pair.Key, Class = pair.Value });
				}
			}

			this.CustomMocks.Add(new CustomMockViewModel { Interface = string.Empty, Class = string.Empty });

			this.CustomMockFieldDeclarationTemplate = this.settings.CustomMockFieldDeclarationTemplate ?? string.Empty;
			this.CustomMockFieldInitializationTemplate = this.settings.CustomMockFieldInitializationTemplate ?? string.Empty;
			this.CustomMockObjectReferenceTemplate = this.settings.CustomMockObjectReferenceTemplate ?? string.Empty;
		}

		public void Apply()
		{
			this.SaveCurrentSettings();
			this.settings.Apply();
		}

		public void SaveCurrentSettings()
		{
			this.settings.FileNameTemplate = this.TestFileNameFormat;

			if (this.PreferredTestFramework.Name == AutoName)
			{
				this.settings.PreferredTestFramework = null;
			}
			else
			{
				this.settings.PreferredTestFramework = this.PreferredTestFramework;
			}

			if (this.PreferredMockFramework.Name == AutoName)
			{
				this.settings.PreferredMockFramework = null;
			}
			else
			{
				this.settings.PreferredMockFramework = this.PreferredMockFramework;
			}

			var customMocksMap = new Dictionary<string, string>();

			foreach (CustomMockViewModel customMockViewModel in this.CustomMocks)
			{
				if (!string.IsNullOrWhiteSpace(customMockViewModel.Interface) && !string.IsNullOrWhiteSpace(customMockViewModel.Class))
				{
					customMocksMap.Add(customMockViewModel.Interface, customMockViewModel.Class);
				}
			}

			if (customMocksMap.Count > 0)
			{
				this.settings.CustomMocks = customMocksMap;
			}
			else
			{
				this.settings.CustomMocks = null;
			}

			this.settings.CustomMockFieldDeclarationTemplate = this.CustomMockFieldDeclarationTemplate;
			this.settings.CustomMockFieldInitializationTemplate = this.CustomMockFieldInitializationTemplate;
			this.settings.CustomMockObjectReferenceTemplate = this.CustomMockObjectReferenceTemplate;
		}

		public IList<TestFramework> TestFrameworkChoices { get; }


		private TestFramework preferredTestFramework;
		public TestFramework PreferredTestFramework
		{
			get { return this.preferredTestFramework; }

			set
			{
				if (value == null)
				{
					return;
				}

				this.Set(ref this.preferredTestFramework, value);
			}
		}

		public IList<MockFramework> MockFrameworkChoices { get; }

		private MockFramework preferredMockFramework;
		public MockFramework PreferredMockFramework
		{
			get { return this.preferredMockFramework; }

			set
			{
				if (value == null)
				{
					return;
				}

				this.Set(ref this.preferredMockFramework, value);
			}
		}

		private string testFileNameFormat;
		public string TestFileNameFormat
		{
			get => this.testFileNameFormat;
			set => this.Set(ref this.testFileNameFormat, value);
		}

		public ObservableCollection<CustomMockViewModel> CustomMocks { get; } = new ObservableCollection<CustomMockViewModel>();

		private string customMockFieldDeclarationTemplate;
		public string CustomMockFieldDeclarationTemplate
		{
			get => this.customMockFieldDeclarationTemplate;
			set => this.Set(ref this.customMockFieldDeclarationTemplate, value);
		}

		private string customMockFieldInitializationTemplate;
		public string CustomMockFieldInitializationTemplate
		{
			get => this.customMockFieldInitializationTemplate;
			set => this.Set(ref this.customMockFieldInitializationTemplate, value);
		}

		private string customMockObjectReferenceTemplate;
		public string CustomMockObjectReferenceTemplate
		{
			get => this.customMockObjectReferenceTemplate;
			set => this.Set(ref this.customMockObjectReferenceTemplate, value);
		}

		private RelayCommand addNewCustomMockCommand;
		public RelayCommand AddNewCustomMockCommand
		{
			get
			{
				return this.addNewCustomMockCommand ?? (this.addNewCustomMockCommand = new RelayCommand(
					() =>
					{
						this.CustomMocks.Add(new CustomMockViewModel { Interface = string.Empty, Class = string.Empty });
					}));
			}
		}
	}
}
