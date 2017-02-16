#region Build Information
// octapush.Utilities : AES256.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2017-01-18
// CratedTime  : 19:55
#endregion

#region Namespaces
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace octapush.Utilities.Encryption
{
    public static class Aes256
    {
        #region bytes
        public static byte[] Aes256_Encrypt(this byte[] input, byte[] password)
        {
            byte[] encBytes;
            var saltBytes = new byte[] {0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8};
            using (var ms = new MemoryStream())
            {
                using (var rm = new RijndaelManaged
                {
                    BlockSize = 0x80,
                    KeySize = 0x100,
                    Mode = CipherMode.CBC
                })
                {
                    var key = new Rfc2898DeriveBytes(password, saltBytes, 0x3e8);
                    rm.Key = key.GetBytes(rm.KeySize/8);
                    rm.IV = key.GetBytes(rm.BlockSize/8);

                    using (var cs = new CryptoStream(ms, rm.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(input, 0, input.Length);
                        cs.Close();
                    }

                    encBytes = ms.ToArray();
                }
            }

            return encBytes;
        }

        public static byte[] Aes256_Decrypt(this byte[] input, byte[] password)
        {
            byte[] decBytes;
            var saltBytes = new byte[] {0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8};
            using (var ms = new MemoryStream())
            {
                using (var rm = new RijndaelManaged
                {
                    BlockSize = 0x80,
                    KeySize = 0x100,
                    Mode = CipherMode.CBC
                })
                {
                    var key = new Rfc2898DeriveBytes(password, saltBytes, 0x3e8);
                    rm.Key = key.GetBytes(rm.KeySize/8);
                    rm.IV = key.GetBytes(rm.BlockSize/8);

                    using (var cs = new CryptoStream(ms, rm.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(input, 0, input.Length);
                        cs.Close();
                    }

                    decBytes = ms.ToArray();
                }
            }

            return decBytes;
        }
        #endregion bytes

        #region string
        public static string Aes256_Encrypt(this string input, string password)
        {
            return Convert
                .ToBase64String(
                    Encoding
                        .UTF8
                        .GetBytes(input)
                        .Aes256_Encrypt(
                            SHA256
                                .Create()
                                .ComputeHash(Encoding.UTF8.GetBytes(password))
                        )
                );
        }

        public static string Aes256_Decrypt(this string input, string password)
        {
            return Encoding
                .UTF8
                .GetString(
                    Convert
                        .FromBase64String(input)
                        .Aes256_Decrypt(
                            SHA256
                                .Create()
                                .ComputeHash(Encoding.UTF8.GetBytes(password))
                        )
                );
        }
        #endregion string
    }
}