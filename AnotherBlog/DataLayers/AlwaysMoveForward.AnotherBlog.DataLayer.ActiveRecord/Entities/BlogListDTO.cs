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

using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Entities
{
    [ActiveRecord("BlogLists")]
    public class BlogListDTO
    {
        public BlogListDTO()
        {
            this.Id = -1;
        }

        [PrimaryKey(PrimaryKeyType.Identity, "Id", UnsavedValue = "-1")]
        public int Id { get; set; }

        [BelongsTo("BlogId", Type = typeof(BlogDTO))]
        public BlogDTO Blog { get; set; }

        [Property("Name")]
        public string Name { get; set; }

        [Property("ShowOrdered")]
        public bool ShowOrdered { get; set; }

        [HasMany(typeof(BlogListItemDTO), Inverse = true)]
        public IList<BlogListItemDTO> Items { get; set; }
    }
}
