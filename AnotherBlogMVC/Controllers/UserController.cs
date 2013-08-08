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

using AnotherBlog.Common.Utilities;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Core.Service;
using AnotherBlog.Core.Utilities;
using AnotherBlog.MVC.Models;

namespace AnotherBlog.MVC.Controllers
{
    public class UserController : PublicController
    {
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
                HttpCookie authCookie = this.Request.Cookies[cookieName];

                if (this.Response.Cookies[cookieName] != null)
                {
                    this.Response.Cookies[cookieName].Expires = DateTime.Now.AddDays(1);
                }
            }
            catch (Exception e)
            {
                this.Logger.Error(e.Message, e);
            }
        }

        public ActionResult Login(string blogSubFolder, string userName, string password, string loginAction, string currentPage)
        {
            UserModel model = (UserModel)this.InitializeDataModel(blogSubFolder, new UserModel());

            if (loginAction == "login")
            {
                model.CurrentUser = Services.Users.Login(userName, password);

                if (model.CurrentUser == null)
                {
                    this.EliminateUserCookie();
                    this.CurrentPrincipal = new SecurityPrincipal(Services.Users.GetDefaultUser());
                    model.CurrentUser = this.CurrentPrincipal.CurrentUser;
                    ViewData.ModelState.AddModelError("loginError", "Invalid login.");
                }
                else
                {
                    this.CurrentPrincipal = new SecurityPrincipal(model.CurrentUser, true);
                }

                this.EstablishCurrentUserCookie(this.CurrentPrincipal);
            }
            else
            {
                this.EliminateUserCookie();
                this.CurrentPrincipal = new SecurityPrincipal(Services.Users.GetDefaultUser());
                model.CurrentUser = this.CurrentPrincipal.CurrentUser;
            }

            if (currentPage == null)
            {
                currentPage = "/" + blogSubFolder + "/Home/Index";
            }

            return View("UserLogin", model);
        }

        [CustomAuthorization]
        public ActionResult Preferences(string blogSubFolder)
        {
            UserModel model = (UserModel)this.InitializeDataModel(blogSubFolder, new UserModel());
            model.ContentTitle = "My Account";
            model.CurrentUser = this.CurrentPrincipal.CurrentUser;
            return View(model);
        }

        [CustomAuthorization]
        public ActionResult SavePreferences(string blogSubFolder, string password, string email, string userAbout, string displayName)
        {
            UserModel model = (UserModel)this.InitializeDataModel(blogSubFolder, new UserModel());
            model.ContentTitle = "My Account";

            User userToSave = this.CurrentPrincipal.CurrentUser;

            userToSave.Password = password;
            userToSave.Email = email;

            model.CurrentUser = Services.Users.Save(userToSave.UserName, password, email, userToSave.UserId, userToSave.IsSiteAdministrator, userToSave.ApprovedCommenter, userToSave.IsActive, userAbout, displayName);

            return View("Preferences", model);
        }

        public ActionResult Register(string blogSubFolder, string registerAction, string userName, string password, string email, string userAbout, string displayName)
        {
            UserModel model = (UserModel)this.InitializeDataModel(blogSubFolder, new UserModel());

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
                    model.CurrentUser = Services.Users.Save(userName, password, email, 0, false, false, true, userAbout, displayName);

                    this.CurrentPrincipal = new SecurityPrincipal(model.CurrentUser, true);

                    this.EstablishCurrentUserCookie(this.CurrentPrincipal);

                    // Using redirect because redirec to action loses the preceeding blogSubFolder
                    if (blogSubFolder == "All")
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
            UserModel model = (UserModel)this.InitializeDataModel("", new UserModel());
            return View(model);
        }

        public ActionResult ForgotPassword(string blogSubFolder, string userEmail)
        {
            UserModel model = (UserModel)this.InitializeDataModel(blogSubFolder, new UserModel());
            Services.Users.SendPassword(userEmail, MvcApplication.emailConfig);
            return View("UserLogin", model);
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator)]
        public ActionResult ViewUserSocial(string blogSubFolder, string userId)
        {
            UserModel model = (UserModel)this.InitializeDataModel(blogSubFolder, new UserModel());

            int targetUser = int.Parse(userId);

            model.BlogList = Services.Blogs.GetAll();
            model.CurrentUser = Services.Users.GetById(targetUser);

            return View(model);
        }
    }
}
