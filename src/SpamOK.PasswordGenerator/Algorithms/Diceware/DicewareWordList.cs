// -----------------------------------------------------------------------
// <copyright file="DicewareWordList.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace SpamOK.PasswordGenerator.Algorithms.Diceware
{
    /// <summary>
    /// Diceware word lists that can be used.
    /// </summary>
    public enum DicewareWordList
    {
        /// <summary>
        /// English word list.
        /// </summary>
        [ResourceName("SpamOK.PasswordGenerator.Algorithms.Diceware.WordLists.en.diceware")]
        English,

        /// <summary>
        /// Dutch word list.
        /// </summary>
        [ResourceName("SpamOK.PasswordGenerator.Algorithms.Diceware.WordLists.nl.diceware")]
        Dutch,
    }
}
