using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AlwaysMoveForward.Common.Encryption
{
    public class KeyStoreEncryptionConfiguration : ConfigurationSection
    {
        public const string k_DefaultSection = "Vistaprint/Digital/KeyStoreEncryptionConfiguration";

        public const string k_StoreName = "StoreName";
        public const string k_StoreLocation = "StoreLocation";
        public const string k_CertificateName = "CertificateName";

        static KeyStoreEncryptionConfiguration configurationInstance;

        public static KeyStoreEncryptionConfiguration GetInstance()
        {
            return KeyStoreEncryptionConfiguration.GetInstance(k_DefaultSection);
        }

        public static KeyStoreEncryptionConfiguration GetInstance(String configurationSection)
        {
            if (configurationInstance == null)
            {
                configurationInstance = (KeyStoreEncryptionConfiguration)System.Configuration.ConfigurationManager.GetSection(configurationSection);
            }

            return configurationInstance;
        }

        public KeyStoreEncryptionConfiguration() { }
        /// <summary>
        /// Define password to use to encrypt the data
        /// </summary>
        [ConfigurationProperty(k_StoreName, IsRequired = false)]
        public string StoreName
        {
            get { return (string)this[k_StoreName]; }
            set { this[k_StoreName] = value; }
        }
        /// <summary>
        /// Define the salt used to modify the password.
        /// </summary>
        [ConfigurationProperty(k_StoreLocation, IsRequired = false)]
        public string StoreLocation
        {
            get { return (string)this[k_StoreLocation]; }
            set { this[k_StoreLocation] = value; }
        }
        /// <summary>
        /// Define the salt used to modify the password.
        /// </summary>
        [ConfigurationProperty(k_CertificateName, IsRequired = false)]
        public string CertificateName
        {
            get { return (string)this[k_CertificateName]; }
            set { this[k_CertificateName] = value; }
        }
    }
}
