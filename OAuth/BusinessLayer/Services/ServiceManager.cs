using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VP.Digital.Common.DataLayer;
using VP.Digital.Common.Security;
using VP.Digital.Common.Utilities.Logging;
using VP.Digital.Security.OAuth.DataLayer;
using VP.Digital.Security.OAuth.DataLayer.Repositories;

namespace VP.Digital.Security.OAuth.BusinessLayer.Services
{
    /// <summary>
    /// The service manager for the OAuth services
    /// </summary>
    public class ServiceManager : IServiceManager
    {
        /// <summary>
        /// The constructor for the manager
        /// </summary>
        /// <param name="repositoryManager">The container for the repositories used by this classes services</param>
        public ServiceManager(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
        {
            this.RepositoryManager = repositoryManager;

            this.UnitOfWork = unitOfWork as UnitOfWork;
        }

        /// <summary>
        /// Gets the current Unit Of Work
        /// </summary>
        public UnitOfWork UnitOfWork { get; private set; }

        /// <summary>
        /// Gets the repository container
        /// </summary>
        private IRepositoryManager RepositoryManager { get; set; }

        /// <summary>
        /// The services containing the Consumer business rules
        /// </summary>
        private IConsumerService consumerService;

        /// <summary>
        /// Gets the current consumer service
        /// </summary>
        public IConsumerService ConsumerService
        {
            get
            {
                if (this.consumerService == null)
                {
                    this.consumerService = new ConsumerService(this.RepositoryManager.ConsumerRepository, this.RepositoryManager.ConsumerNonceRepository);
                }

                return this.consumerService;
            }
        }

        /// <summary>
        /// The service containing the Token business rules
        /// </summary>
        private ITokenService tokenService;

        /// <summary>
        /// Gets the current Token service
        /// </summary>
        public ITokenService TokenService
        {
            get
            {
                if (this.tokenService == null)
                {
                    this.tokenService = new TokenService(this.RepositoryManager.ConsumerRepository, this.RepositoryManager.RequestTokenRepository);
                }

                return this.tokenService;
            }
        }

        /// <summary>
        /// The service containing the Token business rules
        /// </summary>
        private IUserService userService;

        /// <summary>
        /// Gets the current Token service
        /// </summary>
        public IUserService UserService
        {
            get
            {
                if (this.userService == null)
                {
                    this.userService = new UserService(this.RepositoryManager.DigitalUserRepository, this.RepositoryManager.LoginAttemptRepository);
                }

                return this.userService;
            }
        }

        /// <summary>
        /// The service containing the Whitelist business rules
        /// </summary>
        private IWhiteListService whitelistService;

        /// <summary>
        /// Gets the current Token service
        /// </summary>
        public IWhiteListService WhiteListService
        {
            get
            {
                if (this.whitelistService == null)
                {
                    this.whitelistService = new WhiteListService();
                }

                return this.whitelistService;
            }
        }
    }
}
