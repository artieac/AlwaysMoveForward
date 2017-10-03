using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth2.Common.Encryption
{
    public class KeyFileEncryptionConfiguration
    {
        private static KeyFileEncryptionConfiguration configurationInstance;

        public static KeyFileEncryptionConfiguration GetInstance()
        {
            if (configurationInstance == null)
            {
                configurationInstance = new KeyFileEncryptionConfiguration();
            }

            return configurationInstance;
        }

        public KeyFileEncryptionConfiguration() { }
        /// <summary>
        /// Define the salt used to modify the password.
        /// </summary>
        public string KeyFile { get; set; }

        /// <summary>
        /// Define the salt used to modify the password.
        /// </summary>
        public string KeyFilePassword { get; set; }
    }
}
