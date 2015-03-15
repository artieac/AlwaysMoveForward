using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.PointChart.DataLayer.DTO;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.DataLayer.DataMapper
{
    public class ChartDataMap : DataMapBase<Chart, ChartDTO>
    {
        #region Chart Aggregate Root

        public override ChartDTO Map(Chart source, ChartDTO destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override Chart Map(ChartDTO source, Chart destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        #endregion
    }
}
