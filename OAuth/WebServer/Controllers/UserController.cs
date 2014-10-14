using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DevDefined.OAuth.Framework;
using VP.Digital.Common.Entities;
using VP.Digital.Common.Security;
using VP.Digital.Common.Utilities.Logging;
using VP.Digital.Security.OAuth.Contracts;
using VP.Digital.Security.OAuth.Common.DomainModel;
using VP.Digital.Security.OAuth.WebServer.Models;
using VP.Digital.Security.OAuth.WebServer.Code;

namespace VP.Digital.Security.OAuth.WebServer.Controllers
{
    /// <summary>
    /// This controller allows a user to sign in and authorize an OAuth token
    /// </summary>
    public class UserController : ControllerBase
    {        
        /// <summary>
        /// Show the initial sign in page
        /// </summary>
        /// <param name="oauth_Token">The oauth token that this sign in is trying to authorize.</param>
        /// <returns>The index view</returns>
        public ActionResult Signin(string oauth_Token)
        {
            TokenModel model = new TokenModel() { Token = oauth_Token };

            Consumer consumer = this.ServiceManager.ConsumerService.GetByRequestToken(oauth_Token);

            if (consumer != null)
            {
                model.ConsumerName = consumer.Name;
            }

            return this.View("Signin", model);
        }

        /// <summary>
        /// Returns the Signup view for registration.
        /// </summary>
        /// <returns>The MVC View for a registaration</returns>
        public ActionResult SignUp(string oauthToken)
        {
            TokenModel model = new TokenModel() { Token = oauthToken };
            return this.View(model);
        }

        /// <summary>
        /// Setup the logged in user on the current thread and setup an auth cookie.
        /// </summary>
        /// <param name="digitalUser"></param>
        private void SetCurrentUser(DigitalUserLogin digitalUser)
        {
            this.CurrentPrincipal = new OAuthServerSecurityPrincipal(digitalUser);
            FormsAuthentication.SetAuthCookie(digitalUser.Id.ToString(), false);
        }

        /// <summary>
        /// Register a new user with the site
        /// </summary>
        /// <param name="registerModel">The incoming parameters used to register the user</param>
        /// <returns>The Registered user view</returns>
        public ActionResult Register(RegisterModel registerModel)
        {
            DigitalUserLogin registeredUser = null;

            if (registerModel != null)
            {
                registeredUser = this.ServiceManager.UserService.GetByEmail(registerModel.UserEmail);

                if (registeredUser == null)
                {
                    registeredUser = this.ServiceManager.UserService.Register(registerModel.UserEmail, registerModel.Password, registerModel.PasswordHint, registerModel.FirstName, registerModel.LastName);
                }
                else
                {
                    TokenModel model = new TokenModel() { Token = registerModel.OAuthToken };
                    return this.View("Signin", model);
                }
            }

            if (registeredUser != null)
            {
                this.SetCurrentUser(registeredUser);
                TokenModel model = new TokenModel() { Token = registerModel.OAuthToken };
                return this.View(model);
            }
            else
            {
                return this.View("SignUp", new { oauthToken = registerModel.OAuthToken });
            }
        }

        /// <summary>
        /// Logs a user into the system and authorizes a request token
        /// </summary>
        /// <param name="userName">The username to log in</param>
        /// <param name="password">The users password to sign in</param>
        /// <param name="oauthToken">The oauth token to authorize</param>
        /// <returns>The Grant Access view or the sign in screen again</returns>
        [ValidateAntiForgeryToken]
        public ActionResult ProcessSignin(string userName, string password, string oauthToken)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                // yes actually look up the user, but for now
                DigitalUserLogin digitalUser = this.ServiceManager.UserService.LogonUser(userName, password, this.Request.UserHostAddress);

                if (digitalUser != null)
                {
                    this.SetCurrentUser(digitalUser);

                    if (!string.IsNullOrEmpty(oauthToken))
                    {
                        return this.GrantAccess(oauthToken);
                    }
                    else
                    {
                        if(this.CurrentPrincipal.IsInRole(OAuthRoles.Administrator.ToString()))
                        {
                            return this.Redirect("/Admin/Management/Index");
                        }
                        else 
                        {
                            return this.View();
                        }
                    }
                }               
            }

            return this.Signin(oauthToken);
        }

        /// <summary>
        /// Prompt the user to request or deny access to the oauth token
        /// </summary>
        /// <param name="oauthToken">The request token</param>
        /// <returns>The view that gives the user the option to grant or deny access</returns>
        [Authorize]
        public ActionResult GrantAccess(string oauthToken)
        {
            TokenModel model = new TokenModel() { Token = oauthToken };
            return this.View("GrantAccess", model);              
        }

        /// <summary>
        /// Approves access for the request token
        /// </summary>
        /// <param name="oauthToken">The request token</param>
        [Authorize]
        public void ApproveAccess(string oauthToken)
        {
            try
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                DigitalUserLogin currentUser = this.ServiceManager.UserService.GetUserById(int.Parse(ticket.Name));

                if (currentUser != null)
                {
                    this.CurrentPrincipal = new OAuthServerSecurityPrincipal(currentUser);
                    this.RedirectToClient(this.ServiceManager.TokenService.CreateVerifierAndAssociateUserInfo(oauthToken, currentUser), true);
                }
            }
            catch (Exception e)
            {
                LogManager.GetLogger(this.GetType()).Error(e);
            }
        }

        /// <summary>
        /// Denies access to the request token
        /// </summary>
        /// <param name="oauthToken">the request token</param>
        [Authorize]
        public void DenyAccess(string oauthToken)
        {
            this.RedirectToClient(this.ServiceManager.TokenService.DenyRequestToken(oauthToken), false);
        }

        /// <summary>
        /// Redirect the caller to the oauth call back once the user has been approved/denied
        /// </summary>
        /// <param name="requestToken">The oauth token</param>
        /// <param name="granted">Whether or not access ahs been granted</param>
        void RedirectToClient(RequestToken requestToken, bool granted)
        {
            string consumerKey = string.Empty;
            string callBackUrl = string.Empty;
            string verifier = string.Empty;
            string oauthToken = string.Empty;

            if (requestToken != null)
            {
                consumerKey = requestToken.ConsumerKey;
                oauthToken = requestToken.Token;

                if (requestToken.RequestTokenAuthorization != null)
                {
                    verifier = requestToken.RequestTokenAuthorization.VerifierCode;
                    callBackUrl = requestToken.GenerateCallBackUrl();
                }
            }

            if (!string.IsNullOrEmpty(callBackUrl))
            {
                this.Response.Redirect(callBackUrl, true);
            }
            else
            {
                if (granted)
                {
                    string successMessage = string.Format(
                      "You have been successfully granted Access, To complete the process, please provide <I>{0}</I> with this verification code: <B>{1}</B>",
                      consumerKey, HttpUtility.HtmlEncode(verifier));

                    this.Response.Write(successMessage);
                }
                else
                {
                    this.Response.Write("Denied");
                }

                this.Response.End();
            }
        }

        /// <summary>
        /// this action returns the partial view to show the password hint
        /// </summary>
        /// <param name="emailAddress">The email address of the user</param>
        /// <returns>An MVC view</returns>
        public ActionResult PasswordHint(string emailAddress)
        {
            PasswordHintModel retVal = new PasswordHintModel();

            DigitalUserLogin targetUser = this.ServiceManager.UserService.GetByEmail(emailAddress);

            if (targetUser != null)
            {
                retVal.PasswordHint = targetUser.PasswordHint;
            }

            return this.View(retVal);
        }

        /// <summary>
        /// this action returns the partial view to show the password hint
        /// </summary>
        /// <returns>An MVC view</returns>
        [OAuthSignatureValidation]
        public JsonResult Details()
        {
            DigitalUser retVal = new DigitalUser();

            if (this.CurrentPrincipal.User != null)
            {
                retVal.Email = this.CurrentPrincipal.User.Email;
                retVal.FirstName = this.CurrentPrincipal.User.FirstName;
                retVal.LastName = this.CurrentPrincipal.User.LastName;
                retVal.Id = this.CurrentPrincipal.User.Id;
            }

            return this.Json(retVal, JsonRequestBehavior.AllowGet);
        }
    }
}
