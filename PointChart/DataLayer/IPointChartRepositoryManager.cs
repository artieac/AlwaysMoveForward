using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.DataLayer.Repositories;

namespace AlwaysMoveForward.PointChart.DataLayer
{
    public interface IPointChartRepositoryManager 
    {
        IUserRepository UserRepository { get; }
        ChartRepository Charts { get; }
        CompletedTaskRepository CompletedTask { get; }
        TaskRepository Tasks { get; }
        PointsSpentRepository PointsSpent { get; }
    }
}
