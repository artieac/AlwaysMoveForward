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

namespace AnotherBlog.Data.ActiveRecord.Entities
{
    [ActiveRecord("EntryComments")]
    public class EntryCommentsDTO : Comment
    {
        public EntryCommentsDTO() : base()
        {

        }

        [PrimaryKey(PrimaryKeyType.Identity, "CommentId", UnsavedValue="-1")]
        public override int CommentId { get; set; }

        [Property("Status")]
        public override int Status { get; set; }

        [BelongsTo("BlogId", Type=typeof(BlogDTO))]
        public override Blog Blog { get; set; }

        [Property("Link")]
        public override string Link { get; set; }

        [Property("AuthorEmail")]
        public override string AuthorEmail { get; set; }

        [Property("Comment", ColumnType = "StringClob")]
        public override string Text { get; set; }

        [Property("AuthorName")]
        public override string AuthorName { get; set; }

        [BelongsTo("EntryId", Type=typeof(BlogPostDTO))]
        public override BlogPost Post { get; set; }

        [Property("DatePosted")]
        public override DateTime DatePosted { get; set; }
    }
}
