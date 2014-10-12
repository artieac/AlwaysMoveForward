﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VP.Digital.Security.OAuth.DataLayer.Repositories
{
    /// <summary>
    /// The common interface for the repository manager.  This makes mocking easier
    /// </summary>
    public interface IRepositoryManager
    {        
        /// <summary>
        /// Gets the current instance of the RequestTokenRepository
        /// </summary>
        IRequestTokenRepository RequestTokenRepository { get; }

        /// <summary>
        /// Gets the current instance of the ConsumerRepository
        /// </summary>
        IConsumerRepository ConsumerRepository { get; }

        /// <summary>
        /// Gets the current instance of the ConsumerNonceRepository
        /// </summary>
        IConsumerNonceRepository ConsumerNonceRepository { get; }

        /// <summary>
        /// Gets the current instance of the DigitalUserRepository
        /// </summary>
        IDigitalUserRepository DigitalUserRepository { get; }

        /// <summary>
        /// Gets the current instance of the loginAttemptRepository
        /// </summary>
        ILoginAttemptRepository LoginAttemptRepository { get; }
    }
}