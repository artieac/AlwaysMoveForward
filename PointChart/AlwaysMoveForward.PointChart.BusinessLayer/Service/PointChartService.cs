using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.Business;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.DataLayer;

namespace AlwaysMoveForward.PointChart.BusinessLayer.Service
{
    public class PointChartService
    {
        public PointChartService(IUnitOfWork unitOfWork, IPointChartRepositoryManager repositoryManager)
        {
            this.UnitOfWork = unitOfWork;
            this.PointChartRepositories = repositoryManager;
        }

        public IUnitOfWork UnitOfWork { get; private set; }
        public IPointChartRepositoryManager PointChartRepositories { get; private set;}
    }
}
