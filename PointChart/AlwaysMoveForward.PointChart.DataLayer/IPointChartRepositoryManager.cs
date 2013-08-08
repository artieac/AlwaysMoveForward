using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.DataLayer.Repositories;

namespace AlwaysMoveForward.PointChart.DataLayer
{
    public interface IPointChartRepositoryManager : IRepositoryManager
    {
        ChartRepository Charts { get; }
        CompletedTaskRepository CompletedTask { get; }
        TaskRepository Tasks { get; }
        IDbInfoRepository DbInfo { get; }
        PointEarnerRepository PointEarner { get; }
        PointsSpentRepository PointsSpent { get; }
    }
}
