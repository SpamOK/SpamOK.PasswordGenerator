//-----------------------------------------------------------------------
// <copyright file="PasswordBuilder.cs" company="SpamOK">
//   Copyright (c) SpamOK. All Rights Reserved.
//   Licensed under the MIT License. See LICENSE.md in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------
namespace SpamOK.PasswordGenerator
{
	using System;
	using System.Linq;
	using System.Security.Cryptography;
	using System.Text;

	/// <summary>
	/// Password generation algorithm.
	/// </summary>
	public enum PasswordAlgorithm
	{
		/// <summary>
		/// Basic algorithm.
		/// </summary>
		Basic,

		/// <summary>
		/// Dictionary algorithm which uses a list of words to generate a passphrase.
		/// </summary>
		Dictionary,

		/// <summary>
		/// Dictionary algorithm which uses a list of words to generate a passphrase.
		/// </summary>
		Diceware,
	}

	/// <summary>
	/// Password builder class.
	/// </summary>
	public class PasswordBuilder
	{
		private int length = 8;
		private bool useNumbers = true;
		private bool useSpecialChars = true;
		private bool useNonAmbiguousChars = false;
		private string excludedChars = string.Empty;
		private PasswordAlgorithm algorithm = PasswordAlgorithm.Basic;

		/// <summary>
		/// Configure the length of the password.
		/// </summary>
		/// <param name="length">Length in characters.</param>
		/// <returns>Updated PasswordBuilder instance.</returns>
		public PasswordBuilder SetLength(int length)
		{
			this.length = length;
			return this;
		}

		/// <summary>
		/// Configure whether to use numbers in the password.
		/// </summary>
		/// <param name="useNumbers">Boolean whether to use numbers in the password or not. Defaults to TRUE.</param>
		/// <returns>Updated PasswordBuilder instance.</returns>
		public PasswordBuilder UseNumbers(bool useNumbers)
		{
			this.useNumbers = useNumbers;
			return this;
		}

		/// <summary>
		/// Configure whether to use special characters in the password.
		/// </summary>
		/// <param name="useSpecial">Boolean whether to use special chars in the password or not. Defaults to TRUE.</param>
		/// <returns>Updated PasswordBuilder instance.</returns>
		public PasswordBuilder UseSpecialChars(bool useSpecial)
		{
			this.useSpecialChars = useSpecial;
			return this;
		}

		/// <summary>
		/// Configure whether to only use non-ambiguous characters in the password.
		/// </summary>
		/// <param name="useNonAmbiguous">Boolean whether to only use non-ambigious chars in the password or not.
		/// Ambigious characters means they look very much the same such as 0 (zero) and O (capital o).</param>
		/// <returns>Updated PasswordBuilder instance.</returns>
		public PasswordBuilder UseNonAmbiguousChars(bool useNonAmbiguous)
		{
			this.useNonAmbiguousChars = useNonAmbiguous;
			return this;
		}

		/// <summary>
		/// Configure characters to exclude from the password.
		/// </summary>
		/// <param name="excludedChars">String of characters to exclude from the generated password.</param>
		/// <returns>Updated PasswordBuilder instance.</returns>
		public PasswordBuilder ExcludeChars(string excludedChars)
		{
			this.excludedChars = excludedChars;
			return this;
		}

		/// <summary>
		/// Specify the algorithm to use for generating the password. Defaults to PasswordAlgorithm.Basic.
		/// </summary>
		/// <param name="algorithm">PasswordAlgorithm enum.</param>
		/// <returns>Updated PasswordBuilder instance.</returns>
		public PasswordBuilder UseAlgorithm(PasswordAlgorithm algorithm)
		{
			this.algorithm = algorithm;
			return this;
		}

		/// <summary>
		/// Build the password based on the configured settings.
		/// </summary>
		/// <returns>Generated password.</returns>
		/// <exception cref="NotImplementedException">Thrown if algorithm is unsupported.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if algorithm is unknown.</exception>
		public string Build()
		{
			switch (this.algorithm)
			{
				case PasswordAlgorithm.Basic:
					return this.GenerateBasicPassword();
				case PasswordAlgorithm.Dictionary:
				case PasswordAlgorithm.Diceware:
					throw new NotImplementedException("Dictionary and Diceware algorithms are not implemented yet.");
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		/// <summary>
		/// Generate a random password based on the specified length and character set.
		/// </summary>
		/// <param name="length">Desired length of the password.</param>
		/// <param name="charSet">Allowed characters to use in the password.</param>
		/// <returns>Generated password.</returns>
		private static string GenerateRandomPassword(int length, string charSet)
		{
			byte[] randomBytes = new byte[length];
			using (var rng = new RNGCryptoServiceProvider())
			{
				rng.GetBytes(randomBytes);
			}

			char[] chars = new char[length];
			for (int i = 0; i < length; i++)
			{
				chars[i] = charSet[randomBytes[i] % charSet.Length];
			}

			return new string(chars);
		}

		/// <summary>
		/// Generate a random password based on the specified length and character set.
		/// </summary>
		/// <returns>Generated password.</returns>
		private string GenerateBasicPassword()
		{
			const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
			const string numbers = "0123456789";
			const string specialChars = "!@#$%^&*()-_=+[]{}|;:,.<>?";
			const string nonAmbiguousChars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789";

			StringBuilder charSet = new StringBuilder();
			charSet.Append(letters);

			if (this.useNumbers)
			{
				charSet.Append(numbers);
			}

			if (this.useSpecialChars)
			{
				charSet.Append(specialChars);
			}

			if (this.useNonAmbiguousChars)
			{
				charSet = new StringBuilder(new string(charSet.ToString().Intersect(nonAmbiguousChars).ToArray()));
			}

			foreach (char c in this.excludedChars)
			{
				charSet = charSet.Replace(c.ToString(), string.Empty);
			}

			return GenerateRandomPassword(this.length, charSet.ToString());
		}
	}
}
