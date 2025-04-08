using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SharpView.Utils
{
    /// <summary>
    /// Provides FIPS-compliant hashing functionality to replace LibZ's MD5 usage
    /// </summary>
    public static class FipsCompliantHasher
    {
        /// <summary>
        /// Computes a FIPS-compliant hash of the input string using SHA-256
        /// </summary>
        /// <param name="input">The input string to hash</param>
        /// <returns>A hexadecimal string representation of the hash</returns>
        public static string ComputeHash(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            using (var sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }

        /// <summary>
        /// Computes a FIPS-compliant hash of the input file using SHA-256
        /// </summary>
        /// <param name="filePath">The path to the file to hash</param>
        /// <returns>A hexadecimal string representation of the hash</returns>
        public static string ComputeFileHash(string filePath)
        {
            if (!File.Exists(filePath))
                return string.Empty;

            using (var sha256 = SHA256.Create())
            using (var fileStream = File.OpenRead(filePath))
            {
                byte[] hashBytes = sha256.ComputeHash(fileStream);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }
    }
} 