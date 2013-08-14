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
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Security.Principal;
using System.Web.Security;

using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.Common.DataLayer.Entities;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.BusinessLayer.Service;
using AlwaysMoveForward.AnotherBlog.BusinessLayer.Utilities;
using AlwaysMoveForward.AnotherBlog.Web.Models;
using AlwaysMoveForward.AnotherBlog.Web.Code.Filters;

namespace AlwaysMoveForward.AnotherBlog.Web.Controllers
{
    public class UserController : PublicController
    {
        public UserModel InitializeUserModel()
        {
            UserModel retVal = new UserModel();
            retVal.Common = this.InitializeCommonModel();
            retVal.Common.Calendar = this.InitializeCalendarModel(retVal.Common.TargetMonth);
            return retVal;
        }

        public UserModel InitializeUserModel(String blogSubFolder)
        {
            UserModel retVal = new UserModel();
            retVal.Common = this.InitializeCommonModel(this.Services.BlogService.GetBySubFolder(blogSubFolder));
            retVal.Common.Calendar = this.InitializeCalendarModel(retVal.Common.TargetMonth);
            return retVal;
        }

        private void EstablishCurrentUserCookie(SecurityPrincipal currentPrincipal)
        {
            if (currentPrincipal != null && currentPrincipal.CurrentUser!=null)
            {
                // I'm not sure I like having the cookie here, but I'm having a problem passing
                // this user back to the view (even though it worked fine in my Edit method)
                FormsAuthenticationTicket authTicket =
                new FormsAuthenticationTicket(1, currentPrincipal.CurrentUser.UserName, DateTime.Now, DateTime.Now.AddMinutes(180), false, "");

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                this.HttpContext.Response.Cookies.Add(faCookie);

                this.CurrentPrincipal = currentPrincipal;
            }
        }

        private void EliminateUserCookie()
        {
            try
            {
                string cookieName = FormsAuthentication.FormsCookieName;
                HttpCookie authCookie = this.Response.Cookies[cookieName];

                if (authCookie != null)
                {
                    authCookie.Expires = DateTime.Now.AddDays(-1);
                }
            }
            catch (Exception e)
            {
                LogManager.GetLogger().Error(e);
            }
        }

        public ActionResult Login(string blogSubFolder, string userName, string password, string loginAction, string currentPage)
        {
            UserModel model = this.InitializeUserModel(blogSubFolder);

            if (loginAction == "login")
            {
                model.CurrentUser = Services.UserService.Login(userName, password);

                if (model.CurrentUser == null)
                {
                    this.EliminateUserCookie();
                    this.CurrentPrincipal = new SecurityPrincipal(Services.UserService.GetDefaultUser());
                    model.CurrentUser = this.CurrentPrincipal.CurrentUser;
                    ViewData.ModelState.AddModelError("loginError", "Invalid login.");
                }
                else
                {
                    this.CurrentPrincipal = new SecurityPrincipal(model.CurrentUser, true);
                    this.EstablishCurrentUserCookie(this.CurrentPrincipal);
                }
            }
            else
            {
                this.EliminateUserCookie();
                this.CurrentPrincipal = new SecurityPrincipal(Services.UserService.GetDefaultUser());
                model.CurrentUser = this.CurrentPrincipal.CurrentUser;
            }

            if (currentPage == null)
            {
                currentPage = "/" + blogSubFolder + "/Home/Index";
            }

            return View("UserLogin", blogSubFolder);
        }

        public JsonResult AjaxLogin(string blogSubFolder, string userName, string password, string loginAction)
        {
            AjaxLoginModel retVal = new AjaxLoginModel();

            if (loginAction == "login")
            {
                retVal.ProcessedLogin = true;

                User currentUser = Services.UserService.Login(userName, password);

                if (currentUser == null)
                {
                    this.EliminateUserCookie();
                    this.CurrentPrincipal = new SecurityPrincipal(Services.UserService.GetDefaultUser());
                    ViewData.ModelState.AddModelError("loginError", "Invalid login.");
                }
                else
                {
                    retVal.IsAuthorized = true;
                    this.CurrentPrincipal = new SecurityPrincipal(currentUser, true);
                    this.EstablishCurrentUserCookie(this.CurrentPrincipal);
                }
            }
            else
            {
                this.EliminateUserCookie();
                this.CurrentPrincipal = new SecurityPrincipal(Services.UserService.GetDefaultUser());
            }

            return Json(retVal);
        }

        [CustomAuthorization]
        public ActionResult Preferences(string blogSubFolder)
        {
            UserModel model = this.InitializeUserModel(blogSubFolder);
            model.Common.ContentTitle = "My Account";
            model.CurrentUser = this.CurrentPrincipal.CurrentUser;
            return View(model);
        }

        [CustomAuthorization]
        public ActionResult SavePreferences(string blogSubFolder, string password, string email, string userAbout, string displayName)
        {
            UserModel model = this.InitializeUserModel(blogSubFolder);
            model.Common.ContentTitle = "My Account";

            User userToSave = this.CurrentPrincipal.CurrentUser;

            userToSave.Password = password;
            userToSave.Email = email;

            model.CurrentUser = Services.UserService.Save(userToSave.UserName, password, email, userToSave.UserId, userToSave.IsSiteAdministrator, userToSave.ApprovedCommenter, userToSave.IsActive, userAbout, displayName);

            return View("Preferences", model);
        }

        public ActionResult Register(string blogSubFolder, string registerAction, string userName, string password, string email, string userAbout, string displayName)
        {
            UserModel model = this.InitializeUserModel(blogSubFolder);
            model.TargetBlog = Services.BlogService.GetBySubFolder(blogSubFolder);

            if (registerAction == "save")
            {
                if (userName == "")
                {
                    ModelState.AddModelError("userName", "Please enter a user name.");
                }

                if (password == "")
                {
                    ModelState.AddModelError("password", "Please enter a password.");
                }

                if (email == "")
                {
                    ModelState.AddModelError("email", "Please enter an email address.");
                }

                if(ModelState.IsValid)
                {
                    model.CurrentUser = Services.UserService.Save(userName, password, email, 0, false, false, true, userAbout, displayName);

                    this.CurrentPrincipal = new SecurityPrincipal(model.CurrentUser, true);

                    this.EstablishCurrentUserCookie(this.CurrentPrincipal);

                    // Using redirect because redirec to action loses the preceeding blogSubFolder
                    if (blogSubFolder == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Blog", new { blogSubFolder = blogSubFolder });
                    }
                }
            }

            return View(model);
        }

        public ActionResult BlogNavMenu()
        {
            UserModel model = this.InitializeUserModel();
            return View(model);
        }

        public ActionResult ForgotPassword(string blogSubFolder, string userEmail)
        {
            UserModel model = this.InitializeUserModel(blogSubFolder);
            Services.UserService.SendPassword(userEmail, MvcApplication.emailConfig);
            return View("UserLogin", model);
        }

        [CustomAuthorization(RequiredRoles = RoleType.SiteAdministrator + "," + RoleType.Administrator)]
        public ActionResult ViewUserSocial(string blogSubFolder, string userId)
        {
            UserModel model = this.InitializeUserModel(blogSubFolder);

            int targetUser = int.Parse(userId);
            model.CurrentUser = Services.UserService.GetById(targetUser);

            return View(model);
        }
    }
}
