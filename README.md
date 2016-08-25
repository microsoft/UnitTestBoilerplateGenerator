# Unit Test Boilerplate Generator

<!-- Replace this badge with your own-->
[![Build status](https://ci.appveyor.com/api/projects/status/7ecfxkoe7sj4nw5h?svg=true)](https://ci.appveyor.com/project/RandomEngy/unittestboilerplategenerator)

<!-- Update the VS Gallery link after you upload the VSIX-->
Download this extension from the [VS Gallery](https://visualstudiogallery.msdn.microsoft.com/e1bc3b8f-6280-490e-8294-5109fe7bed77)
or get the [CI build](http://vsixgallery.com/extension/UnitTestBoilerplate.RandomEngy.ab470ad4-8fce-418f-9a5d-d22d50d71215/).

---------------------------------------

Generates a unit test boilerplate from a given class, setting up mocks for all dependencies. Supports Moq with constructor injection or dependency injection via Unity.

Right click an item in Solution Explorer and choose "Create Unit Test Boilerplate" .

![Before Screenshot](BeforeScreenshot.png)

This will create a test class in the same relative path as the class in a specified unit test project.
All the dependencies are mocked and saved as fields which are created fresh for each test via [TestInitialize].
It also adds a helper method which will construct the class for you and pass in all of the mock dependencies.
You can then call .Setup() and .Verify() on the mocks in your test methods.

![After Screenshot](AfterScreenshot.png)

See the [changelog](CHANGELOG.md) for changes and roadmap.

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