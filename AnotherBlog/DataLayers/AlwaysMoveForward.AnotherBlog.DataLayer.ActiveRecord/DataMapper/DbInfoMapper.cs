using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.DomainModel.DataMap;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class DbInfoMapper : DataMapBase<DbInfo, DbInfoDTO>
    {
        public override DbInfoDTO MapProperties(DbInfo source, DbInfoDTO destination)
        {
            DbInfoDTO retVal = destination;

            if (source != null)
            {
                if (retVal == null)
                {
                    retVal = new DbInfoDTO();
                }
                retVal.Version = source.Version;
            }

            return retVal;
        }

        public override DbInfo MapProperties(DbInfoDTO source, DbInfo destination)
        {
            throw new NotImplementedException();
        }
    }
}
