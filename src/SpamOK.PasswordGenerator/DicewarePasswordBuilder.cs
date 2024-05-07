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

        // private DicewareSeparator _separator;
        // private DicewareWordList _wordList;

        /// <summary>
        /// Generate a new password based on the configured settings.
        /// </summary>
        /// <returns>Generated password.</returns>
        public string GeneratePassword()
        {
            var diceRolls = GenerateDiceRoll(_count);

            // Return dice rolls array elements as a concatenated string.
            return string.Join(string.Empty, diceRolls);
        }

        /// <summary>
        /// Generate a dice roll and each number as an array.
        /// </summary>
        /// <param name="diceCount">The amount of dices to roll.</param>
        /// <returns>int[] with each dice result as separate element.</returns>
        private int[] GenerateDiceRoll(int diceCount)
        {
            // Roll a 1-6 dice for each word in the password based on count.
            // Roll a 1-6 dice.
            byte[] randomBytes = new byte[diceCount];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            int[] diceRolls = new int[diceCount];
            string charSet = "123456";
            for (int i = 0; i < diceCount; i++)
            {
                // Convert the character to its numeric value (1-6)
                diceRolls[i] = charSet[randomBytes[i] % charSet.Length] - '0';
            }

            return diceRolls;
        }
    }
}
