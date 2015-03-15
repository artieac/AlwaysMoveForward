using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.Common.Business
{
    public class ServiceManager
    {        
        public ServiceManager(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
        {
            this.UnitOfWork = unitOfWork;
            this.RepositoryManager = repositoryManager;
        }
        
        public IRepositoryManager RepositoryManager { get; private set; }

        public IUnitOfWork UnitOfWork  { get; private set; }
    }
}
