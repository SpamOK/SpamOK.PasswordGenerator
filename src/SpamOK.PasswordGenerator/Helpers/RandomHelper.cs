//-----------------------------------------------------------------------
// <copyright file="RandomHelper.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------
namespace SpamOK.PasswordGenerator.Helpers
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// Helper class for generating cryptographically secure random data.
    /// </summary>
    public static class RandomHelper
    {
        /// <summary>
        /// Generate a random boolean value with a specified probability of being true.
        /// </summary>
        /// <param name="probability">Probability (in percentage) for the boolean to be true.</param>
        /// <returns>Random boolean value.</returns>
        public static bool GenerateRandomBoolean(int probability)
        {
            byte[] randomByte = GenerateRandomBytes(1);

            // Convert the byte to a value between 0 and 100
            int chance = (randomByte[0] % 100) + 1;
            return chance <= probability;
        }

        /// <summary>
        /// Generate a random alphanumeric character ('a'-'z', 'A'-'Z', '0'-'9').
        /// </summary>
        /// <returns>Random alphanumeric character.</returns>
        public static char GenerateRandomAlphanumericCharacter()
        {
            string alphanumericSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            byte[] randomByte = GenerateRandomBytes(1);

            // Map the byte value to an index within the alphanumeric character set
            int index = randomByte[0] % alphanumericSet.Length;
            return alphanumericSet[index];
        }

        /// <summary>
        /// Generate a random integer between the specified minimum and maximum values.
        /// </summary>
        /// <param name="minValue">The minimum value (inclusive).</param>
        /// <param name="maxValue">The maximum value (inclusive).</param>
        /// <returns>Random integer between the specified values.</returns>
        public static int GenerateRandomNumberBetween(int minValue, int maxValue)
        {
            // Validate the range
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(minValue), "minValue should not be greater than maxValue.");
            }

            // Get a random byte array large enough to ensure uniform distribution
            byte[] randomBytes = GenerateRandomBytes(4);

            // Convert the bytes to an unsigned integer
            uint num = BitConverter.ToUInt32(randomBytes, 0);

            // Calculate the range size
            uint range = (uint)(maxValue - minValue + 1);

            // Scale the random number to the desired range
            uint scaled = num % range;
            return (int)(minValue + scaled);
        }

        /// <summary>
        /// Generate random bytes using a secure method.
        /// </summary>
        /// <param name="size">Number of bytes to generate.</param>
        /// <returns>Array of random bytes.</returns>
        public static byte[] GenerateRandomBytes(int size)
        {
            byte[] randomBytes = new byte[size];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            return randomBytes;
        }
    }
}
