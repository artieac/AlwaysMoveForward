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

namespace AnotherBlog.Data.ActiveRecord.Entities
{
    [ActiveRecord("EntryComments")]
    public class AREntryComment : CE.Comment
    {
        public AREntryComment() : base()
        {

        }

        [PrimaryKey(PrimaryKeyType.Identity, "CommentId")]
        public override int CommentId{ get; set;}

        [Property("Status")]
        public override int Status{ get; set;}

        [BelongsTo("BlogId", Type=typeof(ARBlog))]
        public override CE.Blog Blog{ get; set;}

        [Property("Link")]
        public override string Link{ get; set;}

        [Property("AuthorEmail")]
        public override string AuthorEmail{ get; set;}

        [Property("Comment", ColumnType = "StringClob")]
        public override string Text{ get; set;}

        [Property("AuthorName")]
        public override string AuthorName{ get; set;}

        [BelongsTo("EntryId", Type=typeof(ARBlogPost))]
        public override CE.BlogPost Post{ get; set;}

        [Property("DatePosted")]
        public override DateTime DatePosted{ get; set;}
    }
}
