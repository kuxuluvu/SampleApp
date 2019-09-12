using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.Helper
{
    public struct SampleHelper
    {
        public static string CreateSalt(int size)
        {
            var rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

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

        public static string HashString(string text)
        {
            var salt = CreateSalt(16);

            return GenerateSaltedHash(Encoding.ASCII.GetBytes(text), Convert.FromBase64String(salt));
        }
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
