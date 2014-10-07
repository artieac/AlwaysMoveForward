using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.PointChart.DataLayer.DTO;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.DataLayer.DataMapper
{
    public class TaskDataMap : DataMapBase<Task, TaskDTO>
    {
        public override TaskDTO Map(Task source, TaskDTO destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override Task Map(TaskDTO source, Task destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
