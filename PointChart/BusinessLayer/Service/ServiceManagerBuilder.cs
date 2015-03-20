using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.OAuth.Client;
using AlwaysMoveForward.OAuth.Client.Configuration;
using AlwaysMoveForward.PointChart.DataLayer;
using AlwaysMoveForward.PointChart.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.BusinessLayer.Service;

namespace AlwaysMoveForward.PointChart.BusinessLayer.Service
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
            UnitOfWork unitOfWork = this.CreateUnitOfWork() as UnitOfWork;
            IPointChartRepositoryManager repositoryManager = this.CreateRepositoryManager(unitOfWork);
            return new ServiceManager(unitOfWork, repositoryManager, this.CreateOAuthClient());
        }

        protected virtual IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork();
        }

        protected virtual IPointChartRepositoryManager CreateRepositoryManager(IUnitOfWork unitOfWork)
        {
            return new RepositoryManager(unitOfWork as UnitOfWork);
        }

        protected virtual OAuthClientBase CreateOAuthClient()
        {
            OAuthKeyConfiguration keyConfiguration = OAuthKeyConfiguration.GetInstance();
            EndpointConfiguration oauthEndpoints = EndpointConfiguration.GetInstance();
            return new AlwaysMoveForward.OAuth.Client.RestSharp.OAuthClient(oauthEndpoints.ServiceUri, keyConfiguration.ConsumerKey, keyConfiguration.ConsumerSecret, oauthEndpoints);
        }
    }
}
