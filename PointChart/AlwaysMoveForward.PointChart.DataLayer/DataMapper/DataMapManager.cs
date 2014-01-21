using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.DataLayer.DataMapper
{
    public class DataMapManager
    {
        static DataMapManager()
        {
            AutoMapperConfiguration.Configure();
        }

        private static DataMapManager _dataMapManager = null;

        public static DataMapManager Mappers()
        {
            if(_dataMapManager==null)
            {
                _dataMapManager = new DataMapManager();
            }

            return _dataMapManager;
        }

        private ChartDataMap chartDataMap = null;
        private CompletedTaskDataMap ctiDataMap = null;
        private PointEarnerDataMap pointEarnerMap = null;
        private PointsSpentDataMap pointsSpentMap = null;
        private TaskDataMap taskDataMap = null;

        public ChartDataMap Chart
        {
            get
            {
                if(chartDataMap==null)
                {
                    chartDataMap = new ChartDataMap();
                }

                return chartDataMap;
            }
        }

        public CompletedTaskDataMap CompletedTask
        {
            get
            {
                if (ctiDataMap == null)
                {
                    ctiDataMap = new CompletedTaskDataMap();
                }

                return ctiDataMap;
            }
        }

        public PointEarnerDataMap PointEarner
        {
            get
            {
                if (pointEarnerMap == null)
                {
                    pointEarnerMap = new PointEarnerDataMap();
                }

                return pointEarnerMap;
            }
        }


        public PointsSpentDataMap PointsSpent
        {
            get
            {
                if (pointsSpentMap == null)
                {
                    pointsSpentMap = new PointsSpentDataMap();
                }

                return pointsSpentMap;
            }
        }

        public TaskDataMap Task
        {
            get
            {
                if (taskDataMap == null)
                {
                    taskDataMap = new TaskDataMap();
                }

                return taskDataMap;
            }
        }
    }
}
