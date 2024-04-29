using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SpamOK.PasswordGenerator
{

    public enum PasswordAlgorithm
    {
        Basic,
        Dictionary,
        Diceware
    }

    public class PasswordBuilder
    {
        private int _length = 8;
        private bool _useNumbers = true;
        private bool _useSpecialChars = true;
        private bool _useNonAmbiguousChars = false;
        private string _excludedChars = "";
        private PasswordAlgorithm _algorithm = PasswordAlgorithm.Basic;

        public PasswordBuilder SetLength(int length)
        {
            _length = length;
            return this;
        }

        public PasswordBuilder UseNumbers(bool useNumbers)
        {
            _useNumbers = useNumbers;
            return this;
        }

        public PasswordBuilder UseSpecialChars(bool useSpecial)
        {
            _useSpecialChars = useSpecial;
            return this;
        }

        public PasswordBuilder UseNonAmbiguousChars(bool useNonAmbiguous)
        {
            _useNonAmbiguousChars = useNonAmbiguous;
            return this;
        }

        public PasswordBuilder ExcludeChars(string excludedChars)
        {
            _excludedChars = excludedChars;
            return this;
        }

        public PasswordBuilder UseAlgorithm(PasswordAlgorithm algorithm)
        {
            _algorithm = algorithm;
            return this;
        }

        public string Build()
        {
            switch (_algorithm)
            {
                case PasswordAlgorithm.Basic:
                    return GenerateBasicPassword();
                case PasswordAlgorithm.Dictionary:
                case PasswordAlgorithm.Diceware:
                    throw new NotImplementedException("Dictionary and Diceware algorithms are not implemented yet.");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private string GenerateBasicPassword()
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";
            const string specialChars = "!@#$%^&*()-_=+[]{}|;:,.<>?";
            const string nonAmbiguousChars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789";

            StringBuilder charSet = new StringBuilder();
            charSet.Append(letters);

            if (_useNumbers)
            {
                charSet.Append(numbers);
            }

            if (_useSpecialChars)
            {
                charSet.Append(specialChars);
            }

            if (_useNonAmbiguousChars)
            {
                charSet = new StringBuilder(new string(charSet.ToString().Intersect(nonAmbiguousChars).ToArray()));
            }

            foreach (char c in _excludedChars)
            {
                charSet = charSet.Replace(c.ToString(), "");
            }

            return GenerateRandomPassword(_length, charSet.ToString());
        }

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
    }
}