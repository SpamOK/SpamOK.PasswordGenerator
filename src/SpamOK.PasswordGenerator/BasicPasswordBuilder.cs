//-----------------------------------------------------------------------
// <copyright file="BasicPasswordBuilder.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------
namespace SpamOK.PasswordGenerator
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SpamOK.PasswordGenerator.Helpers;
    using SpamOK.PasswordGenerator.Interfaces;
    using SpamOK.PasswordGenerator.Models;

    /// <summary>
    /// Basic password generation algorithm.
    /// </summary>
    public class BasicPasswordBuilder : IPasswordBuilder
    {
        private int _length = 8;
        private bool _useLowercaseLetters = true;
        private bool _useUppercaseLetters = true;
        private bool _useNumbers = true;
        private bool _useSpecialChars = true;
        private bool _useNonAmbiguousChars;
        private string _excludedChars = string.Empty;

        /// <summary>
        /// Disable all password options. If all options are disabled it is required to enable at least one option
        /// before generating a password.
        /// </summary>
        /// <returns>Updated PasswordBuilder instance.</returns>
        public BasicPasswordBuilder DisableAllOptions()
        {
            ResetOptions(false);

            return this;
        }

        /// <summary>
        /// Enable all password options.
        /// </summary>
        /// <returns>Updated PasswordBuilder instance.</returns>
        public BasicPasswordBuilder EnableAllOptions()
        {
            ResetOptions(true);
            return this;
        }

        /// <summary>
        /// Configure the length of the password.
        /// </summary>
        /// <param name="length">Length in characters.</param>
        /// <returns>Updated PasswordBuilder instance.</returns>
        public BasicPasswordBuilder SetLength(int length)
        {
            _length = length;
            return this;
        }

        /// <summary>
        /// [Charset] Configure whether to use lowercase letters in the password.
        /// </summary>
        /// <param name="useLowercaseLetters">Boolean whether to use capital letters in the password or not. Defaults to TRUE.</param>
        /// <returns>Updated PasswordBuilder instance.</returns>
        public BasicPasswordBuilder UseLowercaseLetters(bool useLowercaseLetters)
        {
            _useLowercaseLetters = useLowercaseLetters;
            return this;
        }

        /// <summary>
        /// [Charset] Configure whether to use uppercase letters in the password.
        /// </summary>
        /// <param name="useUppercaseLetters">Boolean whether to use capital letters in the password or not. Defaults to TRUE.</param>
        /// <returns>Updated PasswordBuilder instance.</returns>
        public BasicPasswordBuilder UseUppercaseLetters(bool useUppercaseLetters)
        {
            _useUppercaseLetters = useUppercaseLetters;
            return this;
        }

        /// <summary>
        /// [Charset] Configure whether to use numbers in the password.
        /// </summary>
        /// <param name="useNumbers">Boolean whether to use numbers in the password or not. Defaults to TRUE.</param>
        /// <returns>Updated PasswordBuilder instance.</returns>
        public BasicPasswordBuilder UseNumbers(bool useNumbers)
        {
            _useNumbers = useNumbers;
            return this;
        }

        /// <summary>
        /// [Charset] Configure whether to use special characters in the password.
        /// </summary>
        /// <param name="useSpecial">Boolean whether to use special chars in the password or not. Defaults to TRUE.</param>
        /// <returns>Updated PasswordBuilder instance.</returns>
        public BasicPasswordBuilder UseSpecialChars(bool useSpecial)
        {
            _useSpecialChars = useSpecial;
            return this;
        }

        /// <summary>
        /// [Charset] Configure whether to only use non-ambiguous characters in the password.
        /// </summary>
        /// <param name="useNonAmbiguous">Boolean whether to only use non-ambigious chars in the password or not.
        /// Ambigious characters means they look very much the same such as 0 (zero) and O (capital o).</param>
        /// <returns>Updated PasswordBuilder instance.</returns>
        public BasicPasswordBuilder UseNonAmbiguousChars(bool useNonAmbiguous)
        {
            _useNonAmbiguousChars = useNonAmbiguous;
            return this;
        }

        /// <summary>
        /// [Charset] Configure characters to exclude from the password.
        /// </summary>
        /// <param name="excludedChars">String of characters to exclude from the generated password.</param>
        /// <returns>Updated PasswordBuilder instance.</returns>
        public BasicPasswordBuilder ExcludeChars(string excludedChars)
        {
            _excludedChars = excludedChars;
            return this;
        }

        /// <summary>
        /// Generate a random password based on the specified length and character set.
        /// </summary>
        /// <returns>Generated password.</returns>
        /// <exception cref="InvalidOperationException">Thrown if no charsets have been enabled.</exception>
        public Password GeneratePassword()
        {
            var charSet = GetCharSet();

            if (charSet.Length == 0)
            {
                throw new InvalidOperationException("Error during generation: no characters to choose from. Please enable at least one character set in the PasswordBuilder options, e.g. UseLowercaseLetters(true).");
            }

            var password = GenerateRandomPassword(_length, charSet.ToString());

            var possibleSymbolsCount = GetPossibleSymbolsCount();
            var entropy = EntropyCalculatorHelper.CalculateStringEntropy(password, possibleSymbolsCount);
            return new Password(password, entropy);
        }

        /// <summary>
        /// Generate a random password based on the specified length and character set asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the generated password.</returns>
        /// <exception cref="InvalidOperationException">Thrown if no charsets have been enabled.</exception>
        public Task<Password> GeneratePasswordAsync()
        {
            return Task.Run(() => GeneratePassword());
        }

        /// <summary>
        /// Generate a random password based on the specified length and character set.
        /// </summary>
        /// <param name="length">Desired length of the password.</param>
        /// <param name="charSet">Allowed characters to use in the password.</param>
        /// <returns>Generated password.</returns>
        private static string GenerateRandomPassword(int length, string charSet)
        {
            char[] chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = GetUnbiasedRandomChar(charSet);
            }

            return new string(chars);
        }

        /// <summary>
        /// Get a random character from the character set without modulo bias.
        /// </summary>
        /// <remarks>
        /// See https://research.kudelskisecurity.com/2020/07/28/the-definitive-guide-to-modulo-bias-and-how-to-avoid-it/ for more information
        /// about the modulo bias problem and how to avoid it.
        /// </remarks>
        /// <param name="charSet">Character set to choose from.</param>
        /// <returns>Random character from the character set.</returns>
        private static char GetUnbiasedRandomChar(string charSet)
        {
            // Calculate the largest multiple of charSet.Length that's <= 256
            int maxValidValue = 256 - (256 % charSet.Length);

            while (true)
            {
                var buffer = RandomHelper.GenerateRandomBytes(1);
                if (buffer[0] < maxValidValue)
                {
                    return charSet[buffer[0] % charSet.Length];
                }

                // If the value is too large, try again
            }
        }

        /// <summary>
        /// Get the character set based on the current configuration.
        /// </summary>
        /// <returns>Character set based on the current configuration.</returns>
        private StringBuilder GetCharSet()
        {
            const string lowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
            const string uppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";
            const string specialChars = "!@#$%^&*()-_=+[]{}|;:,.<>?";
            const string nonAmbiguousChars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789!@#$%^&*-=+[]|:,<>?";

            StringBuilder charSet = new StringBuilder();

            if (_useLowercaseLetters)
            {
                charSet.Append(lowercaseLetters);
            }

            if (_useUppercaseLetters)
            {
                charSet.Append(uppercaseLetters);
            }

            if (_useNumbers)
            {
                charSet.Append(numbers);
            }

            if (_useSpecialChars)
            {
                charSet.Append(specialChars);
            }

            if (_useNonAmbiguousChars)
            {
                charSet = new StringBuilder(new string(charSet.ToString().Intersect(nonAmbiguousChars).ToArray()));
            }

            foreach (char c in _excludedChars)
            {
                charSet = charSet.Replace(c.ToString(), string.Empty);
            }

            return charSet;
        }

        /// <summary>
        /// Get the number of possible symbols based on the current configuration. This is used to calculate the
        /// bit entropy of the password.
        /// </summary>
        /// <returns>Number of possible symbols based on current password builder configuration.</returns>
        private int GetPossibleSymbolsCount()
        {
            var charSet = GetCharSet();
            return charSet.Length;
        }

        /// <summary>
        /// Reset all password options to either TRUE or FALSE. If all options are set to FALSE it is required
        /// to enable at least one option before generating a password.
        /// </summary>
        /// <param name="defaultValue">True to enable all options, false to disable all options.</param>
        private void ResetOptions(bool defaultValue)
        {
            _useLowercaseLetters = defaultValue;
            _useUppercaseLetters = defaultValue;
            _useNumbers = defaultValue;
            _useSpecialChars = defaultValue;
            _useNonAmbiguousChars = defaultValue;
            _excludedChars = string.Empty;
        }
    }
}
