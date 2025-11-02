using System;
using System.Security.Cryptography;
using System.Text;

namespace Alkhabeer.Core.Shared
{
    public static class HashHelper
    {
        /// <summary>
        /// Hashes a plain text string (such as a password) using SHA256.
        /// </summary>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));
            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = SHA256.HashData(bytes);

            // Convert to Base64 string for safe DB storage
            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Verifies a plain text password against a stored hash.
        /// </summary>
        public static bool VerifyPassword(string inputPassword, string storedHash)
        {
            if (string.IsNullOrEmpty(storedHash))
                return false;

            var inputHash = HashPassword(inputPassword);
            return inputHash == storedHash;
        }
    }
}
