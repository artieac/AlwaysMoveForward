using AlwaysMoveForward.Core.Common.Configuration;
using AlwaysMoveForward.Core.Common.DataLayer;
using AlwaysMoveForward.Core.Common.DataLayer.Dapper;
using AlwaysMoveForward.OAuth2.Common.Factories;
using AlwaysMoveForward.OAuth2.DataLayer;
using AlwaysMoveForward.OAuth2.DataLayer.Repositories;
using Microsoft.Extensions.Options;

namespace AlwaysMoveForward.OAuth2.BusinessLayer.Services
{
    /// <summary>
    /// Builds domain data service manager with default repository
    /// </summary>
    public class ServiceManagerBuilder
    {
        /// <summary>
        /// A default encryption key value
        /// </summary>
        private const string DefaultEncryptionKey = "4ADDEBFF7C3D4F6FA455D1D1285387EC53D29CCDCFED4C56ADD65EB24F3D1C68D4C4D4683EA3436880DFBEF684F5DC51F26875A89AAD49DCB74B1DDFD6A7AF53";

        /// <summary>
        /// A default salt value
        /// </summary>
        private const string DefaultSalt = "36E336FABA034E47B6CEEF9BEF1E0D57";

        public ServiceManagerBuilder(IOptions<DatabaseConfiguration> databaseConfigurationSection)
        {
            this.DatabaseConfiguration = databaseConfigurationSection.Value;
        }

        private DatabaseConfiguration DatabaseConfiguration { get; set; }

        public IServiceManager Create()
        { 
            IServiceManager retVal = null;

            if (this.DatabaseConfiguration != null)
            {
                string connectionString = string.Empty;

                if (this.DatabaseConfiguration.EncryptionMethod == AlwaysMoveForward.Core.Common.Encryption.EncryptedConfigurationSection.EncryptionMethodOptions.Internal)
                {
                    connectionString = this.DatabaseConfiguration.GetDecryptedConnectionString(DefaultEncryptionKey, DefaultSalt);
                }
                else
                {
                    connectionString = this.DatabaseConfiguration.GetDecryptedConnectionString();
                }

                retVal = this.Create(connectionString);
            }

            return retVal;
        }

        /// <summary>
        /// Creates a domain data service manager with the specified configuration
        /// </summary>
        /// <param name="connectionString">Repository connection string</param>
        /// <param name="databaseName">Database name</param>
        /// <returns>Service manager</returns>
        public IServiceManager Create(string connectionString)
        {
            IUnitOfWork unitOfWork = this.CreateUnitOfWork(connectionString);
            IRepositoryManager repositoryManager = this.CreateRepositoryManager(unitOfWork);
            return this.CreateServiceManager(unitOfWork, repositoryManager);
        }

        /// <summary>
        /// Creates a domain data service manager with the specified configuration
        /// </summary>
        /// <param name="repositoryManager">Service manager repository manager</param>
        /// <returns>Service manager</returns>
        public virtual IServiceManager CreateServiceManager(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
        {
            return new ServiceManager(unitOfWork, repositoryManager);
        }

        /// <summary>
        /// Creates domain data unit of work with the given configuration
        /// </summary>
        /// <param name="connectionString">Repository connection string</param>
        /// <param name="databaseName">Database name</param>
        /// <returns>Domain data Mongo unit of work</returns>
        public virtual IUnitOfWork CreateUnitOfWork(string connectionString)
        {
            return new UnitOfWork(connectionString);
        }

        /// <summary>
        /// Creates domain data repository manager with a given unit of work
        /// </summary>
        /// <param name="unitOfWork">Mongo unit of work</param>
        /// <returns>Domain data repository manager</returns>
        public virtual IRepositoryManager CreateRepositoryManager(IUnitOfWork nhunitOfWork)
        {
            return new RepositoryManager(nhunitOfWork as UnitOfWork);
        }
    }
}
