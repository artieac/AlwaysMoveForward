using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TheOffWing.AnotherBlog.Core.SystemExtensions;

namespace TheOffWing.AnotherBlog.Core.Entity
{
    /// <summary>
    /// This class contains all the code to extract User data from the repository using LINQ
    /// </summary>
    /// <param name="dataContext"></param>
    public class UserGateway : GatewayBase
    {
        public UserGateway(DataContextManager dataContext)
            : base(dataContext)
        {

        }
        /// <summary>
        /// Save the specified user.  First see if its in the database (if so the update happens naturaly via LINQ), if not
        /// insert it into the LINQ context.
        /// </summary>
        /// <param name="userToSave"></param>
        /// <param name="_submitChanges"></param>
        public void Save(User userToSave, bool _submitChanges)
        {
            User targetUser = null;

            try
            {
                targetUser = (from foundItem in this.DataContext.Users where foundItem.UserId == userToSave.UserId select foundItem).Single();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            if (targetUser == null)
            {
                this.DataContext.Users.InsertOnSubmit(userToSave);
            }

            if (_submitChanges == true)
            {
                this.SubmitChanges();
            }
        }
        /// <summary>
        /// Get all users in the system.
        /// </summary>
        /// <returns></returns>
        public PagedList<User> GetAll(int currentPageIndex, int pageSize)
        {
            IQueryable<User> retVal = from foundItem in this.DataContext.Users select foundItem;

            return Pagination.ToPagedList(retVal, currentPageIndex, pageSize);
        }
        /// <summary>
        /// Get a specific by their user name.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User GetByUserName(string userName)
        {
            User retVal = null;

            try
            {
                retVal = (from foundItem in this.DataContext.Users where foundItem.UserName == userName select foundItem).Single();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;
        }
        /// <summary>
        /// This method is used by the login.  If no match is found then something doesn't jibe in the login attempt.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User GetByUserNameAndPassword(string userName, string password)
        {
            User retVal = null;

            try
            {
                retVal = (from foundItem in this.DataContext.Users where foundItem.UserName == userName && foundItem.Password == password select foundItem).Single();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;
        }
        /// <summary>
        /// Get a specific user by Id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetById(int userId)
        {
            User retVal = null;

            try
            {
                retVal = (from foundItem in this.DataContext.Users where foundItem.UserId == userId select foundItem).Single();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;
        }
        /// <summary>
        /// Get a specific user by email
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public User GetByEmail(string userEmail)
        {
            User retVal = null;

            try
            {
                retVal = (from foundItem in this.DataContext.Users where foundItem.Email == userEmail select foundItem).Single();
            }
            catch (Exception e)
            {
                this.Logger.Error(e.Message, e);
            }

            return retVal;
        }
        /// <summary>
        /// Get all users that have the Administrator or Blogger role for the specific blog.
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public PagedList<User> GetBlogWriters(int blogId)
        {
            IQueryable<User> retVal = from foundItem in this.DataContext.Users 
                                      join userBlog in this.DataContext.BlogUsers on foundItem.UserId equals userBlog.UserId
                                      join userRoles in this.DataContext.Roles on userBlog.RoleId equals userRoles.RoleId
                                      where (userRoles.Name=="Administrator" || userRoles.Name=="Blogger") &&
                                          userBlog.BlogId == blogId && userBlog.RoleId == userRoles.RoleId
                                      select foundItem;
            return Pagination.ToPagedList(retVal);
        }
    }
}
