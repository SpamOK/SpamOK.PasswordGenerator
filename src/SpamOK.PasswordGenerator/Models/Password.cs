//-----------------------------------------------------------------------
// <copyright file="Password.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------
namespace SpamOK.PasswordGenerator.Models
{
    /// <summary>
    /// Generated password model which contains password and metadata. Access the password string via the ToString() method.
    /// </summary>
    public class Password
    {
        private double _entropy;
        private string _password;

        /// <summary>
        /// Initializes a new instance of the <see cref="Password"/> class.
        /// </summary>
        /// <param name="password">The generated password.</param>
        /// <param name="entropy">The entropy based on amount of possible character combinations in the password.</param>
        public Password(string password, double entropy)
        {
            _password = password;
            _entropy = entropy;
        }

        /// <summary>
        /// Get the generated password as string.
        /// </summary>
        /// <returns>The generated password.</returns>
        public override string ToString()
        {
            return _password;
        }

        /// <summary>
        /// Gets information about the entropy of the password including password strength, time to crack and more.
        /// </summary>
        /// <returns>PasswordEntropy object.</returns>
        public PasswordEntropy GetEntropy()
        {
            return new PasswordEntropy(_password, _entropy);
        }
    }
}
