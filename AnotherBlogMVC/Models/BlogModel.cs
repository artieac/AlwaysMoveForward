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
using System.Web;

using AnotherBlog.Common.Utilities;
using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.MVC.Models
{
    public class BlogModel : ModelBase
    {
        public BlogModel()
            : base()
        {

        }

        public PagedList<BlogPost> BlogEntries{ get; set;}
        public IList<User> BlogWriters{ get; set;}
        public BlogPost BlogEntry { get; set; }
        public IList<Tag> EntryTags { get; set; }
        public BlogPost PreviousEntry { get; set; }
        public BlogPost NextEntry { get; set; }
        public IList<Comment> Comments { get; set; }
    }
}
