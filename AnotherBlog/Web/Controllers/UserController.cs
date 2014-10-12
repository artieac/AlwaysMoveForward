﻿/**
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
using AlwaysMoveForward.Common.DomainModel;
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

        public UserModel InitializeUserModel(string blogSubFolder)
        {
            UserModel retVal = new UserModel();
            retVal.Common = this.InitializeCommonModel(this.Services.BlogService.GetBySubFolder(blogSubFolder));
            retVal.Common.Calendar = this.InitializeCalendarModel(retVal.Common.TargetMonth);
            return retVal;
        }

        private void EstablishCurrentUserCookie(SecurityPrincipal currentPrincipal)
        {
            if (currentPrincipal != null && currentPrincipal.CurrentUser != null)
            {
                // I'm not sure I like having the cookie here, but I'm having a problem passing
                // this user back to the view (even though it worked fine in my Edit method)
                FormsAuthenticationTicket authTicket =
                new FormsAuthenticationTicket(1, currentPrincipal.CurrentUser.AMFUser.Email, DateTime.Now, DateTime.Now.AddMinutes(180), false, string.Empty);

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                this.HttpContext.Response.Cookies.Add(authenticationCookie);

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

        public JsonResult AjaxLogin(string blogSubFolder, string loginAction)
        {
            AjaxLoginModel retVal = new AjaxLoginModel();

            if (loginAction == "login")
            {
                retVal.ProcessedLogin = true;

                AnotherBlogUser currentUser = Services.UserService.Login(userName, password);

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

            return this.Json(retVal);
        }

        [CustomAuthorization]
        public ActionResult Preferences(string blogSubFolder)
        {
            UserModel model = this.InitializeUserModel(blogSubFolder);
            model.Common.ContentTitle = "My Account";
            model.CurrentUser = this.CurrentPrincipal.CurrentUser;
            return this.View(model);
        }

        [CustomAuthorization]
        public ActionResult SavePreferences(string blogSubFolder, string userAbout)
        {
            UserModel model = this.InitializeUserModel(blogSubFolder);
            model.Common.ContentTitle = "My Account";

            AnotherBlogUser userToSave = this.CurrentPrincipal.CurrentUser;

            model.CurrentUser = Services.UserService.Save(userToSave.Id, userToSave.IsSiteAdministrator, userToSave.ApprovedCommenter, userAbout);

            return this.View("Preferences", model);
        }

        public ActionResult BlogNavMenu()
        {
            UserModel model = this.InitializeUserModel();
            return this.View(model);
        }

        [CustomAuthorization(RequiredRoles = RoleType.Names.SiteAdministrator + "," + RoleType.Names.Administrator)]
        public ActionResult ViewUserSocial(string blogSubFolder, string userId)
        {
            UserModel model = this.InitializeUserModel(blogSubFolder);

            int targetUser = int.Parse(userId);
            model.CurrentUser = Services.UserService.GetById(targetUser);

            return this.View(model);
        }
    }
}
