//-----------------------------------------------------------------------
// <copyright file="ExtensionsTest.cs" company="SpamOK">
// Copyright (c) SpamOK. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------
namespace SpamOK.PasswordGenerator.Tests
{
    using SpamOK.PasswordGenerator.Algorithms.Diceware.Extensions;

    /// <summary>
    /// Tests that cover extension methods.
    /// </summary>
    public class ExtensionsTest
    {
        /// <summary>
        /// Defines a test enum.
        /// </summary>
        private enum TestEnum
        {
            /// <summary>
            /// Test value with custom attributes.
            /// </summary>
            [SeparatorCharacter('\0')]
            [ResourceName("SpamOK.PasswordGenerator.Algorithms.Diceware.WordLists.en.diceware")]
            WithAttributes,

            /// <summary>
            /// Test value without attributes.
            /// </summary>
            WithoutAttributes,
        }

        /// <summary>
        /// Test that calling enum extension methods with invalid enum values throws an exception.
        /// </summary>
        [Test]
        public void TestEnumExtensionAttributeValid()
        {
            // Test that calling extension methods on an enum value with attributes works.
            Assert.That(TestEnum.WithAttributes.GetSeparatorCharacter(), Is.EqualTo('\0'));
            Assert.That(TestEnum.WithAttributes.GetResourceName(), Is.EqualTo("SpamOK.PasswordGenerator.Algorithms.Diceware.WordLists.en.diceware"));
        }

        /// <summary>
        /// Test that calling enum extension methods with invalid enum values throws an exception.
        /// </summary>
        [Test]
        public void TestEnumExtensionAttributeMissingException()
        {
            // Test that calling extension methods on an enum value without attributes throws an exception.
            Assert.Throws<ArgumentNullException>(() => TestEnum.WithoutAttributes.GetSeparatorCharacter());
            Assert.Throws<ArgumentNullException>(() => TestEnum.WithoutAttributes.GetResourceName());
        }
    }
}
