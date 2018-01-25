using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace UnitTestBoilerplate.ViewModel
{
	public class FileLocationOptionsDialogViewModel : ViewModelBase
	{
		public FileLocationOptionsDialogViewModel()
		{
		}

		public void Initialize()
		{
			this.TestFileNameFormat = StaticBoilerplateSettings.FileNameTemplate;
		}

		public void Apply()
		{
			StaticBoilerplateSettings.FileNameTemplate = this.TestFileNameFormat;
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
