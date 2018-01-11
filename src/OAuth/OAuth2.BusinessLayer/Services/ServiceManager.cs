using System;
using System.Collections.Generic;
using System.Linq;
using AlwaysMoveForward.OAuth2.Common.Security;
using AlwaysMoveForward.OAuth2.Common.Factories;
using AlwaysMoveForward.OAuth2.DataLayer;
using AlwaysMoveForward.OAuth2.DataLayer.Repositories;
using AlwaysMoveForward.Core.Common.DataLayer;

namespace AlwaysMoveForward.OAuth2.BusinessLayer.Services
{
    /// <summary>
    /// The service manager for the OAuth services
    /// TBD:  In .Net Core this can be switched to scoped dependency injection
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
                    this.consumerService = new ConsumerService(this.RepositoryManager.ConsumerRepository);
                }

                return this.consumerService;
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
                    this.userService = new UserService(this.RepositoryManager.UserRepository, this.RepositoryManager.LoginAttemptRepository);
                }

                return this.userService;
            }
        }

        private IApiResourceService apiResourceService;

        /// <summary>
        /// Gets the current ApiResource service
        /// </summary>
        public IApiResourceService ApiResourceService
        {
            get
            {
                if (this.apiResourceService == null)
                {
                    this.apiResourceService = new ApiResourceService(this.RepositoryManager.ApiResourceRepository);
                }

                return this.apiResourceService;
            }
        }

        private IClientService clientService;

        /// <summary>
        /// Gets the current ApiResource service
        /// </summary>
        public IClientService ClientService
        {
            get
            {
                if (this.clientService == null)
                {
                    this.clientService = new ClientService(this.RepositoryManager.ClientRepository);
                }

                return this.clientService;
            }
        }

    }
}
