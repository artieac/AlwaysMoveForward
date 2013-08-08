using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Core.Entity;

namespace AnotherBlog.Core
{
    public class BlogUserService : ServiceBase
    {
        public BlogUserService(ModelContext managerContext)
            : base(managerContext)
        {
           
        }

        public BlogUser Create()
        {
            return new BlogUser();
        }

        public BlogUser Save(int userId, int blogId, int roleId)
        {            
            BlogUser retVal = null;

            BlogService blogManager = (BlogService)ServiceFactory.GetManager(ServiceFactory.Types.Blog, this.ModelContext);
            Blog validBlog = blogManager.GetById(blogId);

            UserManager userManager = (UserManager)ServiceFactory.GetManager(ServiceFactory.Types.User, this.ModelContext);
            User validUser = userManager.GetById(userId);

            RoleService roleManager = (RoleService)ServiceFactory.GetManager(ServiceFactory.Types.Role, this.ModelContext);
            Role validRole = roleManager.GetById(roleId);

            if (validBlog != null && validUser != null && validRole != null)
            {
                BlogUserGateway gateway = new BlogUserGateway(this.ModelContext.DataContext);
                retVal = gateway.GetUserBlog(userId, blogId);

                if (retVal == null)
                {
                    retVal = this.Create();
                }

                retVal.UserId = userId;
                retVal.BlogId = blogId;
                retVal.RoleId = roleId;

                retVal = gateway.Save(retVal, true);
            }

            return retVal;
        }

        public BlogUser GetUserBlog(int userId, int blogId)
        {
            BlogUserGateway gateway = new BlogUserGateway(this.ModelContext.DataContext);
            return gateway.GetUserBlog(userId, blogId);
        }

        public List<BlogUser> GetUserBlogs(int userId)
        {
            BlogUserGateway gateway = new BlogUserGateway(this.ModelContext.DataContext);
            return gateway.GetUserBlogs(userId);
        }

        public bool DeleteUserBlog(int blogId, int userId)
        {
            bool retVal = false;

            BlogService blogManager = (BlogService)ServiceFactory.GetManager(ServiceFactory.Types.Blog, this.ModelContext);
            Blog validBlog = blogManager.GetById(blogId);

            UserManager userManager = (UserManager)ServiceFactory.GetManager(ServiceFactory.Types.User, this.ModelContext);
            User validUser = userManager.GetById(userId);

            if (validBlog!=null && validUser!=null)
            {
                BlogUserGateway gateway = new BlogUserGateway(this.ModelContext.DataContext);
                retVal = gateway.DeleteUserBlog(validBlog.BlogId, validUser.UserId);
            }

            return retVal;
        }
    }
}
