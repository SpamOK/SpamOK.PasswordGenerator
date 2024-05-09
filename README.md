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
This library supports multiple password generation methods, which are:

1. Basic password
   - This method generates a random password using a (configurable) combination of lowercase letters, uppercase letters, numbers, and special characters.
2. Diceware passphrase generation
   - This method generates a password using a list of words from a wordlist. The words are selected randomly from the list and concatenated to form a password.

### 1. Basic password
Basic usage of the library is as follows:
```csharp
var passwordBuilder = new SpamOK.PasswordGenerator.BasicPasswordBuilder();
string password = passwordBuilder
    .SetLength(12)
    .UseLowercaseLetters(true)
    .UseUppercaseLetters(true)
    .UseNumbers(true)
    .UseSpecialChars(true)
    .UseNonAmbiguousChars(false)
    .ExcludeChars("abcdefg")
    .GeneratePassword();
```

#### (Optional) Enable/disable all options

Instead of enabling or disabling each option individually, you can use the
DisableAllOptions() and EnableAllOptions() methods to quickly set all options to a specific state.

This example will disable all options by default and then enable lowercase letters only:

```csharp
var passwordBuilder = new SpamOK.PasswordGenerator.BasicPasswordBuilder();
string password = passwordBuilder
    .DisableAllOptions()
    .UseLowercaseLetters(true)
    .GeneratePassword();
```

### 2. Diceware passphrase generation
Diceware passphrase generation is a method of generating passwords using a list of words from a wordlist. The words are selected randomly from the list and concatenated to form a passphrase.
You can read more about Diceware passphrases [here](https://en.wikipedia.org/wiki/Diceware).

This library supports Diceware in multiple languages, including English and Dutch. The default language is English.

Basic usage of the library is as follows:
```csharp
var passwordBuilder = new SpamOK.PasswordGenerator.DicewarePasswordBuilder();
string password = passwordBuilder
    .SetLength(6)
    .SetWordList(SpamOK.PasswordGenerator.Algorithms.Diceware.DicewareWordList.English)
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

## Credits
Credits belong to the following sources that are used in this project:

- NL Diceware wordlist: https://mko.re/diceware/diceware-wordlist-8k-composites-nl.txt
- EN Diceware wordlist: https://theworld.com/~reinhold/diceware8k.txt
- DE Diceware wordlist: https://github.com/dys2p/wordlists-de
- FR Diceware wordlist: https://github.com/ArthurPons/diceware-fr-alt
- ES Diceware wordlist: https://github.com/mir123/dadoware-bonito-es/blob/master/DW-es-bonito.csv
- UK Diceware wordlist:



