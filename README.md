<p>&nbsp;</p>

<p align="center">
    <picture>
      <source media="(prefers-color-scheme: dark)" srcset="https://github.com/SpamOK/SpamOK.PasswordGenerator/assets/6917405/2e49a46f-5e53-4e7a-988b-94156bd83d15">
      <source media="(prefers-color-scheme: light)" srcset="https://github.com/SpamOK/SpamOK.PasswordGenerator/assets/6917405/300e3ad2-4217-4479-8609-4d56733cf8ab">
      <img width="600px" alt="Logo" src="https://github.com/SpamOK/SpamOK.PasswordGenerator/assets/6917405/300e3ad2-4217-4479-8609-4d56733cf8ab">
    </picture>
</p>

<div align="center">
    
[<img src="https://img.shields.io/github/v/release/SpamOK/SpamOK.PasswordGenerator?include_prereleases&logo=github">](https://www.nuget.org/packages/SpamOK.PasswordGenerator)  [<img src="https://img.shields.io/github/actions/workflow/status/SpamOK/SpamOK.PasswordGenerator/dotnet-build-run-tests.yml?label=tests">](https://github.com/SpamOK/SpamOK.PasswordGenerator/actions/workflows/dotnet-build-run-tests.yml) [<img src="https://img.shields.io/sonar/coverage/SpamOK_SpamOK.PasswordGenerator?server=https%3A%2F%2Fsonarcloud.io&label=test code coverage">](https://sonarcloud.io/summary/new_code?id=SpamOK_SpamOK.PasswordGenerator) [<img src="https://img.shields.io/sonar/quality_gate/SpamOK_SpamOK.PasswordGenerator?server=https%3A%2F%2Fsonarcloud.io&label=sonarcloud quality gate&logo=sonarcloud">](https://sonarcloud.io/summary/new_code?id=SpamOK_SpamOK.PasswordGenerator) [<img src="https://img.shields.io/nuget/dt/SpamOK.PasswordGenerator?label=nuget downloads&logo=nuget">](https://www.nuget.org/packages/SpamOK.PasswordGenerator)

</div>

<p>&nbsp;</p>

SpamOK.PasswordGenerator is a .NET library designed to generate highly secure, customizable, random passwords. It helps developers ensure their applications adhere to best practices in password security, making it ideal for systems requiring high levels of data protection.

The library supports two password generation methods: basic password generation and [Diceware](https://theworld.com/~reinhold/diceware.html) passphrase generation. The basic password generation method allows you to generate passwords with a configurable length and includes options for numbers, special characters, and uppercase/lowercase differentiation. The Diceware passphrase generation method generates passwords using a list of words from a wordlist, with options for word capitalization, word separation, and salt addition.

## Features
This library is designed to be easy to use and highly configurable, allowing developers to generate passwords that meet their specific requirements. The library is also fully unit tested, ensuring that it is reliable and robust.

This library features cryptographically secure random number generation. Additionally it provides generic helper methods
for things such as checking password strength, hackerifying strings and more.

### Basic password generation
- Generate random passwords with a configurable length.
- Include numbers, special characters, and uppercase/lowercase differentiation.
- Exclude specific characters from the password.
- Use non-ambiguous characters to avoid confusion between similar characters (e.g., 'l' and '1').

### Diceware passphrase generation
- Generate passwords using a list of words from a wordlist.
- Configure the amount of words, word capitalization, word separation, and salt addition.
- Supports multiple languages, including:
  - English
  - Dutch
  - German
  - French
  - Spanish
  - Italian
  - Ukrainian.

## Installation

To install SpamOK.PasswordGenerator, use the following NuGet command:

```bash
Install-Package SpamOK.PasswordGenerator -Version 0.2.0
```

## Usage
Below you can find examples of how to generate passwords using this library.
The PasswordGenerator classes apply the builder pattern, allowing you to set various options before generating the password.

### 1. Basic password
Generating a basic password is done as follows:
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

This example will disable all options and then enable lowercase letters only:

```csharp
var passwordBuilder = new SpamOK.PasswordGenerator.BasicPasswordBuilder();
string password = passwordBuilder
    .DisableAllOptions()
    .UseLowercaseLetters(true)
    .GeneratePassword();
```

### 2. Diceware passphrase generation
Diceware passphrase generation is a method of generating passwords using a dictionary. The words are selected randomly from the dictionary and concatenated to form a passphrase.
You can read more about Diceware passphrases [here](https://en.wikipedia.org/wiki/Diceware).

This library supports Diceware in multiple languages, including English, Dutch, German, French, Italian, Spanish and Ukrainian. The default language is English.

Basic usage is as follows:
```csharp
using SpamOK.PasswordGenerator.Algorithms.Diceware;

var passwordBuilder = new SpamOK.PasswordGenerator.DicewarePasswordBuilder();
string password = passwordBuilder
    .SetLength(5)
    .SetWordList(DicewareWordList.English)
    .SetSeparator(DicewareSeparator.Dash)
    .SetCapitalization(DicewareCapitalization.TitleCase)
    .SetSalt(DicewareSalt.Sprinkle)
    .GeneratePassword();
```

#### (Optional) Simple variant using default values
If you wish to generate a diceware password using all default values, then you can directly call the GeneratePassword() method without setting any options:

```csharp
var passwordBuilder = new SpamOK.PasswordGenerator.DicewarePasswordBuilder();
string password = passwordBuilder.GeneratePassword();
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
- IT Diceware wordlist: https://www.taringamberini.com/downloads/diceware_it_IT/lista-di-parole-diceware-in-italiano/4/word_list_diceware_it-IT-4.txt
- UK Diceware wordlist: ChatGPT 4

