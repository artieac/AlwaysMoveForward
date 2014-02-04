using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.Common.Business
{
    /// <summary>
    /// A contxt class for passing around common service parameters
    /// </summary>
    public class ServiceContext
    {
        /// <summary>
        /// Initializes an instance of ServiceContxt
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="repositoryManager"></param>
        public ServiceContext(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
        {
            this.UnitOfWork = unitOfWork;
            this.RepositoryManager = repositoryManager;
        }
        /// <summary>
        /// Gets the UnitOfWork
        /// </summary>
        public IUnitOfWork UnitOfWork { get; private set; }
        /// <summary>
        /// Gets the Repository Manager
        /// </summary>
        public IRepositoryManager RepositoryManager { get; private set; }
    }
}
