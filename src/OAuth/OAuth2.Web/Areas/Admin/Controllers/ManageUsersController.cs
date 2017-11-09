using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.OAuth2.Common.Utilities;
using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using AlwaysMoveForward.OAuth2.Web.Areas.Admin.Models;
using AlwaysMoveForward.OAuth2.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace AlwaysMoveForward.OAuth2.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Manage the users int he system
    /// </summary>
    [Area("Admin")]
    [Route("admin/[controller]/[action]")]
    [Authorize(Roles = RoleType.Names.Administrator)]
    public class ManageUsersController : AlwaysMoveForward.OAuth2.Web.Controllers.AMFControllerBase
    {
        public ManageUsersController(ServiceManagerBuilder serviceManagerBuilder,
                                     ILoggerFactory loggerFactory) 
                                     : base(serviceManagerBuilder, loggerFactory.CreateLogger<ManageUsersController>()) { }


        /// <summary>
        /// Displays a list of users
        /// </summary>
        /// <returns>A view</returns>
        public ActionResult Index(int? page)
        {
            int currentPageIndex = 0;

            if(page.HasValue)
            {
                currentPageIndex = page.Value - 1;
            }

            IPagedList<AMFUserLogin> users = new PagedList<AMFUserLogin>(this.ServiceManager.UserService.GetAll(), currentPageIndex, PagedListModel<int>.PageSize);
            return this.View(users);
        }

        /// <summary>
        /// Display a user to edit
        /// </summary>
        /// <param name="id">The user id</param>
        /// <returns>A view</returns>
        public ActionResult Edit(int id)
        {
            AMFUserLogin retVal = this.ServiceManager.UserService.GetUserById(id);
            return this.View(retVal);
        }

        /// <summary>
        /// Save changes to a user
        /// </summary>
        /// <param name="user">The user to save</param>
        /// <returns>A view</returns>
        public ActionResult Save(AMFUserLogin user)
        {
            if (user != null)
            {
                using (this.ServiceManager.UnitOfWork.BeginTransaction())
                {
                    this.ServiceManager.UserService.Update(user.Id, user.FirstName, user.LastName, user.UserStatus, user.Role);
                    this.ServiceManager.UnitOfWork.EndTransaction(true);
                }
            }

            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// Dislay teh login history for a user
        /// </summary>
        /// <param name="userName">The users name</param>
        /// <returns>A view</returns>
        public ActionResult LoginHistory(string userName, int? page)
        {
            LoginHistoryModel retVal = new LoginHistoryModel();

            retVal.UserName = userName;

            if(!string.IsNullOrEmpty(userName))
            {
                int currentPageIndex = 0;

                if(page.HasValue)
                {
                    currentPageIndex = page.Value - 1;
                }

                retVal.LoginHistory = new PagedList<LoginAttempt>(this.ServiceManager.UserService.GetLoginHistory(userName), currentPageIndex,PagedListModel<int>.PageSize);
            }

            return this.View(retVal);
        }
    }
}