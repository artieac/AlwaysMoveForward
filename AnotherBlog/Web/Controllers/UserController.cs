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
using DevDefined.OAuth.Framework;
using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.OAuth.Contracts;
using AlwaysMoveForward.OAuth.Contracts.Configuration;
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
                new FormsAuthenticationTicket(1, currentPrincipal.CurrentUser.Id.ToString(), DateTime.Now, DateTime.Now.AddMinutes(180), false, string.Empty);

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

        private Realm GenerateRealm()
        {
            Realm retVal = new Realm();
            retVal.Area = "AlwaysMoveForward";
            retVal.Service = "Blog";
            return retVal;
        }

        public void AjaxLogin(string blogSubFolder, string loginAction)
        {
            AjaxLoginModel retVal = new AjaxLoginModel();

            if (loginAction == "login")
            {
                retVal.ProcessedLogin = true;

                EndpointConfiguration oauthEndpoints = EndpointConfiguration.GetInstance();
                OAuthKeyConfiguration keyConfiguration = OAuthKeyConfiguration.GetInstance();

                AlwaysMoveForward.OAuth.Client.RestSharp.OAuthClient oauthClient = new OAuth.Client.RestSharp.OAuthClient(oauthEndpoints.ServiceUri, keyConfiguration.ConsumerKey, keyConfiguration.ConsumerSecret, oauthEndpoints);
                IOAuthToken requestToken = oauthClient.GetRequestToken(this.GenerateRealm(), this.Request.Url.Scheme + "://" + this.Request.Url.Authority + "/User/OAuthCallback");

                if(requestToken != null)
                {
                    Session[requestToken.Token] = requestToken;

                    string authorizationUrl = oauthClient.GetUserAuthorizationUrl(requestToken);

                    this.Response.Redirect(authorizationUrl, false);
                } 
            }
            else
            {
                this.EliminateUserCookie();
                this.CurrentPrincipal = new SecurityPrincipal(Services.UserService.GetDefaultUser());
            }
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

        public ActionResult OAuthCallback(string oauth_token, string oauth_verifier)
        {
            string requestTokenString = Request[Parameters.OAuth_Token];
            string verifier = Request[Parameters.OAuth_Verifier];

            IOAuthToken storedRequestToken = (IOAuthToken)Session[requestTokenString];

            OAuthKeyConfiguration oauthConfiguration = OAuthKeyConfiguration.GetInstance();
            EndpointConfiguration endpointConfiguration = EndpointConfiguration.GetInstance();

            AlwaysMoveForward.OAuth.Client.RestSharp.OAuthClient oauthClient = new AlwaysMoveForward.OAuth.Client.RestSharp.OAuthClient("", oauthConfiguration.ConsumerKey, oauthConfiguration.ConsumerSecret, endpointConfiguration);

            if (string.IsNullOrEmpty(verifier))
            {
                throw new Exception("Expected a non-empty verifier value");
            }

            IOAuthToken accessToken;

            try
            {
                accessToken = oauthClient.ExchangeRequestTokenForAccessToken(storedRequestToken, verifier);

                AnotherBlogUser amfUser = this.Services.UserService.GetAnotherBlogUserFromAMFUser(accessToken);

                if (amfUser == null)
                {               
                    this.EliminateUserCookie();
                    this.CurrentPrincipal = new SecurityPrincipal(Services.UserService.GetDefaultUser());
                    ViewData.ModelState.AddModelError("loginError", "Invalid login.");
                }
                else
                {
                    this.CurrentPrincipal = new SecurityPrincipal(amfUser, true);
                    this.EstablishCurrentUserCookie(this.CurrentPrincipal);
                }
            }
            catch (OAuthException authEx)
            {
                LogManager.GetLogger().Error(authEx);
                Response.Redirect("AccessDenied.aspx");
            }

            return this.RedirectToAction("Index", "Home");
        }
    }
}
