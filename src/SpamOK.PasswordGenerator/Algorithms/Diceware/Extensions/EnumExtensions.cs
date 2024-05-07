// -----------------------------------------------------------------------
// <copyright file="EnumExtensions.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace SpamOK.PasswordGenerator.Algorithms.Diceware
{
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
        public static string GetResourceName(this DicewareWordList value)
        {
            // Retrieve the field info for the specific enum value
            FieldInfo field = value.GetType().GetField(value.ToString());

            // Get the FilenameAttribute on the enum field
            ResourceNameAttribute attribute = field.GetCustomAttribute<ResourceNameAttribute>();

            // Return the filename if the attribute is found, otherwise null
            return attribute?.ResourceName;
        }
    }
}
