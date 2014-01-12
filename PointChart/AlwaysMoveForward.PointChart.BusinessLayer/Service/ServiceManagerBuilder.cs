using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DataLayer;
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
            IUnitOfWork unitOfWork = this.CreateUnitOfWork();
            IPointChartRepositoryManager repositoryManager = this.CreateRepositoryManager(unitOfWork);
            return new ServiceManager(unitOfWork, repositoryManager);
        }

        protected virtual IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork();
        }

        protected virtual IPointChartRepositoryManager CreateRepositoryManager(IUnitOfWork unitOfWork)
        {
            return new RepositoryManager(unitOfWork);
        }
    }
}
