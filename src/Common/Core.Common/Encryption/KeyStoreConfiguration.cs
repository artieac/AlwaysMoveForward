namespace AlwaysMoveForward.Core.Common.Encryption
{
    /// <summary>
    /// Key store Encryption Configuration
    /// </summary>
    public class KeyStoreConfiguration
    {
        private static KeyStoreConfiguration keyStoreConfiguration;

        public static KeyStoreConfiguration GetInstance()
        {
            if (KeyStoreConfiguration.keyStoreConfiguration == null)
            {
                keyStoreConfiguration = new KeyStoreConfiguration();
            }

            return keyStoreConfiguration;
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public KeyStoreConfiguration() { }

        /// <summary>
        /// Gets or sets the store name
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// Gets or sets the store location 
        /// </summary>
        public string StoreLocation { get; set; }

        /// <summary>
        /// Gets or sets the certificate name
        /// </summary>
        public string CertificateName { get; set; }
    }
}
