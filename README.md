# Unit Test Boilerplate Generator

[![Build status](https://ci.appveyor.com/api/projects/status/7ecfxkoe7sj4nw5h?svg=true)](https://ci.appveyor.com/project/RandomEngy/unittestboilerplategenerator)

Download this extension from the [VS Gallery](https://visualstudiogallery.msdn.microsoft.com/f3f3b2a7-cefe-4ffe-add1-9740ae117252)
or get the [CI build](http://vsixgallery.com/extension/UnitTestBoilerplate.RandomEngy.ca0bb824-eb5a-41a8-ab39-3b81f03ba3fe/).

---------------------------------------

Generates a unit test boilerplate from a given class, setting up mocks for all dependencies.

Test frameworks supported:
* Visual Studio
* NUnit

Mock frameworks supported:
* Moq
* SimpleStubs

Dependency injection modes supported:
* Constructor injection
* Property injection via Unity

Right click an item in Solution Explorer and choose "Create Unit Test Boilerplate" .

![Before Screenshot](BeforeScreenshot.png)

This will create a test class in the same relative path as the class in a specified unit test project.
All the dependencies are mocked and saved as fields which are created fresh for each test via [TestInitialize].
It also adds a helper method which will construct the class for you and pass in all of the mock dependencies.
You can then call .Setup() and .Verify() on the mocks in your test methods.

![After Screenshot](AfterScreenshot.png)

The mock repository is set up as Strict by default, which means that it expects every call to a mock object
to have a .Setup() call for it. The [TestCleanup] method will call VerifyAll() for you at the end of every test:
this ensures that all of the setups in any mock object have been invoked at least once.

See the [changelog](CHANGELOG.md) for changes and roadmap. If you'd like to see support for other mocking
frameworks like Rhino Mocks, other IoC frameworks like Ninject or other coding styles, [open an issue](https://github.com/Microsoft/UnitTestBoilerplateGenerator/issues/new).

## Contribute
Check out the [contribution guidelines](CONTRIBUTING.md)
if you want to contribute to this project.

For cloning and building this project yourself, make sure
to install the
[Extensibility Tools 2015](https://visualstudiogallery.msdn.microsoft.com/ab39a092-1343-46e2-b0f1-6a3f91155aa6)
extension for Visual Studio which enables some features
used by this project.

## License
[MIT](LICENSE)