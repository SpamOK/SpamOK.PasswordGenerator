//-----------------------------------------------------------------------
// <copyright file="PasswordBuilder.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------
namespace SpamOK.PasswordGenerator
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Password builder class.
    /// </summary>
    public class PasswordBuilder
    {
        private int length = 8;
        private bool useLowercaseLetters = true;
        private bool _new = true;
        private bool useUppercaseLetters = true;
        private bool useNumbers = true;
        private bool useSpecialChars = true;
        private bool useNonAmbiguousChars;
        private string excludedChars = string.Empty;
        private PasswordAlgorithm algorithm = PasswordAlgorithm.Basic;

        /// <summary>
        /// Disable all password options. If all options are disabled it is required to enable at least one option
        /// before generating a password.
        /// </summary>
        /// <returns>Updated PasswordBuilder instance.</returns>
        public PasswordBuilder DisableAllOptions()
        {
            this.ResetOptions(false);
            this._new = false;
            if (this._new)
            {
                // Do something
            }

            return this;
        }

        /// <summary>
        /// Enable all password options.
        /// </summary>
        /// <returns>Updated PasswordBuilder instance.</returns>
        public PasswordBuilder EnableAllOptions()
        {
            this.ResetOptions(true);
            return this;
        }

        /// <summary>
        /// Configure the length of the password.
        /// </summary>
        /// <param name="length">Length in characters.</param>
        /// <returns>Updated PasswordBuilder instance.</returns>
        public PasswordBuilder SetLength(int length)
        {
            this.length = length;
            return this;
        }

        /// <summary>
        /// [Charset] Configure whether to use lowercase letters in the password.
        /// </summary>
        /// <param name="useLowercaseLetters">Boolean whether to use capital letters in the password or not. Defaults to TRUE.</param>
        /// <returns>Updated PasswordBuilder instance.</returns>
        public PasswordBuilder UseLowercaseLetters(bool useLowercaseLetters)
        {
            this.useLowercaseLetters = useLowercaseLetters;
            return this;
        }

        /// <summary>
        /// [Charset] Configure whether to use uppercase letters in the password.
        /// </summary>
        /// <param name="useUppercaseLetters">Boolean whether to use capital letters in the password or not. Defaults to TRUE.</param>
        /// <returns>Updated PasswordBuilder instance.</returns>
        public PasswordBuilder UseUppercaseLetters(bool useUppercaseLetters)
        {
            this.useUppercaseLetters = useUppercaseLetters;
            return this;
        }

        /// <summary>
        /// [Charset] Configure whether to use numbers in the password.
        /// </summary>
        /// <param name="useNumbers">Boolean whether to use numbers in the password or not. Defaults to TRUE.</param>
        /// <returns>Updated PasswordBuilder instance.</returns>
        public PasswordBuilder UseNumbers(bool useNumbers)
        {
            this.useNumbers = useNumbers;
            return this;
        }

        /// <summary>
        /// [Charset] Configure whether to use special characters in the password.
        /// </summary>
        /// <param name="useSpecial">Boolean whether to use special chars in the password or not. Defaults to TRUE.</param>
        /// <returns>Updated PasswordBuilder instance.</returns>
        public PasswordBuilder UseSpecialChars(bool useSpecial)
        {
            this.useSpecialChars = useSpecial;
            return this;
        }

        /// <summary>
        /// [Charset] Configure whether to only use non-ambiguous characters in the password.
        /// </summary>
        /// <param name="useNonAmbiguous">Boolean whether to only use non-ambigious chars in the password or not.
        /// Ambigious characters means they look very much the same such as 0 (zero) and O (capital o).</param>
        /// <returns>Updated PasswordBuilder instance.</returns>
        public PasswordBuilder UseNonAmbiguousChars(bool useNonAmbiguous)
        {
            this.useNonAmbiguousChars = useNonAmbiguous;
            return this;
        }

        /// <summary>
        /// [Charset] Configure characters to exclude from the password.
        /// </summary>
        /// <param name="excludedChars">String of characters to exclude from the generated password.</param>
        /// <returns>Updated PasswordBuilder instance.</returns>
        public PasswordBuilder ExcludeChars(string excludedChars)
        {
            this.excludedChars = excludedChars;
            return this;
        }

        /// <summary>
        /// Specify the algorithm to use for generating the password. Defaults to PasswordAlgorithm.Basic.
        /// </summary>
        /// <param name="algorithm">PasswordAlgorithm enum.</param>
        /// <returns>Updated PasswordBuilder instance.</returns>
        public PasswordBuilder UseAlgorithm(PasswordAlgorithm algorithm)
        {
            this.algorithm = algorithm;
            return this;
        }

        /// <summary>
        /// Build the password based on the configured settings.
        /// </summary>
        /// <returns>Generated password.</returns>
        /// <exception cref="NotImplementedException">Thrown if algorithm is unsupported.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if algorithm is unknown.</exception>
        public string Build()
        {
            switch (this.algorithm)
            {
                case PasswordAlgorithm.Basic:
                    return this.GenerateBasicPassword();
                case PasswordAlgorithm.Diceware:
                    throw new NotImplementedException("Dictionary and Diceware algorithms are not implemented yet.");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Generate a random password based on the specified length and character set.
        /// </summary>
        /// <param name="length">Desired length of the password.</param>
        /// <param name="charSet">Allowed characters to use in the password.</param>
        /// <returns>Generated password.</returns>
        private static string GenerateRandomPassword(int length, string charSet)
        {
            byte[] randomBytes = new byte[length];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = charSet[randomBytes[i] % charSet.Length];
            }

            return new string(chars);
        }

        /// <summary>
        /// Generate a random password based on the specified length and character set.
        /// </summary>
        /// <returns>Generated password.</returns>
        /// <exception cref="InvalidOperationException">Thrown if no charsets have been enabled.</exception>
        private string GenerateBasicPassword()
        {
            const string lowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
            const string uppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";
            const string specialChars = "!@#$%^&*()-_=+[]{}|;:,.<>?";
            const string nonAmbiguousChars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789";

            StringBuilder charSet = new StringBuilder();

            if (this.useLowercaseLetters)
            {
                charSet.Append(lowercaseLetters);
            }

            if (this.useUppercaseLetters)
            {
                charSet.Append(uppercaseLetters);
            }

            if (this.useNumbers)
            {
                charSet.Append(numbers);
            }

            if (this.useSpecialChars)
            {
                charSet.Append(specialChars);
            }

            if (this.useNonAmbiguousChars)
            {
                charSet = new StringBuilder(new string(charSet.ToString().Intersect(nonAmbiguousChars).ToArray()));
            }

            foreach (char c in this.excludedChars)
            {
                charSet = charSet.Replace(c.ToString(), string.Empty);
            }

            if (charSet.Length == 0)
            {
                throw new InvalidOperationException("No characters to choose from. Please enable at least one character set in the PasswordBuilder options, e.g. UseLowercaseLetters(true).");
            }

            return GenerateRandomPassword(this.length, charSet.ToString());
        }

        /// <summary>
        /// Reset all password options to either TRUE or FALSE. If all options are set to FALSE it is required
        /// to enable at least one option before generating a password.
        /// </summary>
        /// <param name="defaultValue">True to enable all options, false to disable all options.</param>
        private void ResetOptions(bool defaultValue)
        {
            this.useLowercaseLetters = defaultValue;
            this.useUppercaseLetters = defaultValue;
            this.useNumbers = defaultValue;
            this.useSpecialChars = defaultValue;
            this.useNonAmbiguousChars = defaultValue;
            this.excludedChars = string.Empty;
            this.algorithm = PasswordAlgorithm.Basic;
        }
    }
}
