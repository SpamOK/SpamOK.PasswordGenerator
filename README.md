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
Install-Package SpamOK.PasswordGenerator -Version 1.0.0
```

## Usage
```csharp
var passwordBuilder = new SpamOK.PasswordGenerator.PasswordBuilder();
string password = passwordBuilder
    .SetLength(12)
    .UseNumbers(true)
    .UseSpecialChars(true)
    .UseNonAmbiguousChars(false)
    .ExcludeChars("l1Io0O")
    .UseAlgorithm(SpamOK.PasswordGenerator.PasswordAlgorithm.Basic)
    .Build();
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

## License
This project is licensed under the MIT License - see the LICENSE file for details.
