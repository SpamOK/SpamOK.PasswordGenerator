//-----------------------------------------------------------------------
// <copyright file="PasswordAlgorithm.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------
namespace SpamOK.PasswordGenerator
{
    /// <summary>
    /// Password generation algorithm.
    /// </summary>
    public enum PasswordAlgorithm
    {
        /// <summary>
        /// Basic algorithm.
        /// </summary>
        Basic,

        /// <summary>
        /// Dictionary algorithm which uses a list of words to generate a passphrase.
        /// </summary>
        Diceware,
    }
}
