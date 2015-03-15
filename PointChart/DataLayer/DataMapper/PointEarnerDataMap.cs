using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.PointChart.DataLayer.DTO;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.DataLayer.DataMapper
{
    public class PointEarnerDataMap : DataMapBase<PointEarner, PointEarnerDTO>
    {
        public override PointEarnerDTO Map(PointEarner source, PointEarnerDTO destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override PointEarner Map(PointEarnerDTO source, PointEarner destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
