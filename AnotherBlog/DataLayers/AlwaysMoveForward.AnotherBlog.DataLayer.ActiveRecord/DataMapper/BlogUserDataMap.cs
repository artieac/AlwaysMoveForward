using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class BlogUserDataMap : DataMapBase<BlogUser, BlogUserDTO>
    {
        public override BlogUser MapProperties(BlogUserDTO source, BlogUser destination)
        {
            BlogUser retVal = destination;

            if (source != null)
            {
                if (retVal == null)
                {
                    retVal = new BlogUser();
                }

                retVal.Blog = DataMapManager.Mappers().BlogDataMap.Map(retVal, source.Blog);
                retVal.BlogUserId = source.BlogUserId;
                retVal.Role = DataMapManager.Mappers().RoleDataMap.Map(source.Role);
                retVal.User = DataMapManager.Mappers().UserDataMap.Map(source.User);
            }

            return retVal;
        }

        public override BlogUserDTO MapProperties(BlogUser source, BlogUserDTO destination)
        {
            BlogUserDTO retVal = destination;

            if (source != null)
            {
                if (retVal == null)
                {
                    retVal = new BlogUserDTO();
                }

                retVal.Blog = DataMapManager.Mappers().BlogDataMap.Map(retVal, source.Blog);
                retVal.BlogUserId = source.BlogUserId;
                retVal.Role = DataMapManager.Mappers().RoleDataMap.Map(source.Role);
                retVal.User = DataMapManager.Mappers().UserDataMap.Map(source.User);
            }

            return retVal;
        }
    }
}
