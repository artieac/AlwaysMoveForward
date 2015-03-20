using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.DataLayer.DataMapper
{
    public class PointEarnerDataMap : DataMapBase<PointEarner, DTO.PointEarner>
    {
        static PointEarnerDataMap()
        {
            PointEarnerDataMap.ConfigureAutoMapper();
        }

        public static void ConfigureAutoMapper()
        {
            var existingMap = Mapper.FindTypeMapFor<PointEarner, DTO.PointEarner>();
            if (existingMap == null)
            {
                var newMap = Mapper.CreateMap<PointEarner, DTO.PointEarner>();
                newMap.MaxDepth(2);
            }

            existingMap = Mapper.FindTypeMapFor<DTO.PointEarner, PointEarner>();
            if (existingMap == null)
            {
                var newMap = Mapper.CreateMap<DTO.PointEarner, PointEarner>();
                newMap.MaxDepth(2);
            }
#if DEBUG
            Mapper.AssertConfigurationIsValid();
#endif        
        }

        public override DTO.PointEarner Map(PointEarner source, DTO.PointEarner destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override PointEarner Map(DTO.PointEarner source, PointEarner destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
