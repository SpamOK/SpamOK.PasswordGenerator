// -----------------------------------------------------------------------
// <copyright file="DicewareLookup.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace SpamOK.PasswordGenerator.Algorithms.Diceware
{
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Lookup class for diceware words.
    /// </summary>
    public class DicewareLookup
    {
        private string[] _words;

        /// <summary>
        /// Initializes a new instance of the <see cref="DicewareLookup"/> class.
        /// </summary>
        /// <param name="wordList">Path to diceware file containing diceware words.</param>
        public DicewareLookup(DicewareWordList wordList)
        {
            LoadWords(wordList.GetResourceName());
        }

        /// <summary>
        /// Get word by dice index.
        /// </summary>
        /// <param name="diceIndex">5-dice concatenated int.</param>
        /// <returns>Word from diceware file.</returns>
        /// <exception cref="IndexOutOfRangeException">If index is higher than the diceware file contains.</exception>
        public string GetWordByDiceIndex(int diceIndex)
        {
            // Convert dice index from base-6 to base-10
            int index = ConvertBase6ToBase10(diceIndex.ToString()) - 1;

            // Check if index is within the bounds of the array
            if (index >= 0 && index < _words.Length)
            {
                return _words[index];
            }
            else
            {
                throw new IndexOutOfRangeException("Dice index is out of range.");
            }
        }

        private void LoadWords(string resourceName)
        {
            // Load all words from the file into an array.
            // This assumes the file's encoding is UTF-8; adjust if necessary.
            var assembly = Assembly.GetExecutingAssembly();

            var i = 0;
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException("Resource '" + resourceName + "' not found.", resourceName);
                }

                using (var reader = new StreamReader(stream))
                {
                    var words = new string[8192];
                    while (!reader.EndOfStream)
                    {
                        words[i] = reader.ReadLine();
                        i++;
                    }

                    _words = words;
                }
            }
        }

        private int ConvertBase6ToBase10(string base6)
        {
            int result = 0;
            foreach (char digit in base6)
            {
                result = (result * 6) + (digit - '1');
            }

            return result;
        }
    }
}
