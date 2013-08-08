using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TheOffWing.AnotherBlog.Core;
using TheOffWing.AnotherBlog.Core.SystemExtensions;

namespace TheOffWing.AnotherBlog.Core.Entity
{
    public class BlogGateway : GatewayBase
    {
        /// <summary>
        /// This class contains all the code to extract data from the repository using LINQ
        /// </summary>
        /// <param name="dataContext"></param>
        public BlogGateway(DataContextManager dataContext)
            : base(dataContext)
        {
        }
        /// <summary>
        /// Save the blog, first check to make sure its not already in the database, if not then insert it
        /// otherwise the update will happen automatically.
        /// </summary>
        /// <param name="itemToSave">The Blog instance that will be saved.</param>
        /// <param name="_submitChanges">Sometimes the submission should be delayed so that it participtes in a specific transaction.  submitChanges is used to control that delay</param>
        public void Save(Blog itemToSave, bool _submitChanges)
        {
            Blog targetItem = null;

            try
            {
                targetItem = this.GetById(itemToSave.BlogId);
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            if (targetItem == null)
            {
                this.DataContext.Blogs.InsertOnSubmit(itemToSave);
            }

            if (_submitChanges == true)
            {
                this.SubmitChanges();
            }
        }
        /// <summary>
        /// Get all blogs found in teh database.
        /// </summary>
        /// <returns></returns>
        public PagedList<Blog> GetAll()
        {
            IQueryable<Blog> retVal = from foundItem in this.DataContext.Blogs select foundItem;
            return Pagination.ToPagedList(retVal);
        }
        /// <summary>
        /// Get a specific blog specified by the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Blog GetById(int id)
        {
            Blog retVal = null;

            try
            {
                retVal = (from foundItem in this.DataContext.Blogs where foundItem.BlogId == id select foundItem).Single();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;
        }
        /// <summary>
        /// Get a blog as specified by the name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Blog GetByName(string name)
        {
            Blog retVal = null;

            try
            {
                retVal = (from foundItem in this.DataContext.Blogs where foundItem.Name == name select foundItem).Single();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;
        }
        /// <summary>
        /// Get a blog specified by the site subfolder that contains it.
        /// </summary>
        /// <param name="subFolder"></param>
        /// <returns></returns>
        public Blog GetBySubFolder(string subFolder)
        {
            Blog retVal = null;

            try
            {
                retVal = (from foundItem in this.DataContext.Blogs where foundItem.SubFolder == subFolder select foundItem).Single();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;
        }
        /// <summary>
        /// Get all blogs that a user is associated with (i.e. ones that the user has security access specifations for it)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public PagedList<Blog> GetByUserId(int userId)
        {
            IQueryable<Blog> retVal = from foundItem in this.DataContext.Blogs 
                                      join userBlogs in this.DataContext.BlogUsers on foundItem.BlogId equals userBlogs.BlogId
                                      where userBlogs.UserId == userId
                                      select foundItem;
            return Pagination.ToPagedList(retVal);
        }
    }
}
