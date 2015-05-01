using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.PointChart.DataLayer;
using AlwaysMoveForward.PointChart.DataLayer.Repositories;

namespace AlwaysMoveForward.PointChart.UnitTests.Mock.Repositories
{
    public class MockRepositoryManager : IPointChartRepositoryManager
    {
        public IUserRepository UserRepository
        {
            get
            {
                return null;
            }
        }

        public IChartRepository Charts
        {
            get
            {
                return null;
            }
        }

        public ICompletedTaskRepository CompletedTaskRepository
        {
            get
            {
                return null;
            }
        }

        public TaskRepository Tasks
        {
            get
            {
                return null;
            }
        }

        public IPointsSpentRepository PointsSpent
        {
            get
            {
                return null;
            }
        }
    }
}
