
using System;

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

        private string encryptionKey;

        /// <summary>
        /// Gets or sets the encryption key
        /// </summary>
        public string EncryptionKey
        {
            get
            {
                if (string.IsNullOrEmpty(this.encryptionKey))
                {
                    if (!string.IsNullOrEmpty(this.KeyEnvironmentSetting))
                    {
                        this.encryptionKey = Environment.GetEnvironmentVariable(this.KeyEnvironmentSetting);
                    }
                }

                return this.encryptionKey;
            }
            set
            {
                this.encryptionKey = value;
            }
        }

        private string salt;
        /// <summary>
        /// Gets or sets the salt
        /// Define the salt used to modify the password.
        /// </summary>
        public string Salt
        {
            get
            {
                if (string.IsNullOrEmpty(this.salt))
                {
                    if (!string.IsNullOrEmpty(this.SaltEnvironmentSetting))
                    {
                        this.salt = Environment.GetEnvironmentVariable(this.SaltEnvironmentSetting);
                    }
                }

                return this.salt;
            }
            set
            {
                this.salt = value;
            }
        }

        public string KeyEnvironmentSetting { get; set; }

        public string SaltEnvironmentSetting { get; set; }
    }
}