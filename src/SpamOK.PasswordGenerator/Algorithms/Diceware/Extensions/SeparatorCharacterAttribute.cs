// -----------------------------------------------------------------------
// <copyright file="SeparatorCharacterAttribute.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace SpamOK.PasswordGenerator.Algorithms.Diceware.Extensions
{
    using System;

    /// <summary>
    /// Separator character attribute for diceware word lists.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class SeparatorCharacterAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeparatorCharacterAttribute"/> class.
        /// </summary>
        /// <param name="character">Separator character.</param>
        public SeparatorCharacterAttribute(char character)
        {
            SeparatorCharacter = character;
        }

        /// <summary>
        /// Gets character to use as separator between words.
        /// </summary>
        public char SeparatorCharacter { get; }
    }
}
