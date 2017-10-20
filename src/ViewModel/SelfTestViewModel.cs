using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using GalaSoft.MvvmLight;
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

		private string status;
		public string Status
		{
			get { return this.status; }
			set { this.Set(ref this.status, value); }
		}

		private int failureIndex;

		private async Task InitializeAsync()
		{
			var createTestService = new SelfTestService();

			createTestService.Clean(this.projects);

			await createTestService.GenerateTestFilesAsync(this.projects);

			string solutionDirectory = Path.GetDirectoryName(this.dte.Solution.FileName);
			string expectedResultsDirectory = Path.Combine(solutionDirectory, "SelfTestExpectedResults");

			// Go over all "*TestCases" folders in actual results folder, dig into "Cases" directory
			int totalFiles = 0;
			int succeededFiles = 0;

			this.failures = new List<SelfTestFileFailure>();

			var resultsInfo = new DirectoryInfo(solutionDirectory);
			foreach (DirectoryInfo projectDirectoryInfo in resultsInfo.GetDirectories("*TestCases"))
			{
				DirectoryInfo casesDirectoryInfo = new DirectoryInfo(Path.Combine(projectDirectoryInfo.FullName, "Cases"));

				foreach (FileInfo actualResultFileInfo in casesDirectoryInfo.GetFiles())
				{
					totalFiles++;

					string actualFileContents = File.ReadAllText(actualResultFileInfo.FullName);

					string expectedFilePath = Path.Combine(expectedResultsDirectory, projectDirectoryInfo.Name, "Cases", actualResultFileInfo.Name);
					if (File.Exists(expectedFilePath))
					{
						string expectedFileContents = File.ReadAllText(expectedFilePath);

						if (expectedFileContents == actualFileContents)
						{
							succeededFiles++;
						}
						else
						{
							this.failures.Add(new SelfTestFileFailure
							{
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
				// Succeeded UI

				return;
			}


		}
	}
}
