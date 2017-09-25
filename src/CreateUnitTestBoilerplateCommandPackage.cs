//------------------------------------------------------------------------------
// <copyright file="CreateUnitTestBoilerplateCommandPackage.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;
using UnitTestBoilerplate.View;

namespace UnitTestBoilerplate
{
	/// <summary>
	/// This is the class that implements the package exposed by this assembly.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The minimum requirement for a class to be considered a valid package for Visual Studio
	/// is to implement the IVsPackage interface and register itself with the shell.
	/// This package uses the helper classes defined inside the Managed Package Framework (MPF)
	/// to do it: it derives from the Package class that provides the implementation of the
	/// IVsPackage interface and uses the registration attributes defined in the framework to
	/// register itself and its components with the shell. These attributes tell the pkgdef creation
	/// utility what data to put into .pkgdef file.
	/// </para>
	/// <para>
	/// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
	/// </para>
	/// </remarks>
	[PackageRegistration(UseManagedResourcesOnly = true)]
	[InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
	[ProvideMenuResource("Menus.ctmenu", 1)]
	[ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids.SolutionExists)]
	[Guid(CreateUnitTestBoilerplateCommandPackage.PackageGuidString)]
	[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
	[ProvideProfile(typeof(OptionsDialogPage), "Unit Test Boilerplate Generator", "Unit Test Boilerplate Generator Settings", 106, 107, true, DescriptionResourceID = 108)]
	[ProvideOptionPage(typeof(OptionsDialogPage), "Unit Test Boilerplate Generator", "Templates", 101, 109, supportsAutomation: true)]
	public sealed class CreateUnitTestBoilerplateCommandPackage : Package
	{
		/// <summary>
		/// CreateUnitTestBoilerplateCommandPackage GUID string.
		/// </summary>
		public const string PackageGuidString = "73cd6dcd-1791-4633-acf4-889c1a09ca55";

		/// <summary>
		/// Initializes a new instance of the <see cref="CreateUnitTestBoilerplateCommand"/> class.
		/// </summary>
		public CreateUnitTestBoilerplateCommandPackage()
		{
			// Inside this method you can place any initialization code that does not require
			// any Visual Studio service because at this point the package object is created but
			// not sited yet inside Visual Studio environment. The place to do all the other
			// initialization is the Initialize method.
		}

		#region Package Members

		/// <summary>
		/// Initialization of the package; this method is called right after the package is sited, so this is the place
		/// where you can put all the initialization code that rely on services provided by VisualStudio.
		/// </summary>
		protected override void Initialize()
		{
			CreateUnitTestBoilerplateCommand.Initialize(this);
			base.Initialize();

			var componentModel = (IComponentModel)this.GetService(typeof(SComponentModel));
			VisualStudioWorkspace = componentModel.GetService<VisualStudioWorkspace>();
		}

		#endregion

		public static VisualStudioWorkspace VisualStudioWorkspace { get; private set; }
	}
}
