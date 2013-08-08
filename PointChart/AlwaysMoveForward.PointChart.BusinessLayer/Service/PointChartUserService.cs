using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.Business;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.DataLayer;

namespace AlwaysMoveForward.PointChart.BusinessLayer.Service
{
    public class PointChartUserService : UserService<ServiceManager>
    {
        public PointChartUserService(ServiceManager serviceManager, IRepositoryManager repositoryManager) : base(serviceManager, repositoryManager) { }
    }
}
