//-----------------------------------------------------------------------
// <copyright file="DicewarePasswordBuilder.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------

namespace SpamOK.PasswordGenerator
{
    using System;
    using System.Security.Cryptography;
    using SpamOK.PasswordGenerator.Algorithms.Diceware;

    /// <summary>
    /// Diceware password generation algorithm.
    /// </summary>
    public class DicewarePasswordBuilder
    {
        private int _count = 5;
        private DicewareWordList _wordList = DicewareWordList.English;

        // private DicewareSeparator _separator;

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
        /// Generate a new password based on the configured settings.
        /// </summary>
        /// <returns>Generated password.</returns>
        public string GeneratePassword()
        {
            string[] words = new string[_count];

            // Get a diceware word for each word in the password.
            for (int i = 0; i < _count; i++)
            {
                // Generate a random diceware index by rolling 5 dices and concatenating the result numbers.
                var dicewareIndex = GenerateRandomDicewareIndex();

                // Get the word from the word list corresponding to the index.
                // Assuming the file is at "C:/path/to/words.txt"
                DicewareLookup lookup = new DicewareLookup(_wordList);

                // Example: Retrieve the word for dice index "11111"
                try
                {
                    string word = lookup.GetWordByDiceIndex(dicewareIndex);
                    words[i] = word;
                    Console.WriteLine(word);  // Outputs the first word in the file
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return string.Join("-", words);
        }

        /// <summary>
        /// Generate a diceword index by rolling 5 dices and concatenating the results.
        /// </summary>
        /// <returns>Int diceword index.</returns>
        private int GenerateRandomDicewareIndex()
        {
            // Roll a 1-6 dice.
            byte[] randomBytes = new byte[5];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

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
