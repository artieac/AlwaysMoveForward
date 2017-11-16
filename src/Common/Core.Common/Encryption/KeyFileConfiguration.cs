namespace AlwaysMoveForward.Core.Common.Encryption
{
    /// <summary>
    /// KeyFile Encryption Configuration
    /// </summary>
    public class KeyFileConfiguration
    {
        private static KeyFileConfiguration keyFileConfiguration;

        public static KeyFileConfiguration GetInstance()
        {
            if (KeyFileConfiguration.keyFileConfiguration == null)
            {
                keyFileConfiguration = new KeyFileConfiguration();
            }

            return keyFileConfiguration;
        }

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
