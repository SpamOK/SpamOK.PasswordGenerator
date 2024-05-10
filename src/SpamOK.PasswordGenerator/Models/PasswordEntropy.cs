//-----------------------------------------------------------------------
// <copyright file="PasswordEntropy.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------
namespace SpamOK.PasswordGenerator.Models
{
    using SpamOK.PasswordGenerator.Helpers;

    /// <summary>
    /// Contains entropy details of a password.
    /// </summary>
    public class PasswordEntropy
    {
        private string _password;
        private int _timeToCrack;

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordEntropy"/> class.
        /// </summary>
        /// <param name="password">The generated password.</param>
        /// <param name="entropy">The entropy of the password.</param>
        public PasswordEntropy(string password, double entropy)
        {
            _password = password;
            BitEntropy = entropy;
            _timeToCrack = EntropyCalculatorHelper.GetTimeToCrack(entropy);
        }

        /// <summary>
        /// Gets the entropy of the password in bits.
        /// </summary>
        public double BitEntropy { get; }

        /// <summary>
        /// Get password strength based on entropy.
        /// </summary>
        /// <returns>PasswordStrength enum that represents password strength.</returns>
        public PasswordStrength GetPasswordStrength()
        {
            // Strength levels:
            // 0-25 bits: Very Weak
            // 26-35 bits: Weak
            // 36-59 bits: Mediocre
            // 60-127 bits: Strong
            // 128-190 bits: Very Strong
            // 190 bits: Overkill
            if (BitEntropy <= 25)
                return PasswordStrength.VeryWeak;
            if (BitEntropy <= 35)
                return PasswordStrength.Weak;
            if (BitEntropy <= 59)
                return PasswordStrength.Mediocre;
            if (BitEntropy <= 127)
                return PasswordStrength.Strong;
            if (BitEntropy <= 190)
                return PasswordStrength.VeryStrong;

            return PasswordStrength.Overkill;
        }

        /// <summary>
        /// Get average time to crack the password in seconds.
        /// </summary>
        /// <returns>Time to crack password in seconds as integer.</returns>
        public int GetTimeToCrackSeconds()
        {
            return _timeToCrack;
        }
    }
}
