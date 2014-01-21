using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.DomainModel.DataMap;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class DbInfoMapper : DataMapBase<DbInfo, DbInfoDTO>
    {
        public override DbInfoDTO Map(DbInfo source, DbInfoDTO destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override DbInfo Map(DbInfoDTO source, DbInfo destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
