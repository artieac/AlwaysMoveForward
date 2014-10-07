/**
 * Copyright (c) 2009 Arthur Correa.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Common Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/cpl1.0.php
 *
 * Contributors:
 *    Arthur Correa – initial contribution
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Castle.ActiveRecord;

using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Entities
{
    [ActiveRecord("BlogUsers")]
    public class BlogUserDTO 
    {
        public BlogUserDTO()
        {
            this.BlogUserId = -1;
        }

        [PrimaryKey(PrimaryKeyType.Identity, "BlogUserId", UnsavedValue = "-1")]
        public int BlogUserId { get; set; }

        [BelongsTo("BlogId", Type = typeof(BlogDTO))]
        public BlogDTO Blog { get; set; }

        [BelongsTo("UserId", Type = typeof(UserDTO))]
        public UserDTO User { get; set; }

        [BelongsTo("RoleId", Type = typeof(RoleDTO))]
        public RoleDTO Role { get; set; }
    }
}
