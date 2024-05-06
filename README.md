# SpamOK.PasswordGenerator
[<img src="https://img.shields.io/github/v/release/SpamOK/SpamOK.PasswordGenerator?include_prereleases&logo=github">](https://github.com/SpamOK/SpamOK.PasswordGenerator/releases)  [<img src="https://img.shields.io/github/actions/workflow/status/SpamOK/SpamOK.PasswordGenerator/dotnet-build-run-tests.yml?label=tests">](https://github.com/SpamOK/SpamOK.PasswordGenerator/actions/workflows/dotnet-build-run-tests.yml)

## Description

SpamOK.PasswordGenerator is a .NET library designed to generate highly secure, random passwords. It helps developers ensure their applications adhere to best practices in password security, making it ideal for systems requiring high levels of data protection.

## Features

- Generate passwords with configurable length.
- Optionally include numbers, special characters, and uppercase/lowercase differentiation.
- Uses cryptographically secure random number generation.

## Installation

To install SpamOK.PasswordGenerator, use the following NuGet command:

```bash
Install-Package SpamOK.PasswordGenerator -Version 0.2.0
```

## Usage

### Basic usage
Basic usage of the library is as follows:
```csharp
var passwordBuilder = new SpamOK.PasswordGenerator.PasswordBuilder();
string password = passwordBuilder
    .SetLength(12)
    .UseLowercaseLetters(true)
    .UseUppercaseLetters(true)
    .UseNumbers(true)
    .UseSpecialChars(true)
    .UseNonAmbiguousChars(false)
    .ExcludeChars("abcdefg")
    .UseAlgorithm(SpamOK.PasswordGenerator.PasswordAlgorithm.Basic)
    .GeneratePassword();
```

### Enable/disable all options

Instead of enabling or disabling each option individually, you can use the
DisableAllOptions() and EnableAllOptions() methods to quickly set all options to a specific state.

This example will disable all options by default and then enable lowercase letters only:

```csharp
var passwordBuilder = new SpamOK.PasswordGenerator.PasswordBuilder();
string password = passwordBuilder
    .DisableAllOptions()
    .UseLowercaseLetters(true)
    .GeneratePassword();
```


## Contributing
Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are greatly appreciated.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

* Fork the Project
* Create your Feature Branch (**git checkout -b feature/AmazingFeature**)
* Commit your Changes (**git commit -m 'Add some AmazingFeature'**)
* Push to the Branch (**git push origin feature/AmazingFeature**)
* Open a Pull Request

### Building the project locally
The SpamOK.PasswordGenerator library itself targets .NET Standard 2.0 which means it works on both .NET Framework and the modern .NET 6.0/7.0/8.0+.

However the test project uses .NET 8.0 as the targeting framework which means .NET 8.0 needs to be installed on your machine in order to run tests.

#### Full compatibility list for local development:
- Visual Studio 2022 / JetBrains Rider 2024.1+
- .NET 8.0+

### Running tests locally
This project uses NUnit for testing. To run the tests, use the following command:

```bash
dotnet test
```

To run the tests with code coverage statistics, use the following command:

```bash
dotnet test -c Release /p:CollectCoverage=true /p:CoverletOutput=coverage /p:CoverletOutputFormat=opencover
```

## License
This project is licensed under the MIT License - see the LICENSE.md file for details.
