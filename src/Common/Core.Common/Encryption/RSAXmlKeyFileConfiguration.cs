namespace AlwaysMoveForward.Core.Common.Encryption
{
    /// <summary>
    /// AES Encryption Configuration
    /// </summary>
    public class RSAXmlKeyFileConfiguration
    {
        private static RSAXmlKeyFileConfiguration rsaXmlKeyFileConfiguration;

        public static RSAXmlKeyFileConfiguration GetInstance()
        {
            if (RSAXmlKeyFileConfiguration.rsaXmlKeyFileConfiguration == null)
            {
                rsaXmlKeyFileConfiguration = new RSAXmlKeyFileConfiguration();
            }

            return rsaXmlKeyFileConfiguration;
        }

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