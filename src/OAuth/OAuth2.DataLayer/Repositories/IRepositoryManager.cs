using System;
using System.Collections.Generic;
using System.Linq;

namespace AlwaysMoveForward.OAuth2.DataLayer.Repositories
{
    /// <summary>
    /// The common interface for the repository manager.  This makes mocking easier
    /// </summary>
    public interface IRepositoryManager
    {        
        /// <summary>
        /// Gets the current instance of the ConsumerRepository
        /// </summary>
        IConsumerRepository ConsumerRepository { get; }

        /// <summary>
        /// Gets the current instance of the AMFUserRepository
        /// </summary>
        IAMFUserRepository UserRepository { get; }

        /// <summary>
        /// Gets the current instance of the loginAttemptRepository
        /// </summary>
        ILoginAttemptRepository LoginAttemptRepository { get; }
    }
}