# Roadmap

- Add an option for what folder to add the test class to.

# Changelog

## 2.4.0
**2020-04-04**
- Added ability to load settings from a user-specified file, thanks to Jess McAnally.

## 2.3.0
**2020-02-23**
- Added ability to generate tests for structs.
- Added support for custom mock implementations. These can be automatically used for specified interfaces.

## 2.2.0
**2020-01-24**
- Added ability to store extension settings in a .utbg.json config file that applies for all users of the solution. This allows sharing templates and settings with team members through source control.
- Moved the Moq VerifyAll() call to the end of each test method. It can hide errors if it throws an exception from within the cleanup method.

## 2.1.0
**2019-08-21**
- Added ability to specify extra "using" namespaces to add to the test class.
- Fixed options dialog closing when pressing Enter on the test method format fields.

## 2.0.1
**2019-08-13**
- Fixed null reference error on classes that have attributes on method parameters.

## 2.0.0
**2019-07-13**
- Added ability to customize the test method via a template. See the [Custom Format Tokens documentation](https://github.com/Microsoft/UnitTestBoilerplateGenerator/wiki/Custom-Format-Tokens) for available tokens in this new template.
- Removed "Test object creation" and "Test object reference" formats as they are now incorporated into the test method format.
- Changed generated test methods to initialize arguments with defaults rather than "TODO", to allow it to compile right away. If you prefer "TODO", you can use the `$ParameterSetupTodo$` token in place of `$ParameterSetupDefaults$`.
- Added link to token reference from template options page.
- Added "NewLineIfPopulated" token modifier that inserts a new line after the token if it's populated.

## 1.10.5
**2019-06-01**
- Added ability to choose no mock framework.
- Fixed crash when trying to revert a template to default.

## 1.10.0
**2019-04-25**
- Added support for FakeItEasy mock framework.

## 1.9.20
**2019-02-05**
- Fixed error with SQL Database projects.

## 1.9.17
**2019-02-05**
- Fixed installation issues with some versions of Visual Studio.

## 1.9.15
**2018-12-18**
- Added support for Visual Studio 2019.

## 1.9.13
**2018-12-06**
- Generated tests in xUnit now use Assert.True(false) instead of the non-existent Assert.Fail() .
- Removed extra newline after placeholder test.

## 1.9.12
**2018-11-18**
- Fixed xUnit detection on .NET Framework projects.

## 1.9.11
**2018-11-16**
- Fixed "Not implemented" error on solutions with unloaded projects on VS 2017 15.9.0.

## 1.9.10
**2018-07-11**
- Updated to use async background loading

## 1.9.8
**2018-07-08**
- Fixed crash issue on VS 2015.

## 1.9.6
**2018-07-05**
- Fixed crash on nullable or fully qualified parameters (Thanks to rCartoux)

## 1.9.0
**2018-07-01**
- Added stub tests for all public method (Thanks to rCartoux)

## 1.8.0
**2018-04-01**
- Added ability to set a preferred test and mock framework to be used when either multiple frameworks are detected or no frameworks are detected.
- Added detected framework information to the "create" dialog.
- Fixed issue with detecting frameworks added as PackageReference in .csproj files with an msbuild namespace.

## 1.7.1
**2018-02-11**
- Fixed crash issue on Visual Studio 2015 due to missing DLL.

## 1.7.0
**2018-01-24**
- Added ability to set the test file name format in settings.

## 1.6.9
**2018-01-14**
- Interface template fields now support tokens that formerly only worked in the main template.

## 1.6.6
**2017-11-07**
- Fixed issue where leading comments in template were dropped.

## 1.6.4
**2017-10-22**
- Added support for the "CamelCase" token modifier. For instance $ClassName.CamelCase$ will give a camelCased version of the tested class name.

## 1.6.2
**2017-10-21**
- Added support for Rhino Mocks

## 1.6.1
**2017-10-20**
- Added support for the xUnit test framework
- Changed templates to be test framework specific as well as mock framework specific. Default templates are now generated dynamically.

## 1.5.12
**2017-09-24**
- Fixed framework detection for .NET Core projects
- Added ability to change the test and mock frameworks before generating the test

## 1.5.10
**2017-04-25**
- Added support for Ninject and Grace property injection
- We no longer try to make mock objects for unmockable structs like DateTime
- Fixed an issue where we would mis-identify an NUnit project as a Visual Studio Test project
- Fixed an issue where tests were being created with some non-CLRF lines

## 1.5.2
**2017-02-26**
- Added support for mocking generic interfaces.

## 1.5.0
**2017-01-30**
- Switched to a template-based system, allowing users to change the generated code from the settings screen.
- Added support for NSubstitute

## 1.4.5
**2016-10-23**
- Fixed issue with detecting test and mock frameworks on UWP projects with project.json.
- Fixed issue with classes with simple type constructor parameters; those are now filled in with "TODO".
- Test project list is now alphabetically sorted.

## 1.4
**2016-09-25**
- Added support for NUnit
- Added support for SimpleStubs mocking framework
- Added settings storage to remember last picked project

## 1.3
**2016-09-13**
- Updated to run mockRepository.VerifyAll() in a cleanup method

## 1.2
**2016-09-01**
- Appropriate using statements are now brought in automatically

## 1.1
**2016-08-25**
- Fixed mixed \r\n and \n on generated code, now only generates \r\n.

## 1.0
**2016-08-25**
- Initial release
- Support for Moq, constructor injection and Unity dependency injection