// -----------------------------------------------------------------------
// <copyright file="DicewareSalt.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace SpamOK.PasswordGenerator.Algorithms.Diceware
{
    /// <summary>
    /// Salt option for Diceware passwords.
    /// </summary>
    public enum DicewareSalt
    {
        /// <summary>
        /// No separator.
        /// </summary>
        None,

        /// <summary>
        /// Adds a random character at the beginning of the passphrase.
        /// </summary>
        Prefix,

        /// <summary>
        /// Adds a random character somewhere in the middle of the passphrase.
        /// </summary>
        Sprinkle,

        /// <summary>
        /// Adds a random character at the end of the passphrase.
        /// </summary>
        Suffix,
    }
}
