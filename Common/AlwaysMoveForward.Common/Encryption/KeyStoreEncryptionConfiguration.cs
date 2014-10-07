using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AlwaysMoveForward.Common.Encryption
{
    public class KeyStoreEncryptionConfiguration : ConfigurationSection
    {
        public const string DefaultSection = "Vistaprint/Digital/KeyStoreEncryptionConfiguration";

        public const string StoreNameSetting = "StoreName";
        public const string StoreLocationSetting = "StoreLocation";
        public const string CertificateNameSetting = "CertificateName";

        private static KeyStoreEncryptionConfiguration configurationInstance;

        public static KeyStoreEncryptionConfiguration GetInstance()
        {
            return KeyStoreEncryptionConfiguration.GetInstance(DefaultSection);
        }

        public static KeyStoreEncryptionConfiguration GetInstance(string configurationSection)
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
        [ConfigurationProperty(StoreNameSetting, IsRequired = false)]
        public string StoreName
        {
            get { return (string)this[StoreNameSetting]; }
            set { this[StoreNameSetting] = value; }
        }
        /// <summary>
        /// Define the salt used to modify the password.
        /// </summary>
        [ConfigurationProperty(StoreLocationSetting, IsRequired = false)]
        public string StoreLocation
        {
            get { return (string)this[StoreLocationSetting]; }
            set { this[StoreLocationSetting] = value; }
        }
        /// <summary>
        /// Define the salt used to modify the password.
        /// </summary>
        [ConfigurationProperty(CertificateNameSetting, IsRequired = false)]
        public string CertificateName
        {
            get { return (string)this[CertificateNameSetting]; }
            set { this[CertificateNameSetting] = value; }
        }
    }
}
