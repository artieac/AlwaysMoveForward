
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace AlwaysMoveForward.Common.Encryption
{
    public class AESEncryptionManager
    {
        public AESEncryptionManager(string key, string salt)
        {
            this.Key = key;
            this.Salt = salt;
        }

        public string Key { get; private set; }
        public string Salt { get; private set; }

        public string Encrypt(string plainText)
        {
            return this.Encrypt(this.Key, this.Salt, plainText);
        }

        public string Encrypt(string encryptionKey, string encryptionSalt, string plainText)
        {
            string retVal = string.Empty;

            if (plainText != null && plainText != string.Empty)
            {
                // Declare the RijndaelManaged object
                // used to encrypt the data.
                RijndaelManaged aesAlg = null;

                try
                {
                    // Create a RijndaelManaged object
                    // with the specified key and IV.
                    aesAlg = new RijndaelManaged();

                    byte[] passwordSaltBytes = Encoding.UTF8.GetBytes(encryptionSalt);

                    PasswordDeriveBytes pdb = new PasswordDeriveBytes(encryptionKey, passwordSaltBytes, "SHA512", 129);
                    aesAlg.Key = pdb.GetBytes(32);
                    aesAlg.IV = pdb.GetBytes(16);

                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for encryption.
                    MemoryStream encryptionStream = new MemoryStream();

                    using (StreamWriter streamWriter = new StreamWriter(new CryptoStream(encryptionStream, encryptor, CryptoStreamMode.Write)))
                    {
                        // Write all data to the stream.
                        streamWriter.Write(plainText);
                    }

                    retVal = Convert.ToBase64String(encryptionStream.ToArray());
                }
                finally
                {
                    // Clear the RijndaelManaged object.
                    if (aesAlg != null)
                        aesAlg.Clear();
                }
            }

            return retVal;
        }

        public string Decrypt(string encryptedText)
        {
            return this.Decrypt(this.Key, this.Salt, encryptedText);
        }

        public string Decrypt(string encryptionKey, string encryptionSalt, string encryptedText)
        {
            string retVal = string.Empty;

            if (encryptedText != null && encryptedText != string.Empty)
            {
                // Declare the RijndaelManaged object
                // used to encrypt the data.
                RijndaelManaged aesAlg = null;

                try
                {
                    // Create a RijndaelManaged object
                    // with the specified key and IV.
                    aesAlg = new RijndaelManaged();

                    byte[] encryptedTextBytes = Convert.FromBase64String(encryptedText);
                    byte[] passwordSaltBytes = Encoding.UTF8.GetBytes(encryptionSalt);

                    PasswordDeriveBytes pdb = new PasswordDeriveBytes(encryptionKey, passwordSaltBytes, "SHA512", 129);
                    aesAlg.Key = pdb.GetBytes(32);
                    aesAlg.IV = pdb.GetBytes(16);

                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for encryption.
                    MemoryStream decryptedStream = new MemoryStream(encryptedTextBytes);

                    using (StreamReader streamReader = new StreamReader(new CryptoStream(decryptedStream, decryptor, CryptoStreamMode.Read)))
                    {
                        // Write all data to the stream.
                        retVal = streamReader.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    string error = e.Message;
                }
                finally
                {
                    // Clear the RijndaelManaged object.
                    if (aesAlg != null)
                        aesAlg.Clear();
                }
            }

            return retVal;
        }
    }
}
