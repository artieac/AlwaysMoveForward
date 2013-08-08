using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.PointChart.DataLayer.DTO;
using AlwaysMoveForward.PointChart.DataLayer.Entities;

namespace AlwaysMoveForward.PointChart.DataLayer.DataMapper
{
    public class TaskDataMap
    {
        public TaskDTO Map(Task source)
        {
            TaskDTO retVal = null;

            if (source != null)
            {
                retVal = new TaskDTO();
                retVal.Id = source.Id;
                retVal.MaxAllowedDaily = source.MaxAllowedDaily;
                retVal.Name = source.Name;
                retVal.Points = source.Points;
                retVal.AdministratorId = source.AdministratorId;
            }

            return retVal;
        }

        public Task Map(TaskDTO source)
        {
            Task retVal = null;

            if (source != null)
            {
                retVal = new Task();
                retVal.Id = source.Id;
                retVal.MaxAllowedDaily = source.MaxAllowedDaily;
                retVal.Name = source.Name;
                retVal.Points = source.Points;
                retVal.AdministratorId = source.AdministratorId;
            }

            return retVal;
        }

        #region PointEarnet Aggregate Root

        public IList<TaskDTO> Map(IList<Task> source, PointEarnerDTO pointEarner, ChartDTO ownerChart)
        {
            IList<TaskDTO> retVal = new List<TaskDTO>();

            if (source != null)
            {
                for (int i = 0; i < source.Count; i++)
                {
                    TaskDTO newDTO = this.Map(source[i]);
                    newDTO.Charts = new List<ChartDTO>();
                    newDTO.Charts.Add(ownerChart);
                    retVal.Add(newDTO);
                }
            }

            return retVal;
        }

        public IList<Task> Map(IList<TaskDTO> source, PointEarner pointEarner, Chart ownerChart)
        {
            IList<Task> retVal = new List<Task>();

            if (source != null)
            {
                for (int i = 0; i < source.Count; i++)
                {
                    Task newDTO = this.Map(source[i]);
                    newDTO.Charts = new List<Chart>();
                    newDTO.Charts.Add(ownerChart);
                    retVal.Add(newDTO);
                }
            }

            return retVal;
        }

        #endregion

        #region CompletedTask Aggregate Root

        public TaskDTO Map(Task source, CompletedTaskDTO completedTask)
        {
            TaskDTO retVal = new TaskDTO();

            if (source != null)
            {
                retVal = this.Map(source);
                retVal.CompletedTasks = new List<CompletedTaskDTO>();
                retVal.CompletedTasks.Add(completedTask);
            }

            return retVal;
        }

        public Task Map(TaskDTO source, CompletedTask completedTask)
        {
            Task retVal = new Task();

            if (source != null)
            {
                retVal = this.Map(source);
                retVal.CompletedTasks = new List<CompletedTask>();
                retVal.CompletedTasks.Add(completedTask);
            }

            return retVal;
        }

        #endregion

    }
}
