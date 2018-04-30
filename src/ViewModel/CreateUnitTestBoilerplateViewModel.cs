using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EnvDTE;
using EnvDTE80;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.VisualStudio.Shell;
using UnitTestBoilerplate.Model;
using UnitTestBoilerplate.Services;
using UnitTestBoilerplate.Utilities;
using Project = EnvDTE.Project;
using Task = System.Threading.Tasks.Task;
using Window = EnvDTE.Window;

namespace UnitTestBoilerplate.ViewModel
{
	public class CreateUnitTestBoilerplateViewModel : ViewModelBase
	{
		private readonly DTE2 dte;

		public CreateUnitTestBoilerplateViewModel()
		{
			this.dte = (DTE2)ServiceProvider.GlobalProvider.GetService(typeof(DTE));


			this.TestProjects = new List<TestProject>();
			IList<Project> allProjects = SolutionUtilities.GetProjects(this.dte);


			string lastSelectedProject = StaticBoilerplateSettings.GetLastSelectedProject(this.dte.Solution.FileName);


			var newProjectList = new List<TestProject>();
			foreach (Project project in allProjects)
			{
				TestProject testProject = new TestProject
				{
					Name = project.Name,
					Project = project
				};

				newProjectList.Add(testProject);
			}

			this.TestProjects = newProjectList.OrderBy(p => p.Name).ToList();

			// First see if we've saved an entry for the last selected test project for this solution.
			if (this.selectedProject == null && lastSelectedProject != null)
			{
				foreach (var project in this.TestProjects)
				{
					if (string.Equals(lastSelectedProject, project.Project.FileName, StringComparison.OrdinalIgnoreCase))
					{
						this.selectedProject = project;
						break;
					}
				}
			}

			// If we don't have an entry yet, look for a project name that contains "Test"
			if (this.selectedProject == null)
			{
				foreach (var project in this.TestProjects)
				{
					if (project.Name.ToLowerInvariant().Contains("test"))
					{
						this.selectedProject = project;
						break;
					}
				}
			}

			// Otherwise select the first project
			if (this.selectedProject == null && this.TestProjects.Count > 0)
			{
				this.selectedProject = this.TestProjects[0];
			}

			this.TestFrameworkChoices = TestFrameworks.List;
			this.MockFrameworkChoices = MockFrameworks.List;

			// Populate selected/detected test/mock frameworks based on selected project
			this.UpdateFrameworks();
		}

		public ICreateUnitTestBoilerplateView View { get; set; }

		public List<TestProject> TestProjects { get; }

		private TestProject selectedProject;
		public TestProject SelectedProject
		{
			get { return this.selectedProject; }
			set
			{
				this.Set(ref this.selectedProject, value);
				this.UpdateFrameworks();
			}
		}

		public IList<TestFramework> TestFrameworkChoices { get; }

		private TestFramework selectedTestFramework;
		public TestFramework SelectedTestFramework
		{
			get { return this.selectedTestFramework; }
			set { this.Set(ref this.selectedTestFramework, value); }
		}

		private string detectedTestFrameworks;
		public string DetectedTestFrameworks
		{
			get { return this.detectedTestFrameworks; }
			set { this.Set(ref this.detectedTestFrameworks, value); }
		}

		public IList<MockFramework> MockFrameworkChoices { get; }

		private MockFramework selectedMockFramework;

		public MockFramework SelectedMockFramework
		{
			get { return this.selectedMockFramework; }
			set { this.Set(ref this.selectedMockFramework, value); }
		}

		private string detectedMockFrameworks;
		public string DetectedMockFrameworks
		{
			get { return this.detectedMockFrameworks; }
			set { this.Set(ref this.detectedMockFrameworks, value); }
		}

		private RelayCommand createUnitTestCommand;
		public RelayCommand CreateUnitTestCommand
		{
			get
			{
				return this.createUnitTestCommand ?? (this.createUnitTestCommand = new RelayCommand(
					async () =>
					{
						try
						{
							IList<ProjectItemSummary> selectedFiles = SolutionUtilities.GetSelectedFiles(this.dte);
							await this.CreateUnitTestAsync(selectedFiles);
						}
						catch (Exception exception)
						{
							MessageBox.Show(exception.ToString());
						}
					}));
			}
		}

		internal async Task CreateUnitTestAsync(IList<ProjectItemSummary> selectedFiles, bool addToProject = true)
		{
			var generationService = new TestGenerationService();
			var createdTestPaths = new List<string>();
			foreach (ProjectItemSummary selectedFile in selectedFiles)
			{
				string generatedTestPath = await generationService.GenerateUnitTestFileAsync(
					selectedFile,
					this.SelectedProject.Project,
					this.SelectedTestFramework,
					this.SelectedMockFramework);

				createdTestPaths.Add(generatedTestPath);
			}

			if (addToProject)
			{
				bool focusSet = false;
				foreach (string createdTestPath in createdTestPaths)
				{
					// Add the file to project
					ProjectItem testItem = this.SelectedProject.Project.ProjectItems.AddFromFile(createdTestPath);

					Window testWindow = testItem.Open(EnvDTE.Constants.vsViewKindCode);
					testItem.ExpandView();
					testWindow.Visible = true;

					if (!focusSet)
					{
						testWindow.SetFocus();
						focusSet = true;
					}
				}

				StaticBoilerplateSettings.SaveSelectedTestProject(this.dte.Solution.FileName, this.SelectedProject.Project.FileName);
			}

			this.View?.Close();
		}

		private void UpdateFrameworks()
		{
			if (this.selectedProject == null)
			{
				this.SelectedTestFramework = TestFrameworks.Default;
				this.SelectedMockFramework = MockFrameworks.Default;
			}
			else
			{
				List<TestFramework> testFrameworks = SolutionUtilities.FindTestFrameworks(this.selectedProject.Project.FileName);
				List<MockFramework> mockFrameworks = SolutionUtilities.FindMockFrameworks(this.selectedProject.Project.FileName);

				this.SelectedTestFramework = SolutionUtilities.PickDefaultTestFramework(testFrameworks);
				this.SelectedMockFramework = SolutionUtilities.PickDefaultMockFramework(mockFrameworks);

				if (testFrameworks.Count == 0)
				{
					this.DetectedTestFrameworks = "Detected: None";
				}
				else
				{
					this.DetectedTestFrameworks = "Detected: " + string.Join(", ", testFrameworks);
				}

				if (mockFrameworks.Count == 0)
				{
					this.DetectedMockFrameworks = "Detected: None";
				}
				else
				{
					this.DetectedMockFrameworks = "Detected: " + string.Join(", ", mockFrameworks);
				}
			}
		}
	}

	public interface ICreateUnitTestBoilerplateView
	{
		void Close();
	}
}
