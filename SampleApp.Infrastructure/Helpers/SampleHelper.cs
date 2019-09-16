// ***********************************************************************
// Assembly         : SampleApp.Infrastructure
// Author           : duc.nguyen
// Created          : 09-16-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="SampleHelper.cs" company="SampleApp.Infrastructure">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Security.Cryptography;
using System.Text;

namespace SampleApp.Infrastructure.Helper
{
    /// <summary>
    /// Struct SampleHelper
    /// </summary>
    public struct SampleHelper
    {
        /// <summary>
        /// Creates the salt.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string CreateSalt()
        {
            var rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[32];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// Generates the salted hash.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>System.String.</returns>
        public static string GenerateSaltedHash(byte[] text, byte[] salt)
        {
            var  algorithm = new SHA256Managed();

            var textWithSaltBytes = new byte[text.Length + salt.Length];

            for (int i = 0; i < text.Length; i++)
            {
                textWithSaltBytes[i] = text[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                textWithSaltBytes[text.Length + i] = salt[i];
            }

            return Convert.ToBase64String(algorithm.ComputeHash(textWithSaltBytes));
        }

        /// <summary>
        /// Hashes the string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>System.String.</returns>
        public static string HashString(string text)
        {
            var salt = CreateSalt();

            return GenerateSaltedHash(Encoding.ASCII.GetBytes(text), Convert.FromBase64String(salt));
        }
        /// <summary>
        /// Compares the byte arrays.
        /// </summary>
        /// <param name="array1">The array1.</param>
        /// <param name="array2">The array2.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
