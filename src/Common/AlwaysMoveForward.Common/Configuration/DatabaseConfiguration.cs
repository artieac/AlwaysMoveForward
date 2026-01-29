using System.Configuration;

namespace AlwaysMoveForward.Common.Configuration
{
    /// <summary>
    /// A class to simplify getting the configuration settings for a database
    /// </summary>
    public class DatabaseConfiguration : AlwaysMoveForward.Common.Encryption.EncryptedConfigurationSection
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;

        /// <summary>
        /// Default constructor
        /// </summary>
        public DatabaseConfiguration()
        { }

        /// <summary>
        /// Get the connection string decrypted
        /// </summary>
        /// <returns>The decrypted connection string</returns>
        public string GetDecryptedConnectionString()
        {
            return this.DecryptString(this.ConnectionString);
        }

        /// <summary>
        /// Get the connection string decrypted
        /// </summary>
        /// <param name="encryptionKey">the key used for decryption</param>
        /// <param name="decryptionSalt">The salt used when encrypting</param>
        /// <returns>The decrypted connection string</returns>
        public string GetDecryptedConnectionString(string decryptionKey, string decryptionSalt)
        {
            return this.DecryptString(this.ConnectionString, decryptionKey, decryptionSalt);
        }

        /// <summary>
        /// Gets the database name decrytped
        /// </summary>
        /// <returns>The decrytped database name</returns>
        public string GetDecryptedDatabaseName()
        {
            return this.DecryptString(this.DatabaseName);
        }

        /// <summary>
        /// Gets the database name decrytped
        /// </summary>
        /// <param name="encryptionKey">the key used for decryption</param>
        /// <param name="decryptionSalt">The salt used when encrypting</param>
        /// <returns>The decrytped database name</returns>
        public string GetDecryptedDatabaseName(string decryptionKey, string decryptionSalt)
        {
            return this.DecryptString(this.DatabaseName, decryptionKey, decryptionSalt);
        }
    }
}
