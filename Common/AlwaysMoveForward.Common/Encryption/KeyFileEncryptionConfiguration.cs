using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AlwaysMoveForward.Common.Encryption
{
    public class KeyFileEncryptionConfiguration : ConfigurationSection
    {
        public const string k_DefaultSection = "Vistaprint/Digital/KeyFileEncryptionConfiguration";

        public const string k_EncryptionKeyFile = "KeyFile";
        public const string k_KeyFilePassword = "KeyFilePassword";

        static KeyFileEncryptionConfiguration configurationInstance;

        public static KeyFileEncryptionConfiguration GetInstance()
        {
            return KeyFileEncryptionConfiguration.GetInstance(k_DefaultSection);
        }

        public static KeyFileEncryptionConfiguration GetInstance(String configurationSection)
        {
            if (configurationInstance == null)
            {
                configurationInstance = (KeyFileEncryptionConfiguration)System.Configuration.ConfigurationManager.GetSection(configurationSection);
            }

            return configurationInstance;
        }

        public KeyFileEncryptionConfiguration() { }
        /// <summary>
        /// Define the salt used to modify the password.
        /// </summary>
        [ConfigurationProperty(k_EncryptionKeyFile, IsRequired = false)]
        public string KeyFile
        {
            get { return (string)this[k_EncryptionKeyFile]; }
            set { this[k_EncryptionKeyFile] = value; }
        }
        /// <summary>
        /// Define the salt used to modify the password.
        /// </summary>
        [ConfigurationProperty(k_KeyFilePassword, IsRequired = false)]
        public string KeyFilePassword
        {
            get { return (string)this[k_KeyFilePassword]; }
            set { this[k_KeyFilePassword] = value; }
        }
    }
}
