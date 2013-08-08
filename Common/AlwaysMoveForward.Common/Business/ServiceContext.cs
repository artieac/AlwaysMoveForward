using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.Common.Business
{
    public class ServiceContext
    {
        public ServiceContext(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
        {
            this.UnitOfWork = unitOfWork;
            this.RepositoryManager = repositoryManager;
        }

        public IUnitOfWork UnitOfWork { get; private set; }
        public IRepositoryManager RepositoryManager { get; private set; }
    }
}
