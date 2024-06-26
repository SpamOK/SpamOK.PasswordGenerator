// -----------------------------------------------------------------------
// <copyright file="DicewareWordList.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace SpamOK.PasswordGenerator.Algorithms.Diceware
{
    using SpamOK.PasswordGenerator.Algorithms.Diceware.Extensions;

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

        /// <summary>
        /// German word list.
        /// </summary>
        [ResourceName("SpamOK.PasswordGenerator.Algorithms.Diceware.WordLists.de.diceware")]
        German,

        /// <summary>
        /// French word list.
        /// </summary>
        [ResourceName("SpamOK.PasswordGenerator.Algorithms.Diceware.WordLists.fr.diceware")]
        French,

        /// <summary>
        /// Spanish word list.
        /// </summary>
        [ResourceName("SpamOK.PasswordGenerator.Algorithms.Diceware.WordLists.es.diceware")]
        Spanish,

        /// <summary>
        /// Ukrainian word list.
        /// </summary>
        [ResourceName("SpamOK.PasswordGenerator.Algorithms.Diceware.WordLists.uk.diceware")]
        Ukrainian,

        /// <summary>
        /// Italian word list.
        /// </summary>
        [ResourceName("SpamOK.PasswordGenerator.Algorithms.Diceware.WordLists.it.diceware")]
        Italian,
    }
}
