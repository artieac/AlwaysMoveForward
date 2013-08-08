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
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Core.Utilities;

namespace AnotherBlog.Core.Service
{
    /// <summary>
    /// A class to manage teh blog roll links for aparticualr blogs
    /// </summary>
    public class BlogRollService : ServiceBase
    {
        internal BlogRollService(ServiceManager serviceManager)
            : base(serviceManager)
        {

        }
        /// <summary>
        /// Initialize and instantiate a BlogRollLink instance
        /// </summary>
        /// <returns></returns>
        public BlogRollLink Create()
        {
            BlogRollLink retVal = new BlogRollLink();
            retVal.BlogRollLinkId = this.Repositories.BlogLinks.UnsavedId;
            return retVal;
        }
        /// <summary>
        /// Get all the blog roll inks stored for a particular blog
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList<BlogRollLink> GetAllByBlog(Blog targetBlog)
        {
            return Repositories.BlogLinks.GetAll(targetBlog.BlogId);
        }
        /// <summary>
        /// Save a blog roll link for a particulra blog
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <param name="linkName"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public BlogRollLink Save(Blog targetBlog, string linkName, string url)
        {
            BlogRollLink retVal = null;

            if (targetBlog != null)
            {
                BlogRollLink blogLink = this.Create();
                blogLink.LinkName = Utils.StripHtml(linkName);
                blogLink.Url = Utils.StripHtml(url);
                blogLink.Blog = targetBlog;

                Repositories.BlogLinks.Save(blogLink);  
            }

            return retVal;
        }

        public bool Delete(BlogRollLink targetLink)
        {
            return this.Repositories.BlogLinks.Delete(targetLink);
        }
    }
}
