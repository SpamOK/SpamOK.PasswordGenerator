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
        /// Test the GeneratePassword method async wrapper.
        /// </summary>
        /// <returns>Returns task.</returns>
        [Test]
        public async Task TestAsyncMethod()
        {
            var result = await _passwordBuilder.GeneratePasswordAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ToString(), Is.Not.Empty);
        }

        /// <summary>
        /// Test that generating a diceware password with default options works.
        /// </summary>
        [Test]
        public void TestPasswordGenerationDefault()
        {
            var password = _passwordBuilder.GeneratePassword().ToString();
            Assert.That(password, Is.Not.Empty);
        }

        /// <summary>
        /// Test that generating a diceware password with setting the length works.
        /// </summary>
        [Test]
        public void TestPasswordGenerationLength()
        {
            var password = _passwordBuilder
                .SetSeparator(DicewareSeparator.Space)
                .SetLength(1)
                .GeneratePassword()
                .ToString();

            Assert.That(password, Does.Not.Contain(' '));

            password = _passwordBuilder
                .SetSeparator(DicewareSeparator.Space)
                .SetLength(3)
                .GeneratePassword()
                .ToString();

            Assert.That(password.Split(' ').Count, Is.EqualTo(3));

            password = _passwordBuilder
                .SetSeparator(DicewareSeparator.Space)
                .SetLength(10)
                .GeneratePassword()
                .ToString();

            Assert.That(password.Split(' ').Count, Is.EqualTo(10));

            password = _passwordBuilder
                .SetSeparator(DicewareSeparator.Space)
                .SetLength(30)
                .GeneratePassword()
                .ToString();

            Assert.That(password.Split(' ').Count, Is.EqualTo(30));
        }

        /// <summary>
        /// Test that generating a diceware password with the English word list works.
        /// </summary>
        [Test]
        public void TestPasswordGenerationEnglish()
        {
            var password = _passwordBuilder
                .SetWordList(DicewareWordList.English)
                .GeneratePassword()
                .ToString();

            Assert.That(password, Is.Not.Empty);
        }

        /// <summary>
        /// Test that generating a diceware password with the Dutch word list works.
        /// </summary>
        [Test]
        public void TestPasswordGenerationDutch()
        {
            var password = _passwordBuilder
                .SetWordList(DicewareWordList.Dutch)
                .GeneratePassword()
                .ToString();

            Assert.That(password, Is.Not.Empty);
        }

        /// <summary>
        /// Test that generating a diceware password with the German word list works.
        /// </summary>
        [Test]
        public void TestPasswordGenerationGerman()
        {
            var password = _passwordBuilder
                .SetWordList(DicewareWordList.German)
                .GeneratePassword()
                .ToString();

            Assert.That(password, Is.Not.Empty);
        }

        /// <summary>
        /// Test that generating a diceware password with the Spanish word list works.
        /// </summary>
        [Test]
        public void TestPasswordGenerationSpanish()
        {
            var password = _passwordBuilder
                .SetWordList(DicewareWordList.Spanish)
                .GeneratePassword()
                .ToString();

            Assert.That(password, Is.Not.Empty);
        }

        /// <summary>
        /// Test that generating a diceware password with the French word list works.
        /// </summary>
        [Test]
        public void TestPasswordGenerationFrench()
        {
            var password = _passwordBuilder
                .SetWordList(DicewareWordList.French)
                .GeneratePassword()
                .ToString();

            Assert.That(password, Is.Not.Empty);
        }

        /// <summary>
        /// Test that generating a diceware password with the Ukrainian word list works.
        /// </summary>
        [Test]
        public void TestPasswordGenerationUkrainian()
        {
            var password = _passwordBuilder
                .SetWordList(DicewareWordList.Ukrainian)
                .GeneratePassword()
                .ToString();

            Assert.That(password, Is.Not.Empty);
        }

        /// <summary>
        /// Test that generating a diceware password with the Italian word list works.
        /// </summary>
        [Test]
        public void TestPasswordGenerationItalian()
        {
            var password = _passwordBuilder
                .SetWordList(DicewareWordList.Italian)
                .GeneratePassword()
                .ToString();

            Assert.That(password, Is.Not.Empty);
        }

        /// <summary>
        /// Test that generating a diceware password with configured separators works.
        /// </summary>
        [Test]
        public void TestPasswordGenerationSeparators()
        {
            var password = _passwordBuilder
                .SetSeparator(DicewareSeparator.Dot)
                .GeneratePassword()
                .ToString();

            Assert.That(password, Does.Contain("."));

            password = _passwordBuilder
                .SetSeparator(DicewareSeparator.Space)
                .GeneratePassword()
                .ToString();

            Assert.That(password, Does.Contain(" "));

            password = _passwordBuilder
                .SetSeparator(DicewareSeparator.Dash)
                .GeneratePassword()
                .ToString();

            Assert.That(password, Does.Contain("-"));

            password = _passwordBuilder
                .SetSeparator(DicewareSeparator.Underscore)
                .GeneratePassword()
                .ToString();

            Assert.That(password, Does.Contain("_"));

            password = _passwordBuilder
                .SetSeparator(DicewareSeparator.None)
                .GeneratePassword()
                .ToString();

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
                .GeneratePassword()
                .ToString();

            Assert.That(password, Is.EqualTo(password.ToLower()));

            int countWithFiveCapitals = 0;
            for (int i = 0; i < 100; i++)
            {
                password = _passwordBuilder
                    .SetCapitalization(DicewareCapitalization.TitleCase)
                    .GeneratePassword()
                    .ToString();

                // Check if there are exactly 5 capital letters
                if (password.CountCapitalLetters() == 5)
                {
                    countWithFiveCapitals++;
                }
            }

            // Assert that at least 50 out of the 100 passwords have exactly 5 capital letters.
            // Some words may not have 5 capital letters because of words that start with numbers, but at least 50% should.
            Assert.That(countWithFiveCapitals, Is.GreaterThanOrEqualTo(50), "Less than 50 passwords have exactly 5 capital letters.");

            password = _passwordBuilder
                .SetCapitalization(DicewareCapitalization.Lowercase)
                .GeneratePassword()
                .ToString();

            Assert.That(password, Is.EqualTo(password.ToLower()));

            password = _passwordBuilder
                .SetCapitalization(DicewareCapitalization.Uppercase)
                .GeneratePassword()
                .ToString();

            Assert.That(password, Is.EqualTo(password.ToUpper()));

            int trials = 100; // Number of times to generate the password
            bool foundCapitalLetter = false;

            for (int i = 0; i < trials; i++)
            {
                password = _passwordBuilder
                    .SetCapitalization(DicewareCapitalization.Random)
                    .GeneratePassword()
                    .ToString();

                if (password.CountCapitalLetters() > 0)
                {
                    foundCapitalLetter = true;
                    break;
                }
            }

            Assert.That(foundCapitalLetter, Is.True, "Expected at least one password with a capital letter out of multiple trials.");
        }

        /// <summary>
        /// Test that generating a diceware password with salt works without errors.
        /// </summary>
        [Test]
        public void TestPasswordGenerationSaltMethod()
        {
            // Assert that generating a password with salt does not throw an exception
            // and that the password is not empty.
            // Internals are tested in TestPasswordGenerationSaltInternals().
            var password = _passwordBuilder
                .SetSalt(DicewareSalt.None)
                .GeneratePassword()
                .ToString();
            Assert.That(password, Is.Not.Empty);

            password = _passwordBuilder
                .SetSalt(DicewareSalt.Prefix)
                .GeneratePassword()
                .ToString();
            Assert.That(password, Is.Not.Empty);

            password = _passwordBuilder
                .SetSalt(DicewareSalt.Sprinkle)
                .GeneratePassword()
                .ToString();
            Assert.That(password, Is.Not.Empty);

            password = _passwordBuilder
                .SetSalt(DicewareSalt.Suffix)
                .GeneratePassword()
                .ToString();
            Assert.That(password, Is.Not.Empty);
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
            Assert.That(_passwordBuilder.AddSalt(password, salt), Is.EqualTo(password));

            _passwordBuilder.SetSalt(DicewareSalt.Prefix);
            Assert.That(_passwordBuilder.AddSalt(password, salt), Is.EqualTo(salt + password));

            _passwordBuilder.SetSalt(DicewareSalt.Sprinkle);
            Assert.That(_passwordBuilder.AddSalt(password, salt), Does.Contain("x"));

            _passwordBuilder.SetSalt(DicewareSalt.Suffix);
            Assert.That(_passwordBuilder.AddSalt(password, salt), Is.EqualTo(password + salt));
        }

        /// <summary>
        /// Test that trying to load a non-existing word list throws an exception.
        /// </summary>
        [Test]
        public void TestPasswordGenerationNonExistingWordList()
        {
            DicewareLookup lookup = new DicewareLookup(DicewareWordList.English);
            Assert.Throws<FileNotFoundException>(() => lookup.LoadWords("unknown-wordlist"));
        }

        /// <summary>
        /// Test that trying to load a word list with incorrect structure throws an exception.
        /// </summary>
        [Test]
        public void TestPasswordGenerationIncorrectStructureWordList()
        {
            DicewareLookup lookup = new DicewareLookup(DicewareWordList.English);
            Assert.Throws<InvalidDataException>(() => lookup.LoadWords("SpamOK.PasswordGenerator.Algorithms.Diceware.WordLists.TestAssets.error.diceware"));
        }

        /// <summary>
        /// Test that generating a diceware password with hackerify mode enabled works as expected.
        /// </summary>
        [Test]
        public void TestPasswordGenerationHackerify()
        {
            for (int i = 0; i < 100; i++)
            {
                var password = _passwordBuilder
                    .HackerifyPassword(true)
                    .GeneratePassword()
                    .ToString();

                // Test that the password contains no characters that should have
                // been replaced with l33tspeak alternatives.
                Assert.Multiple(() =>
                {
                    Assert.That(password, Does.Not.Contain("a"));
                    Assert.That(password, Does.Not.Contain("b"));
                    Assert.That(password, Does.Not.Contain("e"));
                    Assert.That(password, Does.Not.Contain("i"));
                    Assert.That(password, Does.Not.Contain("l"));
                    Assert.That(password, Does.Not.Contain("o"));
                    Assert.That(password, Does.Not.Contain("t"));
                });
            }
        }
    }
}
