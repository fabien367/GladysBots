using Gladys.WebService.Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Galdys.WebServices.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //var credentials = new NetworkCredential("fabien", "test");
            //var handler = new HttpClientHandler { Credentials = credentials };
            //CryptDecrypt c = new CryptDecrypt();
            //var id = c.DataEncrypted("57e5ad63-5e01-4fdc-adae-a7ef9ecff759");
            //using (HttpClient client = new HttpClient(handler))
            //{
            //    var req = new HttpRequestMessage() { RequestUri = new Uri("http://localhost:55493/Outlook/GetAppointments") };                
            //    req.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //    req.Headers.Add("username", "fabien");
            //    //req.Headers.Add("Authorization", id);
            //    var byteArray =  Encoding.Default.GetBytes(id);
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", id);
            //    var reponse = Task.Run(async () => await client.SendAsync(req));
            //    reponse.Wait();
            //    //client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            //    //client.Headers.Add("Authorization", "3987345394580");
            //    //client.Headers.Add("username", "user");
            //    //client.Headers.Add("password", "pwd");
            //    //var token = client.UploadString("http://localhost:55493/Outlook/GetAppointments", "POST", "grant_type=username=fabien&password=test");
            //}
            //using (HttpClient client = new HttpClient())
            //{
            //    var req = new HttpRequestMessage() { RequestUri = new Uri("http://localhost:55493/Outlook/GetAppointments") };

            //    req.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //    req.Headers.Add("username", "user");
            //    var byteArray = new UTF8Encoding().GetBytes("<clientid>:<clientsecret>");
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            //    var reponse = Task.Run(async () => await client.SendAsync(req));
            //    reponse.Wait();
            //    //client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            //    //client.Headers.Add("Authorization", "3987345394580");
            //    //client.Headers.Add("username", "user");
            //    //client.Headers.Add("password", "pwd");
            //    //var token = client.UploadString("http://localhost:55493/Outlook/GetAppointments", "POST", "grant_type=username=fabien&password=test");
            //}
            ////GenerateToken(null, DateTime.Now, "test");
            ///
            DataTableService s = new DataTableService();
            s.GetData("TemplateTable");
        }
        private static string GenerateToken(string user, DateTime expires, string value)
        {
            string token = string.Empty;
            try
            {
                string original = value;

                // Create a new instance of the RijndaelManaged
                // class.  This generates a new key and initialization 
                // vector (IV).
                using (RijndaelManaged myRijndael = new RijndaelManaged())
                {

                    myRijndael.GenerateKey();
                    myRijndael.GenerateIV();
                    // Encrypt the string to an array of bytes.
                    byte[] encrypted = EncryptStringToBytes(original, myRijndael.Key, myRijndael.IV);
                    token = HttpServerUtility.UrlTokenEncode(encrypted);
                    var bytearray = HttpServerUtility.UrlTokenDecode(token);
                    // Decrypt the bytes to a string.
                    string roundtrip = DecryptStringFromBytes(bytearray, myRijndael.Key, myRijndael.IV);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return token;
        }

        private static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

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

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
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

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

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
