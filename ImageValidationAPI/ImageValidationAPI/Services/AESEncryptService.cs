using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ImageValidationAPI.Services
{
    public class AESEncryptService : BasicService
    {

        private byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        private int BlockSize = 128;

        public AESEncryptService()
        {

        }

        public string Encrypt(string teks)
        {
            string result = string.Empty;
            try
            {
                
                byte[] bytes = Encoding.Unicode.GetBytes(teks);
                //Encrypt
                SymmetricAlgorithm crypt = Aes.Create();
                HashAlgorithm hash = MD5.Create();
                crypt.BlockSize = BlockSize;
                crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(this.KunciEnkrip));
                crypt.IV = IV;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream =
                       new CryptoStream(memoryStream, crypt.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(bytes, 0, bytes.Length);
                    }

                    result = Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {

                
            }
            return result;
        }

        public string Decrypt(string cypherteks)
        {
            string result = string.Empty;
            try
            {
                byte[] bytes = Convert.FromBase64String(cypherteks);
                SymmetricAlgorithm crypt = Aes.Create();
                HashAlgorithm hash = MD5.Create();
                crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(this.KunciEnkrip));
                crypt.IV = IV;

                using (MemoryStream memoryStream = new MemoryStream(bytes))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, crypt.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        byte[] decryptedBytes = new byte[bytes.Length];
                        cryptoStream.Read(decryptedBytes, 0, decryptedBytes.Length);
                        result = Encoding.Unicode.GetString(decryptedBytes);
                    }
                }
            }
            catch (Exception ex)
            {

                
            }
            return result;
        }

    }
}