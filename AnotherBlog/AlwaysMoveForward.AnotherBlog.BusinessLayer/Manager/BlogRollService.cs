using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Core.Entity;
using AnotherBlog.Core.Utilities;

namespace AnotherBlog.Core
{
    /// <summary>
    /// A class to manage teh blog roll links for aparticualr blogs
    /// </summary>
    public class BlogRollService : ServiceBase
    {
        public BlogRollService(ModelContext managerContext)
            : base(managerContext)
        {

        }
        /// <summary>
        /// Initialize and instantiate a BlogRollLink instance
        /// </summary>
        /// <returns></returns>
        public BlogRollLink Create()
        {
            return new BlogRollLink();
        }
        /// <summary>
        /// Get all the blog roll inks stored for a particular blog
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public List<BlogRollLink> GetAllByBlogId(int blogId)
        {
            BlogService blogManager = (BlogService)ServiceFactory.GetManager(ServiceFactory.Types.Blog, this.ModelContext);
            Blog targetBlog = blogManager.GetById(blogId);

            BlogRollGateway gateway = new BlogRollGateway(this.ModelContext.DataContext);
            return gateway.GetAllByBlogId(targetBlog);
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
                blogLink.BlogId = targetBlog.BlogId;

                BlogRollGateway gateway = new BlogRollGateway(this.ModelContext.DataContext);
                gateway.Save(targetBlog, blogLink);  
            }

            return retVal;
        }
    }
}
