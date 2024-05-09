//-----------------------------------------------------------------------
// <copyright file="DicewarePasswordTest.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------

namespace SpamOK.PasswordGenerator.Tests
{
    using SpamOK.PasswordGenerator;
    using SpamOK.PasswordGenerator.Algorithms.Diceware;

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
        /// Test that generating a diceware password with default options works.
        /// </summary>
        [Test]
        public void TestPasswordGenerationDefault()
        {
            Assert.IsNotEmpty(_passwordBuilder
                .GeneratePassword());
        }

        /// <summary>
        /// Test that generating a diceware password with the English word list works.
        /// </summary>
        [Test]
        public void TestPasswordGenerationEnglish()
        {
            Assert.IsNotEmpty(_passwordBuilder
                .SetWordList(DicewareWordList.English)
                .GeneratePassword());
        }

        /// <summary>
        /// Test that generating a diceware password with the Dutch word list works.
        /// </summary>
        [Test]
        public void TestPasswordGenerationDutch()
        {
            Assert.IsNotEmpty(_passwordBuilder
                .SetWordList(DicewareWordList.Dutch)
                .GeneratePassword());
        }

        /// <summary>
        /// Test that generating a diceware password with the Dutch word list works.
        /// </summary>
        [Test]
        public void TestPasswordGenerationGerman()
        {
            Assert.IsNotEmpty(_passwordBuilder
                .SetWordList(DicewareWordList.German)
                .GeneratePassword());
        }

        /// <summary>
        /// Test that generating a diceware password with the Dutch word list works.
        /// </summary>
        [Test]
        public void TestPasswordGenerationSeparators()
        {
            var password = _passwordBuilder
                .SetSeparator(DicewareSeparator.Dot)
                .GeneratePassword();

            Assert.That(password, Does.Contain("."));

            password = _passwordBuilder
                .SetSeparator(DicewareSeparator.Space)
                .GeneratePassword();

            Assert.That(password, Does.Contain(" "));

            password = _passwordBuilder
                .SetSeparator(DicewareSeparator.Dash)
                .GeneratePassword();

            Assert.That(password, Does.Contain("-"));

            password = _passwordBuilder
                .SetSeparator(DicewareSeparator.Underscore)
                .GeneratePassword();

            Assert.That(password, Does.Contain("_"));

            password = _passwordBuilder
                .SetSeparator(DicewareSeparator.None)
                .GeneratePassword();

            Assert.That(password, Does.Not.Contain("-"));
            Assert.That(password, Does.Not.Contain(" "));
            Assert.That(password, Does.Not.Contain("."));
            Assert.That(password, Does.Not.Contain("_"));
        }
    }
}
