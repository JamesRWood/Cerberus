namespace Cerberus.Controllers
{
    using Cerberus.Contracts.Controllers;
    using System;
    using System.Security.Cryptography;

    public class PasswordHashController : IPasswordHashController
    {
        private const int SaltLength = 20;
        private const int HashLength = 30;
        private const int IntIterations = 10000;

        public string HashString(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Password");
            }
            else
            {
                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltLength]);

                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, IntIterations);
                var hash = pbkdf2.GetBytes(HashLength);

                var hashBytes = new byte[SaltLength + HashLength];
                Array.Copy(salt, 0, hashBytes, 0, SaltLength);
                Array.Copy(hash, 0, hashBytes, SaltLength, HashLength);

                var savedPasswordHash = Convert.ToBase64String(hashBytes);

                return savedPasswordHash;
            }
        }

        public bool ValidatePassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Password");
            }

            if (string.IsNullOrEmpty(hashedPassword))
            {
                throw new ArgumentNullException("HashedPassword");
            }

            var hashBytes = Convert.FromBase64String(hashedPassword);
            var salt = new byte[SaltLength];
            Array.Copy(hashBytes, 0, salt, 0, SaltLength);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, IntIterations);
            var hash = pbkdf2.GetBytes(HashLength);

            for (var i = 0; i < HashLength; i++)
            {
                if (hashBytes[i + SaltLength] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
