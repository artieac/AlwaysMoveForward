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
    [ActiveRecord("EntryComments")]
    public class EntryCommentsDTO 
    {
        public EntryCommentsDTO() : base()
        {
            this.CommentId = -1;
        }

        [PrimaryKey(PrimaryKeyType.Identity, "CommentId", UnsavedValue = "-1")]
        public int CommentId { get; set; }

        [Property("Status")]
        public int Status { get; set; }

        [Property("Link")]
        public string Link { get; set; }

        [Property("AuthorEmail")]
        public string AuthorEmail { get; set; }

        [Property("Comment", ColumnType = "StringClob")]
        public string Text { get; set; }

        [Property("AuthorName")]
        public string AuthorName { get; set; }

        [Property("EntryId")]
        public int PostId { get; set; }

        [Property("DatePosted")]
        public DateTime DatePosted { get; set; }
    }
}
