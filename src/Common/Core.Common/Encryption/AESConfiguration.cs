
namespace AlwaysMoveForward.Core.Common.Encryption
{
    /// <summary>
    /// AES Encryption Configuration
    /// </summary>
    public class AESConfiguration
    {
        private static AESConfiguration aesConfiguration;

        public static AESConfiguration GetInstance()
        {
            if(AESConfiguration.aesConfiguration==null)
            {
                aesConfiguration = new AESConfiguration();
            }

            return aesConfiguration;
        }
        /// <summary>
        /// Default Constructor
        /// </summary>
        public AESConfiguration() { }

        /// <summary>
        /// Gets or sets the encryption key
        /// </summary>
        public string EncryptionKey { get; set; }
        
        /// <summary>
        /// Gets or sets the salt
        /// Define the salt used to modify the password.
        /// </summary>
        public string Salt { get; set; }
    }
}