using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AnotherBlog.MVC.Utilities;

namespace AnotherBlog.MVC.Views.RSS
{
    public partial class Comments : ViewPage<AnotherBlog.MVC.Models.RSSModel>
    {
        public string BuildBlogEntryUrl(AnotherBlog.Common.Data.Entities.BlogPost blogEntry)
        {
            string retVal = this.Context.Request.Url.Scheme + "://" + this.Context.Request.Url.Authority;
            retVal += Utils.GenerateBlogEntryLink(blogEntry.Blog.SubFolder, blogEntry, false);

            return retVal;
        }
    }
}

