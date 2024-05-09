// -----------------------------------------------------------------------
// <copyright file="DicewareCapitalization.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace SpamOK.PasswordGenerator.Algorithms.Diceware
{
    /// <summary>
    /// Separator for Diceware passwords.
    /// </summary>
    public enum DicewareCapitalization
    {
        /// <summary>
        /// No capitalization.
        /// </summary>
        None,

        /// <summary>
        /// Make the first letter of each word uppercase.
        /// </summary>
        TitleCase,

        /// <summary>
        /// Make all letters uppercase.
        /// </summary>
        Uppercase,

        /// <summary>
        /// Make all letters lowercase.
        /// </summary>
        Lowercase,

        /// <summary>
        /// Randomize the capitalization of each letter.
        /// </summary>
        Random,
    }
}
