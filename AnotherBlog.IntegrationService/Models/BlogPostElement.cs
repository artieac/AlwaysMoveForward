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

namespace AnotherBlog.IntegrationService.Models
{
    public class BlogPostElement
    {
        public int EntryId { get; set; }
        public bool IsPublished { get; set; }
        public int BlogId { get; set; }
        public int AuthorId { get; set; }
        public String AuthorName { get; set; }
        public string EntryText { get; set; }
        public string Title { get; set; }
        public DateTime DatePosted { get; set; }
        public IList<TagElement> Tags{ get; set;}
    }
}
