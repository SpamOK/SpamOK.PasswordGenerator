//-----------------------------------------------------------------------
// <copyright file="DicewarePasswordTest.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------

namespace SpamOK.PasswordGenerator.Tests
{
    using SpamOK.PasswordGenerator;

    /// <summary>
    /// Main tests.
    /// </summary>
    public class DicewarePasswordTest
    {
        private DicewarePasswordBuilder _passwordBuilder;

        /// <summary>
        /// Setup method.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _passwordBuilder = new DicewarePasswordBuilder();
        }

        /// <summary>
        /// Ensure that DisableAllOptions works as expected.
        /// </summary>
        [Test]
        public void TestPasswordGeneration()
        {
            var test = _passwordBuilder
                .GeneratePassword();

            // Attempt to generate a password with all options disabled.
            // This should throw an exception.
            Assert.IsNotEmpty(_passwordBuilder
                .GeneratePassword());
        }
    }
}
