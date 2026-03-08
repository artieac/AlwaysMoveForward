namespace AlwaysMoveForward.Common.Encryption
{
    /// <summary>
    /// KeyFile Encryption Configuration
    /// </summary>
    public class KeyFileConfiguration
    {
        /// <summary>
        /// Default Section Constant
        /// </summary>
        public const string DEFAULT_SECTION = "AlwaysMoveForward:KeyFileEncryption";

        /// <summary>
        /// Default constructor
        /// </summary>
        public KeyFileConfiguration() { }

        /// <summary>
        /// Gets or sets the key file
        /// Define the salt used to modify the password.
        /// </summary>
        public string KeyFile { get; set; }

        /// <summary>
        /// Gets or sets the key file password
        /// Define the salt used to modify the password.
        /// </summary>
        public string KeyFilePassword { get; set; }
    }
}
