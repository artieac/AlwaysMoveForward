using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AlwaysMoveForward.Common.Encryption
{
    public class AESEncryptionConfiguration : ConfigurationSection
    {
        public const string k_DefaultSection = "Vistaprint/Digital/AESEncryptionConfiguration";

        public const string k_EncryptionKey = "EncryptionKey";
        public const string k_Salt = "Salt";

        static AESEncryptionConfiguration configurationInstance;

        public static AESEncryptionConfiguration GetInstance()
        {
            return AESEncryptionConfiguration.GetInstance(k_DefaultSection);
        }

        public static AESEncryptionConfiguration GetInstance(String configurationSection)
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
        [ConfigurationProperty(k_EncryptionKey, IsRequired = false)]
        public string EncryptionKey
        {
            get { return (string)this[k_EncryptionKey]; }
            set { this[k_EncryptionKey] = value; }
        }
        /// <summary>
        /// Define the salt used to modify the password.
        /// </summary>
        [ConfigurationProperty(k_Salt, IsRequired = false)]
        public string Salt
        {
            get { return (string)this[k_Salt]; }
            set { this[k_Salt] = value; }
        }
    }
}