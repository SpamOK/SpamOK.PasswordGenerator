// -----------------------------------------------------------------------
// <copyright file="DicewareSeparator.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace SpamOK.PasswordGenerator.Algorithms.Diceware
{
    /// <summary>
    /// Separator for Diceware passwords.
    /// </summary>
    public enum DicewareSeparator
    {
        /// <summary>
        /// No separator.
        /// </summary>
        None,

        /// <summary>
        /// A dash separator.
        /// </summary>
        Dash,

        /// <summary>
        /// Space separator.
        /// </summary>
        Space,

        /// <summary>
        /// Underscore separator.
        /// </summary>
        Underscore,

        /// <summary>
        /// Dot separator.
        /// </summary>
        Dot,
    }
}
