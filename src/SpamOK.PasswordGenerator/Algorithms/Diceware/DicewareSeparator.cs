// -----------------------------------------------------------------------
// <copyright file="DicewareSeparator.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace SpamOK.PasswordGenerator.Algorithms.Diceware
{
    using SpamOK.PasswordGenerator.Algorithms.Diceware.Extensions;

    /// <summary>
    /// Separator for Diceware passwords.
    /// </summary>
    public enum DicewareSeparator
    {
        /// <summary>
        /// No separator.
        /// </summary>
        [SeparatorCharacter('\0')]
        None,

        /// <summary>
        /// A dash separator.
        /// </summary>
        [SeparatorCharacter('-')]
        Dash,

        /// <summary>
        /// Space separator.
        /// </summary>
        [SeparatorCharacter(' ')]
        Space,

        /// <summary>
        /// Underscore separator.
        /// </summary>
        [SeparatorCharacter('_')]
        Underscore,

        /// <summary>
        /// Dot separator.
        /// </summary>
        [SeparatorCharacter('.')]
        Dot,
    }
}
