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

using CE=AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Utilities;

namespace AnotherBlog.Data.ActiveRecord.Entities
{
    [ActiveRecord("BlogEntries")]
    public class ARBlogPost : CE.BlogPost
    {
        public ARBlogPost()
        {

        }

        [PrimaryKey(PrimaryKeyType.Identity, "EntryId")]
        public override int EntryId{ get; set;}

        [Property("IsPublished")]
        public override bool IsPublished{ get; set;}

        [BelongsTo("BlogId", Type = typeof(ARBlog))]
        public override CE.Blog Blog{ get; set;}

        [BelongsTo("UserId", Type = typeof(ARUser))]
        public override CE.User Author{ get; set;}

        [Property(ColumnType = "StringClob")]
        public override string EntryText{ get; set;}

        [Property("Title")]
        public override string Title{ get; set;}

        [Property("DatePosted")]
        public override DateTime DatePosted{ get; set;}

        [Property("DateCreated")]
        public override DateTime DateCreated { get; set; }

        [HasMany(typeof(AREntryComment), Lazy = false, Where = "Status=1")]
        public override IList<CE.Comment> Comments{ get; set;}

        [HasAndBelongsToMany(typeof(ARTag), ColumnRef="TagId", ColumnKey="BlogEntryId", Table="BlogEntryTags")]
        public override IList<CE.Tag> Tags { get; set;}
    }
}
