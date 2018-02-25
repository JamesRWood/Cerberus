using Cerberus.Contracts.Controllers;

namespace Cerberus.Controllers
{
    using System;
    using System.Security.Cryptography;

    public class PasswordHashController : IPasswordHashController
    {
        private const int SaltLength = 20;
        private const int HashLength = 30;
        private const int IntIterations = 10000;
        private const string InputExceptionMessage = "Value cannot be null or empty.";

        public string HashString(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password), InputExceptionMessage);
            }

            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltLength]);

            var hash = GetHash(password, salt);

            var hashBytes = new byte[SaltLength + HashLength];
            Array.Copy(salt, 0, hashBytes, 0, SaltLength);
            Array.Copy(hash, 0, hashBytes, SaltLength, HashLength);

            return Convert.ToBase64String(hashBytes);
        }

        public bool ValidatePassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password), InputExceptionMessage);
            }

            if (string.IsNullOrEmpty(hashedPassword))
            {
                throw new ArgumentNullException(nameof(hashedPassword), InputExceptionMessage);
            }

            var hashBytes = Convert.FromBase64String(hashedPassword);
            var salt = new byte[SaltLength];
            Array.Copy(hashBytes, 0, salt, 0, SaltLength);

            var hash = GetHash(password, salt);

            for (var i = 0; i < HashLength; i++)
            {
                if (hashBytes[i + SaltLength] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }

        private byte[] GetHash(string password, byte[] salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, IntIterations);
            return pbkdf2.GetBytes(HashLength);
        }
    }
}
