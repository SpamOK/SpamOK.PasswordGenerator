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
    using SpamOK.PasswordGenerator.Tests.Extensions;

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
        /// Test that generating a diceware password with the German word list works.
        /// </summary>
        [Test]
        public void TestPasswordGenerationGerman()
        {
            Assert.IsNotEmpty(_passwordBuilder
                .SetWordList(DicewareWordList.German)
                .GeneratePassword());
        }

        /// <summary>
        /// Test that generating a diceware password with the Spanish word list works.
        /// </summary>
        [Test]
        public void TestPasswordGenerationSpanish()
        {
            Assert.IsNotEmpty(_passwordBuilder
                .SetWordList(DicewareWordList.Spanish)
                .GeneratePassword());
        }

        /// <summary>
        /// Test that generating a diceware password with the French word list works.
        /// </summary>
        [Test]
        public void TestPasswordGenerationFrench()
        {
            Assert.IsNotEmpty(_passwordBuilder
                .SetWordList(DicewareWordList.French)
                .GeneratePassword());
        }

        /// <summary>
        /// Test that generating a diceware password with the Ukrainian word list works.
        /// </summary>
        [Test]
        public void TestPasswordGenerationUkrainian()
        {
            Assert.IsNotEmpty(_passwordBuilder
                .SetWordList(DicewareWordList.Ukrainian)
                .GeneratePassword());
        }

        /// <summary>
        /// Test that generating a diceware password with the Italian word list works.
        /// </summary>
        [Test]
        public void TestPasswordGenerationItalian()
        {
            Assert.IsNotEmpty(_passwordBuilder
                .SetWordList(DicewareWordList.Italian)
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

        /// <summary>
        /// Test that generating a diceware password with capitalization works.
        /// </summary>
        [Test]
        public void TestPasswordGenerationCapitalization()
        {
            var password = _passwordBuilder
                .SetCapitalization(DicewareCapitalization.None)
                .GeneratePassword();

            Assert.That(password, Is.EqualTo(password.ToLower()));

            password = _passwordBuilder
                .SetCapitalization(DicewareCapitalization.TitleCase)
                .GeneratePassword();

            // Assert that there are exactly 5 capital letters in the password because
            // there are 5 words in the password.
            Assert.That(password.CountCapitalLetters(), Is.EqualTo(5));

            password = _passwordBuilder
                .SetCapitalization(DicewareCapitalization.Uppercase)
                .GeneratePassword();

            Assert.That(password, Is.EqualTo(password.ToUpper()));

            int trials = 100; // Number of times to generate the password
            bool foundCapitalLetter = false;

            for (int i = 0; i < trials; i++)
            {
                password = _passwordBuilder
                    .SetCapitalization(DicewareCapitalization.Random)
                    .GeneratePassword();

                if (password.CountCapitalLetters() > 0)
                {
                    foundCapitalLetter = true;
                    break;
                }
            }

            Assert.IsTrue(foundCapitalLetter, "Expected at least one password with a capital letter out of multiple trials.");
        }

        /// <summary>
        /// Test that generating a diceware password with salt works without errors.
        /// </summary>
        [Test]
        public void TestPasswordGenerationSaltMethod()
        {
            // Not actually asserting anything here because of the random nature of the salt.
            // Just testing that the method doesn't throw an exception.
            // Internals are tested in TestPasswordGenerationSaltInternals().
            _passwordBuilder
                .SetSalt(DicewareSalt.None)
                .GeneratePassword();

            _passwordBuilder
                .SetSalt(DicewareSalt.Prefix)
                .GeneratePassword();

            _passwordBuilder
                .SetSalt(DicewareSalt.Sprinkle)
                .GeneratePassword();

            _passwordBuilder
                .SetSalt(DicewareSalt.Suffix)
                .GeneratePassword();
        }

        /// <summary>
        /// Test that generating a diceware password with salt works testing internals.
        /// </summary>
        [Test]
        public void TestPasswordGenerationSaltInternals()
        {
            var password = "aaaaaaaaaa";
            char salt = 'x';

            _passwordBuilder.SetSalt(DicewareSalt.None);
            Assert.That(_passwordBuilder.AddSalt(salt, password), Is.EqualTo(password));

            _passwordBuilder.SetSalt(DicewareSalt.Prefix);
            Assert.That(_passwordBuilder.AddSalt(salt, password), Is.EqualTo(salt + password));

            _passwordBuilder.SetSalt(DicewareSalt.Sprinkle);
            Assert.That(_passwordBuilder.AddSalt(salt, password), Does.Contain("x"));

            _passwordBuilder.SetSalt(DicewareSalt.Suffix);
            Assert.That(_passwordBuilder.AddSalt(salt, password), Is.EqualTo(password + salt));
        }
    }
}
