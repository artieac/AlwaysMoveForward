using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.Configuration;
using AlwaysMoveForward.OAuth.DataLayer;
using AlwaysMoveForward.OAuth.DataLayer.Repositories;

namespace AlwaysMoveForward.OAuth.BusinessLayer.Services
{
    /// <summary>
    /// Builds domain data service manager with default repository
    /// </summary>
    public class ServiceManagerBuilder
    {
        /// <summary>
        /// Creates a domain data service manager with the default repository configuration
        /// </summary>
        /// <returns>Service manager</returns>
        public static IServiceManager CreateServiceManager()
        {
            DatabaseConfiguration config = DatabaseConfiguration.GetInstance(DatabaseConfiguration.DEFAULT_SECTION);
            return CreateServiceManager(config);
        }

        /// <summary>
        /// Creates a domain data service manager with specified repository connection
        /// </summary>
        /// <param name="databaseConfigurationSection">Repository connection string</param>
        /// <returns>Service manager</returns>
        public static IServiceManager CreateServiceManager(DatabaseConfiguration databaseConfigurationSection)
        {
            IServiceManager retVal = null;

            if (databaseConfigurationSection != null)
            {
                retVal = CreateServiceManager(databaseConfigurationSection.GetDecryptedConnectionString());
            }

            return retVal;
        }

        /// <summary>
        /// Creates a domain data service manager with specified configuration
        /// </summary>
        /// <param name="connectionString">Repository connection string</param>
        /// <param name="databaseName">Database name</param>
        /// <returns>Service manager</returns>
        public static IServiceManager CreateServiceManager(string connectionString)
        {
            ServiceManagerBuilder builder = new ServiceManagerBuilder();
            return builder.Create(connectionString);
        }

        /// <summary>
        /// Creates a domain data service manager with the specified configuration
        /// </summary>
        /// <param name="connectionString">Repository connection string</param>
        /// <param name="databaseName">Database name</param>
        /// <returns>Service manager</returns>
        public IServiceManager Create(string connectionString)
        {
            IUnitOfWork unitOfWork = this.CreateNHUnitOfWork(connectionString);
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
        public virtual IUnitOfWork CreateNHUnitOfWork(string connectionString)
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
