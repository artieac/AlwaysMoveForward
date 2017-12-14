using System;

namespace AlwaysMoveForward.Core.Common.Configuration
{
    /// <summary>
    /// A class to simplify getting the configuration settings for a database
    /// </summary>
    public class DatabaseConfiguration : AlwaysMoveForward.Core.Common.Encryption.EncryptedConfigurationSection
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DatabaseConfiguration()
        { }

        private string connectionString;

        /// <summary>
        /// Gets or sets the connection string found in the config file.
        /// </summary>
        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(this.connectionString))
                {
                    if (!string.IsNullOrEmpty(this.ConnectionStringEnvironmentSetting))
                    {
                        this.connectionString = Environment.GetEnvironmentVariable(this.ConnectionStringEnvironmentSetting);
                    }
                }

                return this.connectionString;
            }
            set
            {
                this.connectionString = value;
            }
        }
        
        public string ConnectionStringEnvironmentSetting { get; set; }

        private string databaseName;

        /// <summary>
        /// Gets or sets database name found in the config file
        /// </summary>
        public string DatabaseName
        {
            get
            {
                if (string.IsNullOrEmpty(this.databaseName))
                {
                    if (!string.IsNullOrEmpty(this.DatabaseNameEnvironmentSetting))
                    {
                        this.databaseName = Environment.GetEnvironmentVariable(this.DatabaseNameEnvironmentSetting);
                    }
                }

                return this.databaseName;
            }
            set
            {
                this.databaseName = value;
            }
        }

        public string DatabaseNameEnvironmentSetting { get; set; }

       
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
