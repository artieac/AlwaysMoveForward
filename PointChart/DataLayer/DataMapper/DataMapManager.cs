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

        private static DataMapManager dataMapManager = null;

        public static DataMapManager Mappers()
        {
            if (DataMapManager.dataMapManager == null)
            {
                DataMapManager.dataMapManager = new DataMapManager();
            }

            return DataMapManager.dataMapManager;
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
                if (this.chartDataMap == null)
                {
                    this.chartDataMap = new ChartDataMap();
                }

                return this.chartDataMap;
            }
        }

        public CompletedTaskDataMap CompletedTask
        {
            get
            {
                if (this.ctiDataMap == null)
                {
                    this.ctiDataMap = new CompletedTaskDataMap();
                }

                return this.ctiDataMap;
            }
        }

        public PointEarnerDataMap PointEarner
        {
            get
            {
                if (this.pointEarnerMap == null)
                {
                    this.pointEarnerMap = new PointEarnerDataMap();
                }

                return this.pointEarnerMap;
            }
        }


        public PointsSpentDataMap PointsSpent
        {
            get
            {
                if (this.pointsSpentMap == null)
                {
                    this.pointsSpentMap = new PointsSpentDataMap();
                }

                return this.pointsSpentMap;
            }
        }

        public TaskDataMap Task
        {
            get
            {
                if (this.taskDataMap == null)
                {
                    this.taskDataMap = new TaskDataMap();
                }

                return this.taskDataMap;
            }
        }
    }
}
