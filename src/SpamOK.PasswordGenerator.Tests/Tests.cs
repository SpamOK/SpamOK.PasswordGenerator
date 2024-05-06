//-----------------------------------------------------------------------
// <copyright file="Tests.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------
namespace SpamOK.PasswordGenerator.Tests
{
    /// <summary>
    /// Main tests.
    /// </summary>
    public class Tests
    {
        private PasswordBuilder _passwordBuilder;

        /// <summary>
        /// Setup method.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _passwordBuilder = new PasswordBuilder();
        }

        /// <summary>
        /// Ensure that DisableAllOptions works as expected.
        /// </summary>
        [Test]
        public void TestDisableAllOptionsException()
        {
            // Attempt to generate a password with all options disabled.
            // This should throw an exception.
            Assert.Throws<InvalidOperationException>(() => _passwordBuilder
                .DisableAllOptions()
                .GeneratePassword());
        }

        /// <summary>
        /// Ensure that EnableAllOptions works as expected.
        /// </summary>
        [Test]
        public void TestEnableAllOptionsException()
        {
            // Attempt to generate a password with all options enabled.
            string password = _passwordBuilder
                .EnableAllOptions()
                .GeneratePassword();

            Assert.IsNotEmpty(password);
        }

        /// <summary>
        /// Test password length option.
        /// </summary>
        [Test]
        public void TestPasswordLength()
        {
            string password = _passwordBuilder
                .SetLength(12)
                .GeneratePassword();

            Assert.That(password.Length, Is.EqualTo(12));
        }

        /// <summary>
        /// Test UseLowercaseLetters() option.
        /// </summary>
        [Test]
        public void TestUseLowercaseLetters()
        {
            _passwordBuilder = _passwordBuilder
                .DisableAllOptions()
                .UseLowercaseLetters(true);

            // Generate 100 passwords and check that all of them contain only lowercase letters.
            for (int i = 0; i < 100; i++)
            {
                string password = _passwordBuilder.GeneratePassword();
                Assert.That(password, Does.Match("^[a-z]+$"));
            }
        }

        /// <summary>
        /// Test UseUppercaseLetters() option.
        /// </summary>
        [Test]
        public void TestUseUppercaseLetters()
        {
            _passwordBuilder = _passwordBuilder
                .DisableAllOptions()
                .UseUppercaseLetters(true);

            // Generate 100 passwords and check that all of them contain only lowercase letters.
            for (int i = 0; i < 100; i++)
            {
                string password = _passwordBuilder.GeneratePassword();
                Assert.That(password, Does.Match("^[A-Z]+$"));
            }
        }

        /// <summary>
        /// Test UseNumbers() option.
        /// </summary>
        [Test]
        public void TestUseNumbers()
        {
            _passwordBuilder = _passwordBuilder
                .DisableAllOptions()
                .UseNumbers(true);

            // Generate 100 passwords and check that all of them contain only uppercase letters.
            for (int i = 0; i < 100; i++)
            {
                string password = _passwordBuilder.GeneratePassword();
                Assert.That(password, Does.Match("^[0-9]+$"));
            }
        }

        /// <summary>
        /// Test UseSpecialChars() option.
        /// </summary>
        [Test]
        public void TestUseSpecialChars()
        {
            _passwordBuilder = _passwordBuilder
                .DisableAllOptions()
                .UseSpecialChars(true);

            // Generate 100 passwords and check that all of them only contain special characters.
            for (int i = 0; i < 100; i++)
            {
                string password = _passwordBuilder.GeneratePassword();
                Assert.That(password, Does.Match(@"^[\!@#$%^&*()_=+[\]{}|;:,.<>?-]+$"));
            }
        }

        /// <summary>
        /// Test ambiguous characters option.
        /// </summary>
        [Test]
        public void TestPasswordAmbigiousChars()
        {
            _passwordBuilder = _passwordBuilder
                .EnableAllOptions()
                .UseNonAmbiguousChars(true);

            // Generate 100 passwords and check that none of them contain ambiguous characters
            for (int i = 0; i < 100; i++)
            {
                string password = _passwordBuilder.GeneratePassword();

                Assert.That(password, Does.Not.Contain("l"));
                Assert.That(password, Does.Not.Contain("1"));
                Assert.That(password, Does.Not.Contain("I"));
                Assert.That(password, Does.Not.Contain("o"));
                Assert.That(password, Does.Not.Contain("0"));
                Assert.That(password, Does.Not.Contain("O"));
                Assert.That(password, Does.Not.Contain("_"));
                Assert.That(password, Does.Not.Contain("{"));
            }
        }

        /// <summary>
        /// Test ExcludeChars() option.
        /// </summary>
        [Test]
        public void TestExcludeChars()
        {
            _passwordBuilder = _passwordBuilder
                .EnableAllOptions()
                .ExcludeChars(@"12345+\!_abcd");

            // Generate 100 passwords and check that all of them only contain special characters.
            for (int i = 0; i < 100; i++)
            {
                string password = _passwordBuilder.GeneratePassword();

                Assert.That(password, Does.Not.Contain("1"));
                Assert.That(password, Does.Not.Contain("2"));
                Assert.That(password, Does.Not.Contain("3"));
                Assert.That(password, Does.Not.Contain("4"));
                Assert.That(password, Does.Not.Contain("5"));
                Assert.That(password, Does.Not.Contain("+"));
                Assert.That(password, Does.Not.Contain("\\"));
                Assert.That(password, Does.Not.Contain("!"));
                Assert.That(password, Does.Not.Contain("a"));
                Assert.That(password, Does.Not.Contain("b"));
                Assert.That(password, Does.Not.Contain("c"));
                Assert.That(password, Does.Not.Contain("d"));
            }
        }

        /// <summary>
        /// Ensure that every password generated is unique.
        /// </summary>
        [Test]
        public void TestRandomPassword()
        {
            _passwordBuilder = new PasswordBuilder();

            // Generate 100 passwords and check that none of them are the same
            var passwords = new HashSet<string>();
            for (int i = 0; i < 100; i++)
            {
                string password = _passwordBuilder
                    .SetLength(12)
                    .UseNumbers(true)
                    .UseSpecialChars(true)
                    .UseNonAmbiguousChars(false)
                    .ExcludeChars("l1Io0O")
                    .UseAlgorithm(PasswordAlgorithm.Basic)
                    .GeneratePassword();

                Assert.That(passwords.Add(password), Is.True);
            }
        }

        /// <summary>
        /// Ensure that every password generated is unique.
        /// </summary>
        [Test]
        public void TestLowercaseOnly()
        {
            _passwordBuilder = new PasswordBuilder();

            // Generate 100 passwords and check that none of them are the same
            for (int i = 0; i < 100; i++)
            {
                string password = _passwordBuilder
                    .DisableAllOptions()
                    .UseLowercaseLetters(true)
                    .GeneratePassword();

                // Assert that password only contains lowercase letters
                Assert.That(password, Does.Match("^[a-z]+$"));
            }
        }
    }
}
