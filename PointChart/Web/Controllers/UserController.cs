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
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.OAuth.Client;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.BusinessLayer.Utilities;
using AlwaysMoveForward.PointChart.Web.Code.Filters;
using AlwaysMoveForward.PointChart.Web.Models;

namespace AlwaysMoveForward.PointChart.Web.Controllers
{
    public class UserController : BaseController
    {
        public UserModel InitializeUserModel()
        {
            UserModel retVal = new UserModel();
            return retVal;
        }

        private void EstablishCurrentUserCookie(SecurityPrincipal currentPrincipal)
        {
            if (currentPrincipal != null && currentPrincipal.CurrentUser != null)
            {
                // I'm not sure I like having the cookie here, but I'm having a problem passing
                // this user back to the view (even though it worked fine in my Edit method)
                FormsAuthenticationTicket authTicket =
                new FormsAuthenticationTicket(1, currentPrincipal.CurrentUser.Id.ToString(), DateTime.Now, DateTime.Now.AddMinutes(18000), false, string.Empty);

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

        public void Login()
        {
            IOAuthToken requestToken = this.Services.OAuthClient.GetRequestToken(this.GenerateRealm(), this.Request.Url.Scheme + "://" + this.Request.Url.Authority + "/User/OAuthCallback");

            if (requestToken != null)
            {
                Session[requestToken.Token] = requestToken;

                string authorizationUrl = this.Services.OAuthClient.GetUserAuthorizationUrl(requestToken);

                this.Response.Redirect(authorizationUrl, false);
            }
        }

        public void Logout()
        {
            this.EliminateUserCookie();
            this.CurrentPrincipal = new SecurityPrincipal(Services.UserService.GetDefaultUser());
        }

        public ActionResult OAuthCallback(string oauth_token, string oauth_verifier)
        {
            string requestTokenString = Request[OAuth.Client.Constants.TokenParameter];
            string verifier = Request[OAuth.Client.Constants.VerifierCodeParameter];

            IOAuthToken storedRequestToken = (IOAuthToken)Session[requestTokenString];

            if (string.IsNullOrEmpty(verifier))
            {
                throw new Exception("Expected a non-empty verifier value");
            }

            IOAuthToken accessToken;

            try
            {
                accessToken = this.Services.OAuthClient.ExchangeRequestTokenForAccessToken(storedRequestToken, verifier);

                PointChartUser amfUser = this.Services.UserService.GetFromAMFUser(accessToken);

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
            catch (Exception authEx)
            {
                LogManager.GetLogger().Error(authEx);
                Response.Redirect("AccessDenied.aspx");
            }

            return this.RedirectToAction("Index", "Home");
        }
 
    }
}
