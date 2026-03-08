namespace AlwaysMoveForward.Common.Encryption
{
    /// <summary>
    /// RSA Xml Key File Encryption Configuration
    /// </summary>
    public class RSAXmlKeyFileConfiguration
    {
        /// <summary>
        /// Default Section Constant
        /// </summary>
        public const string DEFAULT_SECTION = "AlwaysMoveForward:RSAXmlKeyFile";

        /// <summary>
        /// Default Constructor
        /// </summary>
        public RSAXmlKeyFileConfiguration() { }

        /// <summary>
        /// Gets or sets the public key file path
        /// </summary>
        public string PublicKeyFile { get; set; }

        /// <summary>
        /// Gets or sets the private key file path
        /// </summary>
        public string PrivateKeyFile { get; set; }
    }
}