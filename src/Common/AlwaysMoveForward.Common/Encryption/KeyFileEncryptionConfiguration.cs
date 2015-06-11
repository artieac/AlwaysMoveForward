using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AlwaysMoveForward.Common.Encryption
{
    public class KeyFileEncryptionConfiguration : ConfigurationSection
    {
        public const string DefaultSection = "AlwaysMoveForward/KeyFileEncryptionConfiguration";

        public const string EncryptionKeyFileSetting = "KeyFile";
        public const string KeyFilePasswordSetting = "KeyFilePassword";

        private static KeyFileEncryptionConfiguration configurationInstance;

        public static KeyFileEncryptionConfiguration GetInstance()
        {
            return KeyFileEncryptionConfiguration.GetInstance(DefaultSection);
        }

        public static KeyFileEncryptionConfiguration GetInstance(string configurationSection)
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
        [ConfigurationProperty(EncryptionKeyFileSetting, IsRequired = false)]
        public string KeyFile
        {
            get { return (string)this[EncryptionKeyFileSetting]; }
            set { this[EncryptionKeyFileSetting] = value; }
        }
        /// <summary>
        /// Define the salt used to modify the password.
        /// </summary>
        [ConfigurationProperty(KeyFilePasswordSetting, IsRequired = false)]
        public string KeyFilePassword
        {
            get { return (string)this[KeyFilePasswordSetting]; }
            set { this[KeyFilePasswordSetting] = value; }
        }
    }
}
