namespace AlwaysMoveForward.Common.Encryption
{
    /// <summary>
    /// AES Encryption Configuration
    /// </summary>
    public class AESConfiguration
    {
        /// <summary>
        /// Default Section Constant
        /// </summary>
        public const string DEFAULT_SECTION = "AlwaysMoveForward:AESEncryption";
        
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