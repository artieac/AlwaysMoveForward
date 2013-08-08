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

using AnotherBlog.Common.Utilities;
using AnotherBlog.Common.Data.Map;

namespace AnotherBlog.Common.Data.Entities
{
    public class Comment : IComment
    {
        /// <summary>
        /// What are the allowed comment statuses?  
        /// </summary>
        public struct CommentStatus
        {
            public const int Unapproved = 0;
            public const int Approved = 1;
            public const int Deleted = 2;

        }

        public virtual int CommentId { get; set; }
        public virtual int Status { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual string Link { get; set; }
        public virtual string AuthorEmail { get; set; }
        public virtual string Text { get; set; }
        public virtual string AuthorName { get; set; }
        public virtual BlogPost Post { get; set; }
        public virtual DateTime DatePosted { get; set; }
        
        public virtual void CleanCommentText()
        {
            this.Text = Utils.StripJavascript(this.Text);
        }
    }
}
