//-----------------------------------------------------------------------
// <copyright file="EntropyTest.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------
namespace SpamOK.PasswordGenerator.Tests
{
    using SpamOK.PasswordGenerator.Models;

    /// <summary>
    /// Tests that cover entropy calculation and helper methods.
    /// </summary>
    public class EntropyTest
    {
        /// <summary>
        /// Test that entropy calculation helper method work as expected for lowercase letters only.
        /// </summary>
        [Test]
        public void TestEntropyCalculationBasicPasswordLowercase()
        {
            BasicPasswordBuilder passwordBuilder = new BasicPasswordBuilder();
            var password = passwordBuilder
                .DisableAllOptions()
                .SetLength(15)
                .UseLowercaseLetters(true)
                .GeneratePassword();

            // 15 characters, 26 lowercase letters is expected to have 70 bits of entropy (rounded down).
            Assert.Multiple(() =>
            {
                Assert.That((int)password.GetEntropy().BitEntropy, Is.EqualTo(70));
                Assert.That(password.GetEntropy().GetPasswordStrength(), Is.EqualTo(PasswordStrength.Strong));
            });
        }

        /// <summary>
        /// Test that entropy calculation helper method work as expected for all chars.
        /// </summary>
        [Test]
        public void TestEntropyCalculationBasicPasswordAllChars()
        {
            BasicPasswordBuilder passwordBuilder = new BasicPasswordBuilder();
            var password = passwordBuilder
                .EnableAllOptions()
                .SetLength(15)
                .GeneratePassword();

            // 15 characters, all possible chars (75) is expected to have 93 bits of entropy (rounded down).
            Assert.Multiple(() =>
            {
                Assert.That((int)password.GetEntropy().BitEntropy, Is.EqualTo(93));
                Assert.That(password.GetEntropy().GetPasswordStrength(), Is.EqualTo(PasswordStrength.Strong));
            });
        }

        /// <summary>
        /// Test that entropy calculation helper method work as expected for lowercase letters only.
        /// </summary>
        [Test]
        public void TestEntropyCalculationDicewarePassword()
        {
            DicewarePasswordBuilder passwordBuilder = new DicewarePasswordBuilder();
            var password = passwordBuilder
                .SetLength(5)
                .GeneratePassword();

            // 5 words should be >= 60 bits of entropy.
            Assert.Multiple(() =>
            {
                Assert.That((int)password.GetEntropy().BitEntropy, Is.GreaterThanOrEqualTo(60));
                Assert.That(password.GetEntropy().GetPasswordStrength(), Is.GreaterThanOrEqualTo(PasswordStrength.Strong));
            });
        }
    }
}
