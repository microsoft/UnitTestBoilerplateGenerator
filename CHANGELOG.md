Features that have a checkmark are complete and available for
download in the
[CI build](http://vsixgallery.com/extension/UnitTestBoilerplate.RandomEngy.ab470ad4-8fce-418f-9a5d-d22d50d71215/).

# Changelog

These are the changes to each version that has been released
on the official Visual Studio extension gallery.

## Roadmap

- Support for dependency properties from other IoC frameworks
- Conform to user's current code styles (tabs, `this.` prefix)

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