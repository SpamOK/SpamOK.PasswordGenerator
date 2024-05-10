//-----------------------------------------------------------------------
// <copyright file="EntropyCalculatorHelper.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------
namespace SpamOK.PasswordGenerator.Helpers
{
    using System;

    /// <summary>
    /// Helper class to calculate entropy and keyspace for Diceware passwords.
    /// </summary>
    public static class EntropyCalculatorHelper
    {
        /// <summary>
        /// Calculate the entropy of a string given the amount of possible symbols used in the string.
        /// </summary>
        /// <param name="input">The string to calculate the entropy for.</param>
        /// <param name="possibleSymbols">Amount of possible symbols for every char in input string. Defaults to 95 for all ASCII printable characters.</param>
        /// <returns>Entropy in bits.</returns>
        public static double CalculateStringEntropy(string input, int possibleSymbols = 95)
        {
            // For printable ASCII characters, there are 95 possibilities (' ' to '~')
            // Calculate entropy using the formula
            return Math.Log(possibleSymbols, 2) * input.Length;
        }

        /// <summary>
        /// Calculate the average time to crack a passphrase based on its bit entropy and guessing rate in seconds.
        /// </summary>
        /// <param name="bitEntropy">Entropy in bits of the password.</param>
        /// <param name="guessesPerSecond">Amount of guesses per second an attacker could do. Defaults to very conservative 1 trillion/sec.</param>
        /// <returns>Time to crack in seconds.</returns>
        public static long GetTimeToCrack(double bitEntropy, double guessesPerSecond = 1e12)
        {
            double keyspace = CalculateKeyspace(bitEntropy);
            double averageKeysToTry = keyspace / 2;
            return (long)(averageKeysToTry / guessesPerSecond);
        }

        /// <summary>
        /// Helper method to calculate the keyspace from bit entropy.
        /// </summary>
        /// <param name="bitEntropy">The bit entropy of the passphrase.</param>
        /// <returns>The total keyspace as a double.</returns>
        private static double CalculateKeyspace(double bitEntropy)
        {
            // Calculate the total keyspace from the bit entropy
            return Math.Pow(2, bitEntropy);
        }
    }
}
