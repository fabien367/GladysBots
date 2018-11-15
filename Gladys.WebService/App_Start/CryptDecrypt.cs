using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Gladys.WebService.App_Start
{
    public class CryptDecrypt
    {
        public string DataEncrypted(string data)
        {
            try
            {
                string dataEncrypted;
                // Create a new instance of the Aes
                // class.  This generates a new key and initialization 
                // vector (IV).
                byte[] key = Encoding.ASCII.GetBytes(TokenConstants.Key);
                byte[] iV= Encoding.ASCII.GetBytes(TokenConstants.IV);

                using (Aes myAes = Aes.Create())
                {
                    // Encrypt the string to an array of bytes.
                    byte[] encrypted = EncryptStringToBytes_Aes(data, key, iV);
                    dataEncrypted = Convert.ToBase64String(encrypted);
                    // Decrypt the bytes to a string.
                    //string roundtrip = DecryptStringFromBytes_Aes(encrypted, key, iV);
                }
                return dataEncrypted;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public string DataDencrypted(string data)
        {
            try
            {
                string dataDencrypted;
                // Create a new instance of the Aes
                // class.  This generates a new key and initialization 
                // vector (IV).
                byte[] key = Encoding.ASCII.GetBytes(TokenConstants.Key);
                byte[] iV = Encoding.ASCII.GetBytes(TokenConstants.IV);

                using (Aes myAes = Aes.Create())
                {
                    // Decrypt the bytes to a string.
                    dataDencrypted = DecryptStringFromBytes_Aes(System.Convert.FromBase64String(data), key, iV);
                }
                return dataDencrypted;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        private string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }
            return plaintext;
        }
    }
}