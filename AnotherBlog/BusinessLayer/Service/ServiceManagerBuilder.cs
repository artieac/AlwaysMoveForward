using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.ActiveRecord;
using AlwaysMoveForward.AnotherBlog.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.BusinessLayer.Service;

namespace AlwaysMoveForward.AnotherBlog.BusinessLayer.Service
{
    public class ServiceManagerBuilder
    {
        public static ServiceManager BuildServiceManager()
        {
            ServiceManagerBuilder serviceManagerBuilder = new ServiceManagerBuilder();
            return serviceManagerBuilder.CreateServiceManager();
        }

        public ServiceManagerBuilder()
        {

        }

        public ServiceManager CreateServiceManager()
        {
            IUnitOfWork unitOfWork = this.CreateUnitOfWork();
            IAnotherBlogRepositoryManager repositoryManager = this.CreateRepositoryManager(unitOfWork);
            return new ServiceManager(unitOfWork, repositoryManager);
        }

        protected virtual IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork();
        }

        protected virtual IAnotherBlogRepositoryManager CreateRepositoryManager(IUnitOfWork unitOfWork)
        {
            return new RepositoryManager(unitOfWork as UnitOfWork);
        }
    }
}
