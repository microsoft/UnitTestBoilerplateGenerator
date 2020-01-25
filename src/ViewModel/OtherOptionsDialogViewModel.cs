using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
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
			get
			{
				return this.testFileNameFormat;
			}

			set
			{
				this.Set(ref this.testFileNameFormat, value);
			}
		}
	}
}
