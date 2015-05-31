using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.PointChart.BusinessLayer.Services;
using AlwaysMoveForward.PointChart.DataLayer;
using AlwaysMoveForward.PointChart.UnitTests.Mock.Repositories;

namespace AlwaysMoveForward.PointChart.UnitTests.Mock
{
    public class MockServiceManagerBuilder : ServiceManagerBuilder
    {
        public static IServiceManager GetServiceManager()
        {
            var mockServiceManagerBuilder = new MockServiceManagerBuilder();
            return mockServiceManagerBuilder.CreateServiceManager();
        }

        protected override IPointChartRepositoryManager CreateRepositoryManager(IUnitOfWork unitOfWork)
        {
            return new MockRepositoryManager();
        }
    }
}