// Copyright (c) 2024 SpamOK. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.mds in the project root for license information.
namespace SpamOK.PasswordGenerator.Tests
{
    /// <summary>
    /// Main tests.
    /// </summary>
    public class Tests
    {
        /// <summary>
        /// Setup method.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Test password length option.
        /// </summary>
        [Test]
        public void TestPasswordLength()
        {
            var passwordBuilder = new SpamOK.PasswordGenerator.PasswordBuilder();
            string password = passwordBuilder
                .SetLength(12)
                .UseNumbers(true)
                .UseSpecialChars(true)
                .UseNonAmbiguousChars(false)
                .ExcludeChars("l1Io0O")
                .UseAlgorithm(PasswordAlgorithm.Basic)
                .Build();

            Assert.That(password.Length, Is.EqualTo(12));
        }

        /// <summary>
        /// Test ambiguous characters option.
        /// </summary>
        [Test]
        public void TestPasswordAmbigiousChars()
        {
            var passwordBuilder = new SpamOK.PasswordGenerator.PasswordBuilder();
            passwordBuilder = passwordBuilder
                .SetLength(12)
                .UseNumbers(true)
                .UseSpecialChars(true)
                .UseNonAmbiguousChars(true)
                .UseAlgorithm(PasswordAlgorithm.Basic);

            // Generate 100 passwords and check that none of them contain ambiguous characters
            for (int i = 0; i < 100; i++)
            {
                string password = passwordBuilder.Build();

                Assert.That(password, Does.Not.Contain("l"));
                Assert.That(password, Does.Not.Contain("1"));
                Assert.That(password, Does.Not.Contain("I"));
                Assert.That(password, Does.Not.Contain("o"));
                Assert.That(password, Does.Not.Contain("0"));
                Assert.That(password, Does.Not.Contain("O"));
            }
        }
    }
}