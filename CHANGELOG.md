# Roadmap

- Support for dependency properties from other IoC frameworks
- Add an option for what folder to add the test class to

# Changelog

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