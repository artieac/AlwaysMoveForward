using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.PointChart.BusinessLayer.Services;
using AlwaysMoveForward.PointChart.UnitTests.Mock;

namespace AlwaysMoveForward.PointChart.UnitTests
{
    public class UnitTestBase
    {
        public IServiceManager ServiceManager
        {
            get { return MockServiceManagerBuilder.GetServiceManager(); }
        }
    }
}
