using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Aes_Example
{
    public static class StringEncrypt
    {
        public static string AesEncryptBase64(string sourceStr, string cryptoKey)
        {
            string encrypt = "";
            try
            {
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
                byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(cryptoKey));
                byte[] iv = md5.ComputeHash(Encoding.UTF8.GetBytes(cryptoKey));
                aes.Key = key;
                aes.IV = iv;

                byte[] dataByteArray = Encoding.UTF8.GetBytes(sourceStr);
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    encrypt = Convert.ToBase64String(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return encrypt;
        }

        public static string AesDecryptBase64(string sourceStr, string cryptoKey)
        {
            string decrypt = "";
            try
            {
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
                byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(cryptoKey));
                byte[] iv = md5.ComputeHash(Encoding.UTF8.GetBytes(cryptoKey));
                aes.Key = key;
                aes.IV = iv;

                byte[] dataByteArray = Convert.FromBase64String(sourceStr);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(dataByteArray, 0, dataByteArray.Length);
                        cs.FlushFinalBlock();
                        decrypt = Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return decrypt;
        }

    }




    class AesExample
    {
        public static void Main()
        {
            string original = "T123144941 ";
            Console.WriteLine(original);
            var Encrypted=StringEncrypt.AesEncryptBase64(original, "Aa123456");
            Console.WriteLine(Encrypted);
            var Decrypted = StringEncrypt.AesDecryptBase64(Encrypted, "Aa123456");
            Console.WriteLine(Decrypted);
            Console.ReadLine();
        }
        
    }
}