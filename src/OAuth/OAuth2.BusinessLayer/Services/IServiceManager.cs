using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.OAuth2.Common.Factories;
using AlwaysMoveForward.OAuth2.DataLayer;

namespace AlwaysMoveForward.OAuth2.BusinessLayer.Services
{
    /// <summary>
    /// Contains the current instances of the business services
    /// </summary>
    public interface IServiceManager
    {
        /// <summary>
        /// The current unit of work
        /// </summary>
        UnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Gets the current ConsumerService instance
        /// </summary>
        IConsumerService ConsumerService { get; }

        /// <summary>
        /// Gets the current User service instance
        /// </summary>
        IUserService UserService { get; }
    }
}

