//-----------------------------------------------------------------------
// <copyright file="IPasswordBuilder.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------
namespace SpamOK.PasswordGenerator.Interfaces
{
    using SpamOK.PasswordGenerator.Models;

    /// <summary>
    /// Password builder interface.
    /// </summary>
    public interface IPasswordBuilder
    {
        /// <summary>
        /// Generate a password based on the set builder options.
        /// </summary>
        /// <returns>The generated password object.</returns>
        Password GeneratePassword();
    }
}
