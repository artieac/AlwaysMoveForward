using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

using AnotherBlog.Core.Entity;

namespace AnotherBlog.Core
{
    /// <summary>
    /// A managing class for a blog objects business rules.
    /// </summary>
    public class BlogService: ServiceBase
    {
        public BlogService(ModelContext managerContext)
            : base(managerContext)
        {

        }
        /// <summary>
        /// Initialize and instantiate a Blog object instance.
        /// </summary>
        /// <returns></returns>
        public Blog Create()
        {
            Blog retVal = new Blog();
            return retVal;
        }
        /// <summary>
        /// Get the default blog for the site (the first one created)
        /// </summary>
        /// <returns></returns>
        public Blog GetDefaultBlog()
        {
            BlogGateway gateway = new BlogGateway(this.ModelContext.DataContext);
            return gateway.GetById(1);
        }
        /// <summary>
        /// Get all blogs configured in the system.
        /// </summary>
        /// <returns></returns>
        public List<Blog> GetAll()
        {
            BlogGateway gateway = new BlogGateway(this.ModelContext.DataContext);
            return gateway.GetAll();
        }
        /// <summary>
        /// Get all blogs that a user is associated with (via different security roles)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Blog> GetByUserId(int userId)
        {
            BlogGateway gateway = new BlogGateway(this.ModelContext.DataContext);
            return gateway.GetByUserId(userId);
        }
        /// <summary>
        /// Get a particular blog by an id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Blog GetById(int id)
        {
            BlogGateway gateway = new BlogGateway(this.ModelContext.DataContext);
            return gateway.GetById(id);
        }
        /// <summary>
        /// Get a particular blog by its name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Blog GetByName(string name)
        {
            BlogGateway gateway = new BlogGateway(this.ModelContext.DataContext);
            return gateway.GetByName(name);
        }
        /// <summary>
        /// Get a particular blog by its site subfolder
        /// </summary>
        /// <param name="subFolder"></param>
        /// <returns></returns>
        public Blog GetBySubFolder(string subFolder)
        {
            BlogGateway gateway = new BlogGateway(this.ModelContext.DataContext);
            return gateway.GetBySubFolder(subFolder);
        }
        /// <summary>
        /// Save a blog instance and its configuration settings.
        /// </summary>
        /// <param name="blogId"></param>
        /// <param name="name"></param>
        /// <param name="subFolder"></param>
        /// <param name="description"></param>
        /// <param name="about"></param>
        /// <param name="blogWelcome"></param>
        /// <returns></returns>
        public Blog Save(int blogId, string name, string subFolder, string description, string about, string blogWelcome, string blogTheme)
        {
            Blog itemToSave = null;
            BlogGateway gateway = new BlogGateway(this.ModelContext.DataContext);

            if (blogId <= 0)
            {
                itemToSave = this.Create();
            }
            else
            {
                itemToSave = gateway.GetById(blogId);
            }

            itemToSave.Name = name;
            itemToSave.SubFolder = subFolder;
            itemToSave.Description = description;
            itemToSave.About = about;
            itemToSave.WelcomeMessage = blogWelcome;
            itemToSave.Theme = blogTheme;

            gateway.Save(itemToSave, true);

            return itemToSave;
        }
    }
}
