// -----------------------------------------------------------------------
// <copyright file="ResourceNameAttribute.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace SpamOK.PasswordGenerator.Algorithms.Diceware.Extensions
{
    using System;

    /// <summary>
    /// Resource name attribute for diceware word lists.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ResourceNameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceNameAttribute"/> class.
        /// </summary>
        /// <param name="resourceName">Filename string.</param>
        public ResourceNameAttribute(string resourceName)
        {
            ResourceName = resourceName;
        }

        /// <summary>
        /// Gets filename of word list.
        /// </summary>
        public string ResourceName { get; }
    }
}
