using Microsoft.Extensions.Options;

namespace AlwaysMoveForward.Common.Encryption
{
    /// <summary>
    /// A class to simplify getting the configuration settings for a database
    /// </summary>
    public class EncryptionConfiguration
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

        private readonly AESConfiguration _aesConfiguration;
        private readonly KeyFileConfiguration _keyFileConfiguration;
        private readonly KeyStoreConfiguration _keyStoreConfiguration;
        private readonly RSAXmlKeyFileConfiguration _rsaXmlKeyFileConfiguration;

        public EncryptionMethodOptions EncryptionMethod { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public EncryptionConfiguration()
        { }

        /// <summary>
        /// Constructor with dependency injection
        /// </summary>
        public EncryptionConfiguration(
            IOptions<AESConfiguration> aesConfiguration,
            IOptions<KeyFileConfiguration> keyFileConfiguration,
            IOptions<KeyStoreConfiguration> keyStoreConfiguration,
            IOptions<RSAXmlKeyFileConfiguration> rsaXmlKeyFileConfiguration)
        {
            _aesConfiguration = aesConfiguration?.Value;
            _keyFileConfiguration = keyFileConfiguration?.Value;
            _keyStoreConfiguration = keyStoreConfiguration?.Value;
            _rsaXmlKeyFileConfiguration = rsaXmlKeyFileConfiguration?.Value;
        }

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
                        if (_aesConfiguration != null)
                        {
                            AESManager aesencryption = new AESManager(_aesConfiguration.EncryptionKey, _aesConfiguration.Salt);
                            retVal = aesencryption.Decrypt(encryptedString);
                        }
                        break;
                    case EncryptionMethodOptions.CertificateKeyFile:
                        if (_keyFileConfiguration != null)
                        {
                            X509CertificateManager keyfileEncryption = new X509CertificateManager(_keyFileConfiguration.KeyFile, _keyFileConfiguration.KeyFilePassword);
                            retVal = keyfileEncryption.Decrypt(encryptedString);
                        }
                        break;
                    case EncryptionMethodOptions.CertificateKeyStore:
                        if (_keyStoreConfiguration != null)
                        {
                            X509CertificateManager keystoreEncryption = new X509CertificateManager(_keyStoreConfiguration.StoreName, _keyStoreConfiguration.StoreLocation, _keyStoreConfiguration.CertificateName);
                            retVal = keystoreEncryption.Decrypt(encryptedString);
                        }
                        break;
                    case EncryptionMethodOptions.RSAXmlKeyFile:
                        if (_rsaXmlKeyFileConfiguration != null)
                        {
                            RSAXmlKeyFileManager rsaxmlKeyFileEncryption = new RSAXmlKeyFileManager(_rsaXmlKeyFileConfiguration.PublicKeyFile, _rsaXmlKeyFileConfiguration.PrivateKeyFile);
                            retVal = rsaxmlKeyFileEncryption.Decrypt(encryptedString);
                        }
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