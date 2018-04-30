using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using UnitTestBoilerplate.Model;

namespace UnitTestBoilerplate.ViewModel
{
	public class OtherOptionsDialogViewModel : ViewModelBase
	{
		private const string NoneName = "None";

		public OtherOptionsDialogViewModel()
		{
			this.TestFrameworkChoices = new List<TestFramework>(TestFrameworks.List);
			this.MockFrameworkChoices = new List<MockFramework>(MockFrameworks.List);

			this.TestFrameworkChoices.Insert(0, new TestFramework(NoneName));
			this.MockFrameworkChoices.Insert(0, new MockFramework(NoneName));
		}

		public void Initialize()
		{
			this.TestFileNameFormat = StaticBoilerplateSettings.FileNameTemplate;
			TestFramework preferredTestFramework = StaticBoilerplateSettings.PreferredTestFramework;

			if (preferredTestFramework == null)
			{
				this.PreferredTestFramework = this.TestFrameworkChoices[0];
			}
			else
			{
				this.PreferredTestFramework = preferredTestFramework;
			}

			MockFramework preferredMockFramework = StaticBoilerplateSettings.PreferredMockFramework;

			if (preferredMockFramework == null)
			{
				this.PreferredMockFramework = this.MockFrameworkChoices[0];
			}
			else
			{
				this.PreferredMockFramework = preferredMockFramework;
			}
		}

		public void Apply()
		{
			StaticBoilerplateSettings.FileNameTemplate = this.TestFileNameFormat;

			if (this.PreferredTestFramework.Name == NoneName)
			{
				StaticBoilerplateSettings.PreferredTestFramework = null;
			}
			else
			{
				StaticBoilerplateSettings.PreferredTestFramework = this.PreferredTestFramework;
			}

			if (this.PreferredMockFramework.Name == NoneName)
			{
				StaticBoilerplateSettings.PreferredMockFramework = null;
			}
			else
			{
				StaticBoilerplateSettings.PreferredMockFramework = this.PreferredMockFramework;
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
