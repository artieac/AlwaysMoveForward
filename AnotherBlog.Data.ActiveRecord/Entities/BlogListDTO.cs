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
using AnotherBlog.Data.ActiveRecord.Entities;

namespace AnotherBlog.Data.ActiveRecord.Entities
{
    [ActiveRecord("BlogLists")]
    public class BlogListDTO : BlogList, IBlogList
    {
        [PrimaryKey(PrimaryKeyType.Identity, "Id", UnsavedValue = "-1")]
        public override int Id { get; set; }

        [BelongsTo("BlogId", Type = typeof(BlogDTO))]
        public override Blog Blog { get; set; }

        [Property("Name")]
        public override String Name { get; set; }

        [Property("ShowOrdered")]
        public override Boolean ShowOrdered { get; set; }
    }
}
