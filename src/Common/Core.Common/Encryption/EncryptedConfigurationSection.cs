
namespace AlwaysMoveForward.Core.Common.Encryption
{
    /// <summary>
    /// A class to simplify getting the configuration settings for a database
    /// </summary>
    public class EncryptedConfigurationSection 
    {        
        /// <summary>
        /// Possible options for the Encryption method
        /// </summary>
        public enum EncryptionMethodOptions
        {
            /// <summary>
            /// There is no encryption
            /// </summary>
            None,

            /// <summary>
            /// The values were encrypted with a certificate stored in a key file
            /// </summary>
            CertificateKeyFile,

            /// <summary>
            /// The values were encrypted with a certificate stored in the key store
            /// </summary>
            CertificateKeyStore,

            /// <summary>
            /// The values were encrypted with AES
            /// </summary>
            AES,

            /// <summary>
            /// The values were encrytped using RSA with the key valies in an xml file
            /// </summary>
            RSAXmlKeyFile,

            /// <summary>
            /// Uses internal settings for encryption
            /// </summary>
            Internal
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public EncryptedConfigurationSection()
        { }

        /// <summary>
        /// Gets or sets a value indicating whether or not it's encrypted
        /// </summary>
        public EncryptionMethodOptions EncryptionMethod { get; set; }

        /// <summary>
        /// Gets the configuration section that defines the encryption parameters
        /// </summary>
        public string EncryptionSetting { get; set; }
 
        /// <summary>
        /// This method decrypts a string using the configuration settings supplied
        /// </summary>
        /// <param name="encryptedString">The encrypted string</param>
        /// <returns>The passed in string, decrypted</returns>
        public string DecryptString(string encryptedString)
        {
            string retVal = string.Empty;

            if (!string.IsNullOrEmpty(encryptedString))
            {
                switch(this.EncryptionMethod)
                {
                    case EncryptionMethodOptions.None:
                        retVal = encryptedString;
                        break;
                    case EncryptionMethodOptions.AES:
                        AESConfiguration aesconfiguration = AESConfiguration.GetInstance();
                        AESManager aesencryption = new AESManager(aesconfiguration.EncryptionKey, aesconfiguration.Salt);
                        retVal = aesencryption.Decrypt(encryptedString);
                        break;
                    case EncryptionMethodOptions.CertificateKeyFile:
                        KeyFileConfiguration keyfileConfiguration = KeyFileConfiguration.GetInstance();
                        X509CertificateManager keyfileEncryption = new X509CertificateManager(keyfileConfiguration.KeyFile, keyfileConfiguration.KeyFilePassword);
                        retVal = keyfileEncryption.Decrypt(encryptedString);
                        break;
                    case EncryptionMethodOptions.CertificateKeyStore:
                        KeyStoreConfiguration keystoreConfiguration = KeyStoreConfiguration.GetInstance();
                        X509CertificateManager keystoreEncryption = new X509CertificateManager(keystoreConfiguration.StoreName, keystoreConfiguration.StoreLocation, keystoreConfiguration.CertificateName);
                        retVal = keystoreEncryption.Decrypt(encryptedString);
                        break;
                    case EncryptionMethodOptions.RSAXmlKeyFile:
                        RSAXmlKeyFileConfiguration rsaxmlKeyFileConfiguration = RSAXmlKeyFileConfiguration.GetInstance();
                        RSAXmlKeyFileManager rsaxmlKeyFileEncryption = new RSAXmlKeyFileManager(rsaxmlKeyFileConfiguration.PublicKeyFile, rsaxmlKeyFileConfiguration.PrivateKeyFile);
                        retVal = rsaxmlKeyFileEncryption.Decrypt(encryptedString);
                        break;
                }
            }

            return retVal;
        }

        public string DecryptString(string encryptedString, string decryptionKey, string decryptionSalt)
        {
            string retVal = string.Empty;

            if (!string.IsNullOrEmpty(encryptedString))
            {
                AESManager internalManager = new AESManager(decryptionKey, decryptionSalt);
                retVal = internalManager.Decrypt(encryptedString);
            }

            return retVal;
        }
    }
}