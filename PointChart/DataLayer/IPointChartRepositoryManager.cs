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
        IChartRepository Charts { get; }
        ICompletedTaskRepository CompletedTaskRepository { get; }
        TaskRepository Tasks { get; }
        IPointsSpentRepository PointsSpent { get; }
    }
}
