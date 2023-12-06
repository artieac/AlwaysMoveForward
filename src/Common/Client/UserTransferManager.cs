using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PucksAndProgramming.Common.Encryption;

namespace PucksAndProgramming.OAuth.Client
{
    /// <summary>
    /// A class to facilitate encrypting and decrypting the user we will pass from the proxy to the endpoint
    /// </summary>
    public class UserTransferManager
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public UserTransferManager() : this(AESConfiguration.GetInstance())
        {

        }

        /// <summary>
        /// Constructor with specified configuration
        /// </summary>
        /// <param name="aesConfiguration">The AES configuration instance</param>
        public UserTransferManager(AESConfiguration aesConfiguration) : this(aesConfiguration.EncryptionKey, aesConfiguration.Salt)
        {

        }

        /// <summary>
        /// Constructor with key and salt passed in directly
        /// </summary>
        /// <param name="encryptionKey">The encryption key</param>
        /// <param name="encryptionSalt">The encryption salt</param>
        public UserTransferManager(string encryptionKey, string encryptionSalt)
        {
            this.EncryptionKey = encryptionKey;
            this.Salt = encryptionSalt;
        }

        /// <summary>
        /// Gets or sets the encryption key
        /// </summary>
        private string EncryptionKey { get; set;}

        /// <summary>
        /// Gets or sets the encryption salt
        /// </summary>
        private string Salt { get; set; }

        /// <summary>
        /// Encrypt a PucksAndProgramming User
        /// </summary>
        /// <param name="user">The current user</param>
        /// <returns>An encrypted string</returns>
        public string Encrypt(PucksAndProgramming.Common.DomainModel.User user)
        {
            string retVal = string.Empty;

            if (user != null)
            {
                AESManager encryptor = new AESManager(this.EncryptionKey, this.Salt);
                retVal = encryptor.Encrypt(PucksAndProgramming.Common.Utilities.SerializationUtilities.SerializeObjectToXmlString(user));
            }

            return retVal;
        }

        /// <summary>
        /// Decrypt to a PucksAndProgramming User
        /// </summary>
        /// <param name="encryptedString">The encrypted string</param>
        /// <returns>A PucksAndProgramming user instance</returns>
        public PucksAndProgramming.Common.DomainModel.User Decrypt(string encryptedString)
        {
            PucksAndProgramming.Common.DomainModel.User retVal = null;

            if (!string.IsNullOrEmpty(encryptedString))
            {
                AESManager encryptor = new AESManager(this.EncryptionKey, this.Salt);
                string decryptedValue = encryptor.Decrypt(encryptedString);
                retVal = PucksAndProgramming.Common.Utilities.SerializationUtilities.DeserializeXmlToObject<PucksAndProgramming.Common.DomainModel.User>(decryptedValue);
            }

            return retVal;
        }
    }
}
