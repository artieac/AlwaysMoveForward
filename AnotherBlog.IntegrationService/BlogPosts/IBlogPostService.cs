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
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using AnotherBlog.IntegrationService.BlogPosts.Requests;
using AnotherBlog.IntegrationService.BlogPosts.Responses;

namespace AnotherBlog.IntegrationService.BlogPosts
{
    // NOTE: If you change the interface name "IBlogPostService" here, you must also update the reference to "IBlogPostService" in Web.config.
    [ServiceContract]
    public interface IBlogPostService
    {
        [OperationContract]
        LoginResponse LoginUser(LoginRequest request);

        [OperationContract]
        GetBlogsResponse GetBlogs(GetBlogsRequest request);

        [OperationContract]
        GetAllBlogEntriesByBlogResponse GetAllBlogEntriesByBlog(GetAllBlogEntriesByBlogRequest request);

    }
}
