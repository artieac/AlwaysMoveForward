using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AlwaysMoveForward.Common.Encryption
{
    public class AESEncryptionConfiguration : ConfigurationSection
    {
        public const string DefaultSection = "Vistaprint/Digital/AESEncryptionConfiguration";

        public const string EncryptionKeySetting = "EncryptionKey";
        public const string SaltSetting = "Salt";

        private static AESEncryptionConfiguration configurationInstance;

        public static AESEncryptionConfiguration GetInstance()
        {
            return AESEncryptionConfiguration.GetInstance(DefaultSection);
        }

        public static AESEncryptionConfiguration GetInstance(string configurationSection)
        {
            if (configurationInstance == null)
            {
                configurationInstance = (AESEncryptionConfiguration)System.Configuration.ConfigurationManager.GetSection(configurationSection);
            }

            return configurationInstance;
        }      
        
        public AESEncryptionConfiguration() { }
        /// <summary>
        /// Define the salt used to modify the password.
        /// </summary>
        [ConfigurationProperty(EncryptionKeySetting, IsRequired = false)]
        public string EncryptionKey
        {
            get { return (string)this[EncryptionKeySetting]; }
            set { this[EncryptionKeySetting] = value; }
        }
        /// <summary>
        /// Define the salt used to modify the password.
        /// </summary>
        [ConfigurationProperty(SaltSetting, IsRequired = false)]
        public string Salt
        {
            get { return (string)this[SaltSetting]; }
            set { this[SaltSetting] = value; }
        }
    }
}