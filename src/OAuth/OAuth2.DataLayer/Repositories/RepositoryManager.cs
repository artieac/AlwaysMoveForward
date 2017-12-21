using AlwaysMoveForward.Core.Common.DataLayer.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth2.DataLayer.Repositories
{
    /// <summary>
    /// Wraps up instances of the repositories so they can all participate in the same unit of work
    /// TBD:  In .Net Core this can be switched to scoped dependency injection
    /// </summary>
    public class RepositoryManager : IRepositoryManager
    {
        /// <summary>
        /// The constructor that takes a unit of work as a parameter
        /// </summary>
        /// <param name="unitOfWork">The unit of work</param>
        public RepositoryManager(UnitOfWork unitOfWork, NewUnitOfWork newUnitOfWork)
        {
            this.UnitOfWork = unitOfWork;
            this.NewUnitOfWork = newUnitOfWork;
        }

        /// <summary>
        /// The contained instance of the Unit Of Work
        /// </summary>
        protected UnitOfWork UnitOfWork { get; set; }

        protected NewUnitOfWork NewUnitOfWork { get; set; }

        /// <summary>
        /// The current instance of the ConsumerRepository
        /// </summary>
        private IConsumerRepository consumerRepository;

        /// <summary>
        /// Gets the current instance of the ConsumerRepository
        /// </summary>
        public IConsumerRepository ConsumerRepository
        {
            get
            {
                if (this.consumerRepository == null)
                {
                    this.consumerRepository = new ConsumerRepository(this.NewUnitOfWork);
                }

                return this.consumerRepository;
            }
        }

        /// <summary>
        /// The current instance of the UserRepository
        /// </summary>
        private IAMFUserRepository userRepository;

        /// <summary>
        /// Gets the current instance of the ConsumerNonceRepository
        /// </summary>
        public IAMFUserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new AMFUserRepository(this.NewUnitOfWork);
                }

                return this.userRepository;
            }
        }

        /// <summary>
        /// The current instance of the LoginAttemptRepository
        /// </summary>
        private ILoginAttemptRepository loginAttemptRepository;

        /// <summary>
        /// Gets the current instance of the loginAttemptRepository
        /// </summary>
        public ILoginAttemptRepository LoginAttemptRepository
        {
            get
            {
                if (this.loginAttemptRepository == null)
                {
                    this.loginAttemptRepository = new LoginAttemptRepository(this.NewUnitOfWork);
                }

                return this.loginAttemptRepository;
            }
        }

        /// <summary>
        /// The current instance of the ApiResourceRepository
        /// </summary>
        private IApiResourceRepository apiResourceRepository;

        /// <summary>
        /// Gets the current instance of the loginAttemptRepository
        /// </summary>
        public IApiResourceRepository ApiResourceRepository
        {
            get
            {
                if (this.apiResourceRepository == null)
                {
                    this.apiResourceRepository = new ApiResourceRepository(this.NewUnitOfWork);
                }

                return this.apiResourceRepository;
            }
        }

        /// <summary>
        /// The current instance of the ApiResourceRepository
        /// </summary>
        private IClientRepository clientRepository;

        /// <summary>
        /// Gets the current instance of the loginAttemptRepository
        /// </summary>
        public IClientRepository ClientRepository
        {
            get
            {
                if (this.clientRepository == null)
                {
                    this.clientRepository = new ClientRepository(this.NewUnitOfWork);
                }

                return this.clientRepository;
            }
        }

    }
}