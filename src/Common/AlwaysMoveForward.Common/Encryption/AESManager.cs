using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using AlwaysMoveForward.Common.Utilities;

namespace AlwaysMoveForward.Common.Encryption
{
    /// <summary>
    /// AES Encryption Manager
    /// </summary>
    public class AESManager
    {

        /// <summary>
        /// The byte size of the salt
        /// </summary>
        public const int SaltByteSize = 24;

        /// <summary>
        /// Default value for number of times to iterate when generating a key.
        /// </summary>
        public const int KEY_GENERATION_ITERATION_COUNT = 129;

        /// <summary>
        /// The number of bytes to use for the algorithm key
        /// </summary>
        public const int AlgorithmKeyBytes = 32;

        /// <summary>
        /// The number of bytes to use for the Initialization Vector
        /// </summary>
        private const int AlgorithmInitializationVectorBytes = 16;

        /// <summary>
        /// AES Encryption Manager constructor
        /// </summary>
        /// <param name="key">the key used to encrypt/decrypt</param>
        /// <param name="salt">the salt used to encrypt/decrypt</param>
        public AESManager(string key, string salt) : this(KEY_GENERATION_ITERATION_COUNT, key, salt) { }

        /// <summary>
        /// AES Encryption Manager constructor
        /// </summary>
        /// <param name="keyGenerationIterationCount">How many times to iterate when generating the key</param>
        /// <param name="key">the key used to encrypt/decrypt</param>
        /// <param name="salt">the salt used to encrypt/decrypt</param>
        public AESManager(int keyGenerationIterationCount, string key, string salt)
        {
            this.Key = key;
            this.Salt = salt;
            this.KeyGenerationIterationCount = keyGenerationIterationCount;
        }

        /// <summary>
        /// Gets how many times to iterate when generating the key.
        /// </summary>
        public int KeyGenerationIterationCount { get; private set; }

        /// <summary>
        /// Gets the encryption Key
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets the encryption Salt
        /// </summary>
        public string Salt { get; private set; }

        /// <summary>
        /// Derives key and IV bytes using PBKDF2
        /// </summary>
        private (byte[] key, byte[] iv) DeriveKeyAndIV(string encryptionKey, string encryptionSalt)
        {
            byte[] passwordSaltBytes = Encoding.UTF8.GetBytes(encryptionSalt);

            using (var pdb = new Rfc2898DeriveBytes(encryptionKey, passwordSaltBytes, this.KeyGenerationIterationCount, HashAlgorithmName.SHA512))
            {
                byte[] key = pdb.GetBytes(AlgorithmKeyBytes);
                byte[] iv = pdb.GetBytes(AlgorithmInitializationVectorBytes);
                return (key, iv);
            }
        }

        /// <summary>
        /// Encrypt some plaintext
        /// </summary>
        /// <param name="plainText">The unencrypted plaintext</param>
        /// <returns>An encrypted string</returns>
        public string Encrypt(string plainText)
        {
            return this.Encrypt(this.Key, this.Salt, plainText);
        }

        /// <summary>
        /// Encrypt some plaintext given a key, a salt and some plaintext
        /// </summary>
        /// <param name="encryptionKey">an encryption key</param>
        /// <param name="encryptionSalt">an encryption salt</param>
        /// <param name="plainText">The unencrypted plaintext</param>
        /// <returns>An encrypted string</returns>
        private string Encrypt(string encryptionKey, string encryptionSalt, string plainText)
        {
            string retVal = string.Empty;

            if (!string.IsNullOrEmpty(plainText))
            {
                using (Aes aesAlg = Aes.Create())
                {
                    try
                    {
                        var (key, iv) = DeriveKeyAndIV(encryptionKey, encryptionSalt);
                        aesAlg.Key = key;
                        aesAlg.IV = iv;

                        // Create an encryptor to perform the stream transform.
                        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                        // Create the streams used for encryption.
                        MemoryStream memStreamEncrypt = new MemoryStream();

                        using (StreamWriter streamWriterEncrypt = new StreamWriter(new CryptoStream(memStreamEncrypt, encryptor, CryptoStreamMode.Write)))
                        {
                            // Write all data to the stream.
                            streamWriterEncrypt.Write(plainText);
                        }

                        retVal = Convert.ToBase64String(memStreamEncrypt.ToArray());
                    }
                    catch (Exception e)
                    {
                        LogManager.GetLogger().Error(e);
                    }
                }
            }

            return retVal;
        }

        /// <summary>
        /// Decrypt some encrypted text
        /// </summary>
        /// <param name="encryptedText">Encrypted string</param>
        /// <returns>Decrypted string</returns>
        public string Decrypt(string encryptedText)
        {
            return this.Decrypt(this.Key, this.Salt, encryptedText);
        }

        /// <summary>
        /// Decrypt some encrypted text with a key and salt
        /// </summary>
        /// <param name="encryptionKey">Encryption Key</param>
        /// <param name="encryptionSalt">Encryption Salt</param>
        /// <param name="encryptedText">Encrypted string</param>
        /// <returns>Decrypted string</returns>
        private string Decrypt(string encryptionKey, string encryptionSalt, string encryptedText)
        {
            string retVal = string.Empty;

            if (!string.IsNullOrEmpty(encryptedText))
            {
                using (Aes aesAlg = Aes.Create())
                {
                    try
                    {
                        byte[] encryptedTextBytes = Convert.FromBase64String(encryptedText);

                        var (key, iv) = DeriveKeyAndIV(encryptionKey, encryptionSalt);
                        aesAlg.Key = key;
                        aesAlg.IV = iv;

                        // Create a decryptor to perform the stream transform.
                        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                        // Create the streams used for decryption.
                        MemoryStream memoryStreamDecrypt = new MemoryStream(encryptedTextBytes);

                        using (StreamReader streamReaderDecrypt = new StreamReader(new CryptoStream(memoryStreamDecrypt, decryptor, CryptoStreamMode.Read)))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            retVal = streamReaderDecrypt.ReadToEnd();
                        }
                    }
                    catch (Exception e)
                    {
                        LogManager.GetLogger().Error(e);
                    }
                }
            }

            return retVal;
        }
    }
}
