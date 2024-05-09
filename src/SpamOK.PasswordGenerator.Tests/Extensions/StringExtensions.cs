//-----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------

namespace SpamOK.PasswordGenerator.Tests.Extensions;

/// <summary>
/// String extensions.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Count the number of capital letters in a string.
    /// </summary>
    /// <param name="input">Input string.</param>
    /// <returns>The amount of capital letters in the string.</returns>
    public static int CountCapitalLetters(this string input)
    {
        return input.Count(char.IsUpper);
    }
}
