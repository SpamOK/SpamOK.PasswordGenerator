//-----------------------------------------------------------------------
// <copyright file="HackerifyHelper.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------
namespace SpamOK.PasswordGenerator.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Helper class for hackerify-ing (converting) a string.
    /// </summary>
    public static class HackerifyHelper
    {
        // Dictionary to map characters to their l33t equivalents
        private static Dictionary<char, string> _leetDictionary = new Dictionary<char, string>
        {
            { 'a', "@" },
            { 'b', "8" },
            { 'e', "3" },
            { 'i', "!" },
            { 'l', "1" },
            { 'o', "0" },
            { 't', "7" },
        };

        /// <summary>
        /// Convert a string to a hackerify-ed version by replacing regular characters with their special character
        /// counterpart.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Hackerified string.</returns>
        /// <exception cref="ArgumentNullException">Thrown if passed input parameter is null.</exception>
        public static string ConvertToHackerify(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            StringBuilder hackerified = new StringBuilder(input.Length);

            foreach (char c in input)
            {
                char lowerChar = char.ToLower(c);
                if (_leetDictionary.TryGetValue(lowerChar, out string replacement))
                {
                    hackerified.Append(replacement);
                }
                else
                {
                    hackerified.Append(c);
                }
            }

            return hackerified.ToString();
        }
    }
}
