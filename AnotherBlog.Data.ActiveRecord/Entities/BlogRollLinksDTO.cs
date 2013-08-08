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

using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Data.ActiveRecord.DataMapper;

namespace AnotherBlog.Data.ActiveRecord.Entities
{
    [ActiveRecord("BlogRollLinks")]
    public class BlogRollLinksDTO : IBlogRollLink
    {
        public BlogRollLinksDTO() : base()
        {

        }

        [PrimaryKey(PrimaryKeyType.Identity, "BlogRollLinkId", UnsavedValue = "-1")]
        public int BlogRollLinkId{ get; set;}

        [Property("LinkName")]
        public string LinkName{ get; set;}

        [Property("Url")]
        public string Url{ get; set;}

        [BelongsTo("BlogId", Type=typeof(BlogDTO))]
        public BlogDTO BlogDTO{ get; set;}

        public Blog Blog
        {
            get { return BlogMapper.GetInstance().Map(this.BlogDTO); }
            set { this.BlogDTO = BlogMapper.GetInstance().Map(value); }
        }
    }
}
