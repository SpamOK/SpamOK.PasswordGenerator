// -----------------------------------------------------------------------
// <copyright file="EnumExtensions.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace SpamOK.PasswordGenerator.Algorithms.Diceware.Extensions
{
    using System;
    using System.Reflection;

    /// <summary>
    /// EnumExtensions class.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Get the resource name of the word list.
        /// </summary>
        /// <param name="value">DicewareWordList value.</param>
        /// <returns>ResourceName attribute.</returns>
        public static string GetResourceName(this Enum value)
        {
            // Retrieve the field info for the specific enum value
            FieldInfo field = value.GetType().GetField(value.ToString());

            // Get the FilenameAttribute on the enum field
            ResourceNameAttribute attribute = field.GetCustomAttribute<ResourceNameAttribute>();

            if (attribute == null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            // Return the filename if the attribute is found, otherwise null
            return attribute.ResourceName;
        }

        /// <summary>
        /// Get the resource name of the word list.
        /// </summary>
        /// <param name="value">DicewareWordList value.</param>
        /// <returns>ResourceName attribute.</returns>
        public static char? GetSeparatorCharacter(this Enum value)
        {
            // Retrieve the field info for the specific enum value
            FieldInfo field = value.GetType().GetField(value.ToString());

            // Get the FilenameAttribute on the enum field
            SeparatorCharacterAttribute attribute = field.GetCustomAttribute<SeparatorCharacterAttribute>();

            if (attribute == null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            // Return the filename if the attribute is found, otherwise null
            return attribute.SeparatorCharacter;
        }
    }
}
