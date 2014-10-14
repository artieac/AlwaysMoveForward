using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VP.Digital.Common.Entities;
using VP.Digital.Security.OAuth.Common.DomainModel;
using VP.Digital.Security.OAuth.BusinessLayer.Services;
using VP.Digital.Security.OAuth.WebServer.Code;
using VP.Digital.Security.OAuth.WebServer.Areas.Admin.Models;

namespace VP.Digital.Security.OAuth.WebServer.Areas.Admin.Controllers
{
    /// <summary>
    /// Manage the users int he system
    /// </summary>
    [AdminAuthorizeAttribute(RequiredRoles = "Administrator")]
    public class ManageUsersController : VP.Digital.Security.OAuth.WebServer.Controllers.ControllerBase
    {
        /// <summary>
        /// Displays a list of users
        /// </summary>
        /// <returns>A view</returns>
        public ActionResult Index()
        {
            IList<DigitalUserLogin> users = this.ServiceManager.UserService.GetAll();
            return this.View(users);
        }

        /// <summary>
        /// Display a user to edit
        /// </summary>
        /// <param name="id">The user id</param>
        /// <returns>A view</returns>
        public ActionResult Edit(int id)
        {
            DigitalUserLogin retVal = this.ServiceManager.UserService.GetUserById(id);
            return this.View(retVal);
        }

        /// <summary>
        /// Save changes to a user
        /// </summary>
        /// <param name="user">The user to save</param>
        /// <returns>A view</returns>
        public ActionResult Save(DigitalUserLogin user)
        {
            if (user != null)
            {
                using (this.ServiceManager.UnitOfWork.BeginTransaction())
                {
                    this.ServiceManager.UserService.Update(user);
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
        public ActionResult LoginHistory(string userName)
        {
            LoginHistoryModel retVal = new LoginHistoryModel();

            retVal.UserName = userName;

            if(!string.IsNullOrEmpty(userName))
            {
                retVal.LoginHistory = this.ServiceManager.UserService.GetLoginHistory(userName);
            }

            return this.View(retVal);
        }
    }
}