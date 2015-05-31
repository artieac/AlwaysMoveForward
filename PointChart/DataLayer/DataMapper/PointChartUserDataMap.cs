using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.DataLayer.DataMapper
{
    public class PointChartUserDataMap : DataMapBase<PointChartUser, DTO.User>
    {
        static PointChartUserDataMap()
        {
            PointChartUserDataMap.ConfigureAutoMapper();
        }

        public static void ConfigureAutoMapper()
        {
            var existingMap = Mapper.FindTypeMapFor<PointChartUser, DTO.User>();
            if (existingMap == null)
            {
                var newMap = Mapper.CreateMap<PointChartUser, DTO.User>()
                    .ForMember(dest => dest.PointEarners, src => src.ResolveUsing<UserDTOListResolver>())
                    .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => ((IRemoteOAuthUser)src).AccessToken))
                    .ForMember(dest => dest.AccessTokenSecret, opt => opt.MapFrom(src => ((IRemoteOAuthUser)src).AccessTokenSecret));
                newMap.MaxDepth(2);
            }

            existingMap = Mapper.FindTypeMapFor<DTO.User, PointChartUser>();
            if (existingMap == null)
            {
                var newMap = Mapper.CreateMap<DTO.User, PointChartUser>()
                    .ForMember(dest => ((IRemoteOAuthUser)dest).AccessToken, opt => opt.MapFrom(src => src.AccessToken))
                    .ForMember(dest => ((IRemoteOAuthUser)dest).AccessTokenSecret, opt => opt.MapFrom(src =>src.AccessTokenSecret));
                newMap.MaxDepth(2);
            }
#if DEBUG
            Mapper.AssertConfigurationIsValid();
#endif        
        }

        #region PointChartUser Aggregate Root

        public override DTO.User Map(PointChartUser source, DTO.User destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override PointChartUser Map(DTO.User source, PointChartUser destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        #endregion
    }
}
