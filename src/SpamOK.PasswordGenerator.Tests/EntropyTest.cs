//-----------------------------------------------------------------------
// <copyright file="EntropyTest.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------
namespace SpamOK.PasswordGenerator.Tests
{
    using SpamOK.PasswordGenerator.Models;
    using SpamOK.PasswordGenerator.Algorithms.Diceware;

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
                Assert.That(password.GetEntropy().GetTimeToCrackSeconds(), Is.GreaterThanOrEqualTo(100000000));
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
                Assert.That(password.GetEntropy().GetTimeToCrackSeconds(), Is.GreaterThanOrEqualTo(1000000000));
            });
        }

        /// <summary>
        /// Test that entropy calculation helper method work as expected for lowercase letters only.
        /// </summary>
        [Test]
        public void TestEntropyCalculationDicewarePassword()
        {
            var passwordBuilder = new SpamOK.PasswordGenerator.BasicPasswordBuilder();
            var passwordObject = passwordBuilder
                .EnableAllOptions()
                .SetLength(10)
                .GeneratePassword();

            // Get the password as a string.
            Console.WriteLine(passwordObject.ToString());
        }

        /// <summary>
        /// Test that entropy calculation helper method work as expected for lowercase letters only.
        /// </summary>
        [Test]
        public void TestEntropyPasswordStrength()
        {
            var passwordEntropy = new PasswordEntropy(10);
            Assert.That(passwordEntropy.GetPasswordStrength(), Is.EqualTo(PasswordStrength.VeryWeak));

            passwordEntropy = new PasswordEntropy(30);
            Assert.That(passwordEntropy.GetPasswordStrength(), Is.EqualTo(PasswordStrength.Weak));

            passwordEntropy = new PasswordEntropy(45);
            Assert.That(passwordEntropy.GetPasswordStrength(), Is.EqualTo(PasswordStrength.Mediocre));

            passwordEntropy = new PasswordEntropy(60);
            Assert.That(passwordEntropy.GetPasswordStrength(), Is.EqualTo(PasswordStrength.Strong));

            passwordEntropy = new PasswordEntropy(130);
            Assert.That(passwordEntropy.GetPasswordStrength(), Is.EqualTo(PasswordStrength.VeryStrong));

            passwordEntropy = new PasswordEntropy(195);
            Assert.That(passwordEntropy.GetPasswordStrength(), Is.EqualTo(PasswordStrength.Overkill));
        }
    }
}
