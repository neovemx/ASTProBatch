using System;
using System.IO;
using System.Security.Cryptography;

namespace AST_ProBatch_Mobile.Security
{
    public class Crypto
    {
        public static string[] GetKeyAndIV()
        {
            AesCryptoServiceProvider myAes = new AesCryptoServiceProvider();
            byte[] Key = myAes.Key;
            byte[] IV = myAes.IV;
            string key = Convert.ToBase64String(Key);
            string iv = Convert.ToBase64String(IV);
            string[] KeyAndIV = new string[2];
            KeyAndIV[0] = key;
            KeyAndIV[1] = iv;
            return KeyAndIV;
        }

        public static string EncryptString(string plainText)
        {
            byte[] Key = Convert.FromBase64String("mTAAF/03GSh0CE9kQ79RAvM4efLv5AJwxkCekknUDW0=");
            byte[] IV = Convert.FromBase64String("B9BbpL8EV7+HzHvyO1IObw==");
            return Convert.ToBase64String(EncryptStringToBytes_Aes(plainText, Key, IV));
        }

        public static string DecodeString(string plainText)
        {
            byte[] Key = Convert.FromBase64String("mTAAF/03GSh0CE9kQ79RAvM4efLv5AJwxkCekknUDW0=");
            byte[] IV = Convert.FromBase64String("B9BbpL8EV7+HzHvyO1IObw==");
            return DecryptStringFromBytes_Aes(Convert.FromBase64String(plainText), Key, IV);
        }

        private static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;
        }

        private static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            string plaintext = null;
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }
            return plaintext;

        }
    }
}
