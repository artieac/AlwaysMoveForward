using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.PointChart.DataLayer.Entities;
using AlwaysMoveForward.PointChart.DataLayer.DTO;

namespace AlwaysMoveForward.PointChart.DataLayer.DataMapper
{
    public class CompletedTaskDataMap
    {
        #region CompletedTask Aggregate Root

        public CompletedTaskDTO Map(CompletedTask source)
        {
            CompletedTaskDTO retVal = null;

            if (source != null)
            {
                retVal = new CompletedTaskDTO();
                retVal.Id = source.Id;
                retVal.DateCompleted = source.DateCompleted;
                retVal.Id = source.Id;
                retVal.NumberOfTimesCompleted = source.NumberOfTimesCompleted;
                retVal.Task = DataMapManager.Mappers().Task.Map(source.Task, retVal);
                retVal.Chart = DataMapManager.Mappers().Chart.Map(source.Chart, retVal);
            }

            return retVal;
        }

        public CompletedTask Map(CompletedTaskDTO source)
        {
            CompletedTask retVal = null;

            if (source != null)
            {
                retVal = new CompletedTask();
                retVal.Id = source.Id;
                retVal.DateCompleted = source.DateCompleted;
                retVal.Id = source.Id;
                retVal.NumberOfTimesCompleted = source.NumberOfTimesCompleted;
                retVal.Task = DataMapManager.Mappers().Task.Map(source.Task, retVal);
                retVal.Chart = DataMapManager.Mappers().Chart.Map(source.Chart, retVal);
            }
            return retVal;
        }

        #endregion

        #region Chart AggregateRoot

        public IList<CompletedTaskDTO> Map(IList<CompletedTask> source, ChartDTO chart)
        {
            IList<CompletedTaskDTO> retVal = new List<CompletedTaskDTO>();

            if (source != null)
            {
                for (int i = 0; i < source.Count; i++)
                {
                    CompletedTaskDTO newItem = new CompletedTaskDTO();
                    newItem.Id = source[i].Id;
                    newItem.DateCompleted = source[i].DateCompleted;
                    newItem.Id = source[i].Id;
                    newItem.NumberOfTimesCompleted = source[i].NumberOfTimesCompleted;
                    newItem.Task = DataMapManager.Mappers().Task.Map(source[i].Task, newItem);
                    newItem.Chart = chart;
                    retVal.Add(newItem);
                }
            }

            return retVal;
        }

        public IList<CompletedTask> Map(IList<CompletedTaskDTO> source, Chart chart)
        {
            IList<CompletedTask> retVal = new List<CompletedTask>();

            if (source != null)
            {
                for (int i = 0; i < source.Count; i++)
                {
                    CompletedTask newItem = new CompletedTask();
                    newItem.Id = source[i].Id;
                    newItem.DateCompleted = source[i].DateCompleted;
                    newItem.Id = source[i].Id;
                    newItem.NumberOfTimesCompleted = source[i].NumberOfTimesCompleted;
                    newItem.Task = DataMapManager.Mappers().Task.Map(source[i].Task, newItem);
                    newItem.Chart = chart;
                    retVal.Add(newItem);
                }
            }

            return retVal;
        }

        #endregion
    }
}
