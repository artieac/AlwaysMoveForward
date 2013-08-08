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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Common.Data.Repositories
{
    public interface IBlogEntryRepository : IRepository<BlogPost>
    {
        IList<BlogPost> GetAll(bool publishedOnly, int maxResults);
        IList<BlogPost> GetAllByBlog(Blog targetBlog, bool publishedOnly, int maxResults);
        BlogPost GetByTitle(string blogTitle, Blog targetBlog);
        BlogPost GetByDateAndTitle(string blogTitle, DateTime postDate, Blog targetBlog);
        IList<BlogPost> GetByTag(Tag targetTag, bool publishedOnly);
        IList<BlogPost> GetByTag(Blog targetBlog, Tag targetTag, bool publishedOnly);
        IList<BlogPost> GetByMonth(DateTime blogDate, bool publishedOnly);
        IList<BlogPost> GetByMonth(DateTime blogDate, Blog targetBlog, bool publishedOnly);
        IList<BlogPost> GetByDate(DateTime blogDate, bool publishedOnly);
        IList<BlogPost> GetByDate(DateTime blogDate, Blog targetBlog, bool publishedOnly);
        BlogPost GetMostRecent(Blog targetBlog, bool published);
        BlogPost GetPreviousEntry(Blog targetBlog, BlogPost currentEntry);
        BlogPost GetNextEntry(Blog targetBlog, BlogPost currentEntry);
        IList<DateTime> GetPublishedDatesByMonth(DateTime blogDate);
        IList GetArchiveDates(Blog targetBlog);
    }
}
