using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.Configuration;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.OAuth.Contracts.Configuration;
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
            DatabaseConfiguration databaseConfiguration = DatabaseConfiguration.GetInstance();
            IUnitOfWork unitOfWork = this.CreateUnitOfWork(databaseConfiguration.GetDecryptedConnectionString());
            IAnotherBlogRepositoryManager repositoryManager = this.CreateRepositoryManager(unitOfWork);
            return new ServiceManager(unitOfWork, repositoryManager, OAuthKeyConfiguration.GetInstance(), EndpointConfiguration.GetInstance());
        }

        protected virtual IUnitOfWork CreateUnitOfWork(string connectionString)
        {
            return new UnitOfWork(connectionString);
        }

        protected virtual IAnotherBlogRepositoryManager CreateRepositoryManager(IUnitOfWork unitOfWork)
        {
            return new RepositoryManager(unitOfWork as UnitOfWork);
        }
    }
}
