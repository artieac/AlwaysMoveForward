﻿using System;
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
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override BlogUserDTO MapProperties(BlogUser source, BlogUserDTO destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
