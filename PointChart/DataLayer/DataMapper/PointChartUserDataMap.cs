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
    public class PointChartUserDataMap : DataMapBase<PointChartUser, UserDTO>
    {
        static PointChartUserDataMap()
        {
            PointChartUserDataMap.ConfigureAutoMapper();
        }

        public static void ConfigureAutoMapper()
        {
            var existingMap = Mapper.FindTypeMapFor<PointChartUser, DTO.UserDTO>();
            if (existingMap == null)
            {
                var newMap = Mapper.CreateMap<PointChartUser, DTO.UserDTO>();
                newMap.MaxDepth(2);
            }

            existingMap = Mapper.FindTypeMapFor<DTO.UserDTO, PointChartUser>();
            if (existingMap == null)
            {
                var newMap = Mapper.CreateMap<DTO.UserDTO, PointChartUser>();
                newMap.MaxDepth(2);
            }
#if DEBUG
            Mapper.AssertConfigurationIsValid();
#endif        
        }

        #region PointChartUser Aggregate Root

        public override UserDTO Map(PointChartUser source, UserDTO destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override PointChartUser Map(UserDTO source, PointChartUser destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        #endregion
    }
}
