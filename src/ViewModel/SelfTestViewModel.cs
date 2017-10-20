using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using EnvDTE;
using EnvDTE80;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.VisualStudio.Shell;
using UnitTestBoilerplate.Commands;
using UnitTestBoilerplate.Model;
using UnitTestBoilerplate.Services;
using UnitTestBoilerplate.Utilities;
using Task = System.Threading.Tasks.Task;

namespace UnitTestBoilerplate.ViewModel
{
	public class SelfTestViewModel : ViewModelBase
	{
		private readonly DTE2 dte;
		private IList<Project> projects;

		private IList<SelfTestFileFailure> failures;

		public SelfTestViewModel()
		{
			this.dte = (DTE2)ServiceProvider.GlobalProvider.GetService(typeof(DTE));
			this.projects = SolutionUtilities.GetProjects(this.dte);
			this.status = "Hello";

			this.InitializeAsync();
		}

		private bool showingDiff;
		public bool ShowingDiff
		{
			get { return this.showingDiff; }
			set { this.Set(ref this.showingDiff, value); }
		}

		private string status;
		public string Status
		{
			get { return this.status; }
			set { this.Set(ref this.status, value); }
		}

		private string relativeFilePath;
		public string RelativeFilePath
		{
			get { return this.relativeFilePath; }
			set { this.Set(ref this.relativeFilePath, value); }
		}

		private string expectedText;
		public string ExpectedText
		{
			get { return this.expectedText; }
			set { this.expectedText = value; Compare(); }
		}

		private string actualText;
		public string ActualText
		{
			get { return this.actualText; }
			set { this.actualText = value; Compare(); }
		}

		public SideBySideDiffModel Diff { get; private set; }

		private int failureIndex;
		public int FailureIndex
		{
			get { return this.failureIndex; }
			set
			{
				if (this.Set(ref this.failureIndex, value))
				{
					this.RefreshUI();
				}
			}
		}

		private int succeededCount;

		private void RefreshUI()
		{
			this.UpdateFailedStatusText();
			this.SucceededStatusText = this.succeededCount + " succeeded";
			this.PreviousCommand.RaiseCanExecuteChanged();
			this.NextCommand.RaiseCanExecuteChanged();
			this.AcceptCommand.RaiseCanExecuteChanged();

			if (this.failures.Count > 0)
			{
				SelfTestFileFailure failure = this.failures[this.FailureIndex];

				this.RelativeFilePath = failure.RelativeFilePath;
				this.expectedText = failure.ExpectedContents;
				this.actualText = failure.ActualContents;
			}

			this.Compare();
		}

		private void UpdateFailedStatusText()
		{
			this.FailedStatusText = (this.FailureIndex + 1) + "/" + this.failures.Count + " failed";
		}

		private string failedStatusText;
		public string FailedStatusText
		{
			get { return this.failedStatusText; }
			set { this.Set(ref this.failedStatusText, value); }
		}

		private string succeededStatusText;
		public string SucceededStatusText
		{
			get { return this.succeededStatusText; }
			set { this.Set(ref this.succeededStatusText, value); }
		}

		private RelayCommand previousCommand;
		public RelayCommand PreviousCommand
		{
			get
			{
				return this.previousCommand ?? (this.previousCommand = new RelayCommand(
					() =>
					{
						this.FailureIndex--;
					},
					() =>
					{
						return this.failures != null && this.FailureIndex > 0;
					}));
			}
		}

		private RelayCommand nextCommand;
		public RelayCommand NextCommand
		{
			get
			{
				return this.nextCommand ?? (this.nextCommand = new RelayCommand(
					() =>
					{
						this.FailureIndex++;
					},
					() =>
					{
						return this.failures != null && this.FailureIndex < this.failures.Count - 1;
					}));
			}
		}

		private RelayCommand acceptCommand;
		public RelayCommand AcceptCommand
		{
			get
			{
				return this.acceptCommand ?? (this.acceptCommand = new RelayCommand(
					() =>
					{
						// Copy file to accepted
						SelfTestFileFailure failure = this.failures[this.FailureIndex];
						File.Copy(failure.ActualFilePath, failure.ExpectedFilePath, overwrite: true);

						this.succeededCount++;

						this.failures.RemoveAt(this.FailureIndex);

						if (this.failures.Count == 0)
						{
							// Succeeded UI
						}
						else if (this.FailureIndex >= this.failures.Count)
						{
							this.failureIndex--;
						}

						this.RefreshUI();
					},
					() =>
					{
						return this.failures != null && this.failures.Count > 0;
					}));
			}
		}

		private async Task InitializeAsync()
		{
			var createTestService = new SelfTestService();

			createTestService.Clean(this.projects);

			await createTestService.GenerateTestFilesAsync(this.projects);

			string solutionDirectory = Path.GetDirectoryName(this.dte.Solution.FileName);
			string expectedResultsDirectory = Path.Combine(solutionDirectory, "SelfTestExpectedResults");

			// Go over all "*TestCases" folders in actual results folder, dig into "Cases" directory
			int totalFiles = 0;
			this.succeededCount = 0;

			this.failures = new List<SelfTestFileFailure>();

			var resultsInfo = new DirectoryInfo(solutionDirectory);
			foreach (DirectoryInfo projectDirectoryInfo in resultsInfo.GetDirectories("*TestCases"))
			{
				DirectoryInfo casesDirectoryInfo = new DirectoryInfo(Path.Combine(projectDirectoryInfo.FullName, "Cases"));

				foreach (FileInfo actualResultFileInfo in casesDirectoryInfo.GetFiles())
				{
					totalFiles++;

					string actualFileContents = File.ReadAllText(actualResultFileInfo.FullName);
					string relativeFilePath = Path.Combine(projectDirectoryInfo.Name, "Cases", actualResultFileInfo.Name);

					string expectedFilePath = Path.Combine(expectedResultsDirectory, relativeFilePath);
					if (File.Exists(expectedFilePath))
					{
						string expectedFileContents = File.ReadAllText(expectedFilePath);

						if (expectedFileContents == actualFileContents)
						{
							this.succeededCount++;
						}
						else
						{
							this.failures.Add(new SelfTestFileFailure
							{
								RelativeFilePath = relativeFilePath,
								ExpectedFilePath = expectedFilePath,
								ActualFilePath = actualResultFileInfo.FullName,
								ExpectedContents = expectedFileContents,
								ActualContents = actualFileContents
							});
						}
					}
					else
					{
						// Expected file does not exist
						this.failures.Add(new SelfTestFileFailure
						{
							RelativeFilePath = relativeFilePath,
							ExpectedFilePath = expectedFilePath,
							ActualFilePath = actualResultFileInfo.FullName,
							ExpectedContents = null,
							ActualContents = actualFileContents
						});
					}
				}
			}

			if (this.failures.Count == 0)
			{
				this.ShowSuccess();
				return;
			}
			else
			{
				this.ShowingDiff = true;
				this.RefreshUI();
			}
		}

		private void ShowSuccess()
		{
			this.ShowingDiff = false;
			this.Status = "All tests succeeded.";
		}

		private void Compare()
		{
			var diffBuilder = new SideBySideDiffBuilder(new Differ());
			this.Diff = diffBuilder.BuildDiffModel(this.ExpectedText ?? string.Empty, this.ActualText ?? string.Empty);
			this.RaisePropertyChanged(nameof(this.Diff));
		}
	}
}
