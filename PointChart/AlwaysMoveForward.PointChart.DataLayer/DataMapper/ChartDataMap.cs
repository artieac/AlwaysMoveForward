using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.PointChart.DataLayer.DTO;
using AlwaysMoveForward.PointChart.DataLayer.Entities;

namespace AlwaysMoveForward.PointChart.DataLayer.DataMapper
{
    public class ChartDataMap
    {
        #region Chart Aggregate Root

        public ChartDTO Map(Chart source)
        {
            ChartDTO retVal = null;

            if (source != null)
            {
                retVal = new ChartDTO();
                retVal.AdministratorId = source.AdministratorId;
                retVal.Id = source.Id;
                retVal.Name = source.Name;
                retVal.PointEarner = DataMapManager.Mappers().PointEarner.Map(source.PointEarner, retVal);
                retVal.Tasks = DataMapManager.Mappers().Task.Map(source.Tasks, retVal.PointEarner, retVal);
                retVal.CompletedTasks = DataMapManager.Mappers().CompletedTask.Map(source.CompletedTasks,
                                                                                   retVal);
            }

            return retVal;
        }

        public Chart Map(ChartDTO source)
        {
            Chart retVal = null;

            if (source != null)
            {
                retVal = new Chart();
                retVal.AdministratorId = source.AdministratorId;
                retVal.Id = source.Id;
                retVal.Name = source.Name;
                retVal.PointEarner = DataMapManager.Mappers().PointEarner.Map(source.PointEarner, retVal);
                retVal.Tasks = DataMapManager.Mappers().Task.Map(source.Tasks, retVal.PointEarner, retVal);
                retVal.CompletedTasks = DataMapManager.Mappers().CompletedTask.Map(source.CompletedTasks,
                                                                                   retVal);
            }

            return retVal;
        }

        #endregion

        #region PointOwner Aggregate Root

        public Chart Map(ChartDTO source, PointEarner ownerObject)
        {
            Chart retVal = null;

            if (source != null)
            {
                retVal = new Chart();
                retVal.AdministratorId = source.AdministratorId;
                retVal.Id = source.Id;
                retVal.PointEarner = ownerObject;
                retVal.Name = source.Name;
            }

            return retVal;
        }

        public ChartDTO Map(Chart source, PointEarnerDTO ownerObject)
        {
            ChartDTO retVal = null;

            if (source != null)
            {
                retVal = new ChartDTO();
                retVal.AdministratorId = source.AdministratorId;
                retVal.Id = source.Id;
                retVal.PointEarner = ownerObject;
                retVal.Name = source.Name;
                retVal.Tasks = DataMapManager.Mappers().Task.Map(source.Tasks, ownerObject, retVal);
            }

            return retVal;
        }

        public IList<Chart> Map(IList<ChartDTO> source, PointEarner ownerObject)
        {
            IList<Chart> retVal = new List<Chart>();

            if(source!=null)
            {
                for(int i = 0; i < source.Count; i++)
                {
                    retVal.Add(this.Map(source[i], ownerObject));
                }
            }
            return retVal;
        }

        public IList<ChartDTO> Map(IList<Chart> source, PointEarnerDTO ownerObject)
        {
            IList<ChartDTO> retVal = new List<ChartDTO>();

            if (source != null)
            {
                for (int i = 0; i < source.Count; i++)
                {
                    retVal.Add(this.Map(source[i], ownerObject));
                }
            }
            return retVal;
        }

        #endregion

        #region CompletedTask Aggregate Root

        public ChartDTO Map(Chart source, CompletedTaskDTO completedTask)
        {
            ChartDTO retVal = null;

            if (source != null)
            {
                retVal = new ChartDTO();
                retVal.AdministratorId = source.AdministratorId;
                retVal.Id = source.Id;
                retVal.Name = source.Name;
                retVal.CompletedTasks = new List<CompletedTaskDTO>();
                retVal.CompletedTasks.Add(completedTask);
            }

            return retVal;
        }

        public Chart Map(ChartDTO source, CompletedTask completedTask)
        {
            Chart retVal = null;

            if (source != null)
            {
                retVal = new Chart();
                retVal.AdministratorId = source.AdministratorId;
                retVal.Id = source.Id;
                retVal.Name = source.Name;
                retVal.CompletedTasks = new List<CompletedTask>();
                retVal.CompletedTasks.Add(completedTask);
            }

            return retVal;
        }

        #endregion
    }
}
