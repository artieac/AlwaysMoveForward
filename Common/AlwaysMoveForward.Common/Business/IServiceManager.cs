using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DataLayer;

namespace AlwaysMoveForward.Common.Business
{
    /// <summary>
    /// A common interface for all Service Managers
    /// </summary>
    public interface IServiceManager
    {
        /// <summary>
        /// All service managers will have a unit of work
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
    }
}
