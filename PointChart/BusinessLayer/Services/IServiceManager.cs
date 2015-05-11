using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.BusinessLayer.Services
{
    public interface IServiceManager
    {
        UserService UserService { get; }
        ChartService Charts { get; }
        TaskService Tasks { get; }
        CompletedTaskService CompletedTaskService { get; }
        PointService PointService { get; }
    }
}
