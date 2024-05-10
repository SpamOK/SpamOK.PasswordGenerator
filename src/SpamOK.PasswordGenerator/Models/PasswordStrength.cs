//-----------------------------------------------------------------------
// <copyright file="PasswordStrength.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------
namespace SpamOK.PasswordGenerator.Models
{
    /// <summary>
    /// Levels of password strength.
    /// </summary>
    public enum PasswordStrength
    {
        /// <summary>
        /// Represents a very weak password. 25 bits of entropy or less.
        /// </summary>
        VeryWeak = 0,

        /// <summary>
        /// Represents a weak password. 26-35 bits of entropy.
        /// </summary>
        Weak = 1,

        /// <summary>
        /// Represents a mediocre password. 36-59 bits of entropy.
        /// </summary>
        Mediocre = 2,

        /// <summary>
        /// Represents a strong password. 60-127 bits of entropy.
        /// </summary>
        Strong = 3,

        /// <summary>
        /// Represents a very strong password. 128-190 bits of entropy.
        /// </summary>
        VeryStrong = 4,

        /// <summary>
        /// Represents the strongest password. 190 bits of entropy or more.
        /// </summary>
        Overkill = 5,
    }
}
