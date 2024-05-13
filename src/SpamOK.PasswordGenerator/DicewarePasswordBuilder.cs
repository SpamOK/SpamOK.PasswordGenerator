//-----------------------------------------------------------------------
// <copyright file="DicewarePasswordBuilder.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SpamOK.PasswordGenerator.Tests")]

namespace SpamOK.PasswordGenerator
{
    using System;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using SpamOK.PasswordGenerator.Algorithms.Diceware;
    using SpamOK.PasswordGenerator.Algorithms.Diceware.Extensions;
    using SpamOK.PasswordGenerator.Helpers;
    using SpamOK.PasswordGenerator.Interfaces;
    using SpamOK.PasswordGenerator.Models;

    /// <summary>
    /// Diceware password generation algorithm.
    /// </summary>
    public class DicewarePasswordBuilder : IPasswordBuilder
    {
        private int _length = 5;
        private DicewareWordList _wordList = DicewareWordList.English;
        private DicewareSeparator _separator = DicewareSeparator.Dash;
        private DicewareCapitalization _capitalization = DicewareCapitalization.None;
        private DicewareSalt _salt = DicewareSalt.None;
        private bool _hackerify = false;

        /// <summary>
        /// Configure the word list to use.
        /// </summary>
        /// <param name="wordList">The wordlist to use. Defaults to DicewareWordList.English.</param>
        /// <returns>Updated DicewarePasswordBuilder instance.</returns>
        public DicewarePasswordBuilder SetWordList(DicewareWordList wordList)
        {
            _wordList = wordList;
            return this;
        }

        /// <summary>
        /// Configure the amount of words in the diceware passphrase.
        /// </summary>
        /// <param name="wordCount">The amount of words. Defaults to 5.</param>
        /// <returns>Updated DicewarePasswordBuilder instance.</returns>
        public DicewarePasswordBuilder SetLength(int wordCount)
        {
            _length = wordCount;
            return this;
        }

        /// <summary>
        /// Configure the separator to use.
        /// </summary>
        /// <param name="separator">The separator to use. Defaults to DicewareSeparator.Dash.</param>
        /// <returns>Updated DicewarePasswordBuilder instance.</returns>
        public DicewarePasswordBuilder SetSeparator(DicewareSeparator separator)
        {
            _separator = separator;
            return this;
        }

        /// <summary>
        /// Configure the capitalization to use.
        /// </summary>
        /// <param name="capitalization">The capitalization type to use. Defaults to DicewareCapitalization.None.</param>
        /// <returns>Updated DicewarePasswordBuilder instance.</returns>
        public DicewarePasswordBuilder SetCapitalization(DicewareCapitalization capitalization)
        {
            _capitalization = capitalization;
            return this;
        }

        /// <summary>
        /// Configure the salt to use.
        /// </summary>
        /// <param name="salt">The salt type to use. Defaults to Salt.None.</param>
        /// <returns>Updated DicewarePasswordBuilder instance.</returns>
        public DicewarePasswordBuilder SetSalt(DicewareSalt salt)
        {
            _salt = salt;
            return this;
        }

        /// <summary>
        /// Enable or disable hackerify mode which converts the password string to leetspeak.
        /// </summary>
        /// <param name="value">TRUE to enable, FALSE to disable. Defaults to FALSE.</param>
        /// <returns>Updated DicewarePasswordBuilder instance.</returns>
        public DicewarePasswordBuilder HackerifyPassword(bool value)
        {
            _hackerify = value;
            return this;
        }

        /// <summary>
        /// Generate a new password based on the configured settings.
        /// </summary>
        /// <returns>Generated password.</returns>
        public Password GeneratePassword()
        {
            string[] words = GetDicewareWords();
            string passphrase = string.Join(_separator.GetSeparatorCharacter().ToString(), words);

            // Add salt to the passphrase based on the configured salt option.
            passphrase = AddSalt(passphrase);

            // Convert the passphrase to leetspeak if hackerify mode is enabled.
            if (_hackerify)
            {
                passphrase = HackerifyHelper.ConvertToHackerify(passphrase);
            }

            // Assuming 40 possible characters in a diceware word (a-z, 0-9 and four separator chars).
            var dicewareSymbolCount = 40;
            var entropy = EntropyCalculatorHelper.CalculateStringEntropy(passphrase, dicewareSymbolCount);
            return new Password(passphrase, entropy);
        }

        /// <summary>
        /// Generate a new password based on the configured settings asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the generated password.</returns>
        public Task<Password> GeneratePasswordAsync()
        {
            return Task.Run(() => GeneratePassword());
        }

        /// <summary>
        /// Adds a salt to the passphrase based on the configured salt option.
        /// </summary>
        /// <param name="passphrase">The passphrase to add the salt to.</param>
        /// <param name="salt">The character that acts as the salt.</param>
        /// <returns>Passphrase with salt included.</returns>
        internal string AddSalt(string passphrase, char salt)
        {
            switch (_salt)
            {
                case DicewareSalt.Prefix:
                    return salt + passphrase;
                case DicewareSalt.Sprinkle:
                    int index = RandomHelper.GenerateRandomNumberBetween(0, passphrase.Length);
                    return passphrase.Insert(index, salt.ToString());
                case DicewareSalt.Suffix:
                    return passphrase + salt;
                case DicewareSalt.None:
                default:
                    return passphrase;
            }
        }

        /// <summary>
        /// Adds a random salt to the passphrase based on the configured salt option.
        /// </summary>
        /// <param name="passphrase">The passphrase to add the random salt to.</param>
        /// <returns>Passphrase with salt included.</returns>
        internal string AddSalt(string passphrase)
        {
            var salt = RandomHelper.GenerateRandomAlphanumericCharacter();
            return AddSalt(passphrase, salt);
        }

        /// <summary>
        /// Generate diceware words for the password.
        /// </summary>
        /// <returns>Returns string array with individual diceware words.</returns>
        private string[] GetDicewareWords()
        {
            string[] words = new string[_length];

            // Get a diceware word for each word in the password.
            for (int i = 0; i < _length; i++)
            {
                // Generate a random diceware index by rolling 5 dices and concatenating the result numbers.
                var dicewareIndex = GenerateRandomDicewareIndex();

                // Get the word from the word list corresponding to the index.
                DicewareLookup lookup = new DicewareLookup(_wordList);
                string word = lookup.GetWordByDiceIndex(dicewareIndex);

                // Capitalize the word based on the configured capitalization.
                word = CapitalizeWord(word);

                words[i] = word;
            }

            return words;
        }

        /// <summary>
        /// Capitalize a word based on the configured capitalization.
        /// </summary>
        /// <param name="word">The word to capitalize.</param>
        /// <returns>Capitalized word.</returns>
        private string CapitalizeWord(string word)
        {
            switch (_capitalization)
            {
                case DicewareCapitalization.TitleCase:
                    return char.ToUpper(word[0]) + word.Substring(1).ToLower();
                case DicewareCapitalization.Uppercase:
                    return word.ToUpper();
                case DicewareCapitalization.Lowercase:
                    return word.ToLower();
                case DicewareCapitalization.Random:
                    // Randomize the capitalization of each letter.
                    char[] chars = word.ToCharArray();
                    for (int i = 0; i < chars.Length; i++)
                    {
                        if (char.IsLetter(chars[i]) && RandomHelper.GenerateRandomBoolean(50))
                        {
                            chars[i] = char.ToUpper(chars[i]);
                        }
                    }

                    return new string(chars);
                default:
                    return word;
            }
        }

        /// <summary>
        /// Generate a diceword index by rolling 5 dices and concatenating the results.
        /// </summary>
        /// <returns>Int diceword index.</returns>
        private int GenerateRandomDicewareIndex()
        {
            // Roll a 1-6 dice.
            byte[] randomBytes = RandomHelper.GenerateRandomBytes(5);
            int[] diceRolls = new int[5];
            string charSet = "123456";
            for (int i = 0; i < 5; i++)
            {
                // Convert the character to its numeric value (1-6)
                diceRolls[i] = charSet[randomBytes[i] % charSet.Length] - '0';
            }

            // Concatenate the dice rolls into a single number.
            return int.Parse(string.Concat(diceRolls));
        }
    }
}
