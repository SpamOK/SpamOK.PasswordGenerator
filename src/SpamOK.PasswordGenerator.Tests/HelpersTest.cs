//-----------------------------------------------------------------------
// <copyright file="HelpersTest.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------
namespace SpamOK.PasswordGenerator.Tests
{
    using SpamOK.PasswordGenerator.Helpers;

    /// <summary>
    /// Tests that cover helper methods.
    /// </summary>
    public class HelpersTest
    {
        /// <summary>
        /// Test that random helper number between method work as expected.
        /// </summary>
        [Test]
        public void TestRandomHelperNumberBetween()
        {
            Assert.Multiple(() =>
            {
                Assert.That(RandomHelper.GenerateRandomNumberBetween(0, 10), Is.InRange(0, 10));

                // Assert that specfiying a range with the same start and end value will always return that value.
                Assert.That(RandomHelper.GenerateRandomNumberBetween(5, 5), Is.EqualTo(5));

                // Assert that specifying a minimum value that is higher than the maximum value will throw an exception.
                Assert.Throws<ArgumentOutOfRangeException>(() => RandomHelper.GenerateRandomNumberBetween(10, 5));
            });
        }

        /// <summary>
        /// Test that random helper boolean method work as expected.
        /// </summary>
        [Test]
        public void TestRandomHelperBoolean()
        {
            Assert.Multiple(() =>
            {
                Assert.That(RandomHelper.GenerateRandomBoolean(100), Is.True);
                Assert.That(RandomHelper.GenerateRandomBoolean(0), Is.False);

                // Assert that when generating 100 booleans with probability of 90, at least one of them is true.
                var test = Enumerable.Range(0, 100).Select(_ => RandomHelper.GenerateRandomBoolean(90)).Any(b => b);
                Assert.That(test, Is.True);
            });
        }
    }
}
