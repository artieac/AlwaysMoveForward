﻿/**
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

using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.Common.DataLayer.Entities;

using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Web.Models;

namespace AlwaysMoveForward.AnotherBlog.Web.Models.BlogModels
{
    public class BlogModel 
    {
        public CommonBlogModel BlogCommon { get; set; }
        public IPagedList<BlogPostModel> BlogEntries { get; set; }
        public IList<User> BlogWriters { get; set; }
    }
}