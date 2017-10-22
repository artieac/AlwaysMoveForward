using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlwaysMoveForward.OAuth2.Common.Configuration;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.OAuth2.Common.Security;
using AlwaysMoveForward.OAuth2.Common.Utilities;
using AlwaysMoveForward.OAuth2.Web.Models;
using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using System;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using IdentityServer4.Services;
using IdentityServer4;
using AlwaysMoveForward.OAuth2.Web.Models.Account;
using System.Security.Claims;
using AlwaysMoveForward.OAuth2.Web.Code;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;

namespace AlwaysMoveForward.OAuth2.Web.Controllers
{
    /// <summary>
    /// This controller allows a user to sign in and authorize an OAuth token
    /// </summary>
    public class AccountController : AMFControllerBase
    {
        private readonly IIdentityServerInteractionService _idsInteractionService;
        private readonly SignInManager<AMFUserLogin> _signInManager;
        private readonly UserManager<AMFUserLogin> _userManager;
        private readonly ILoggerFactory _loggerFactory;

        public AccountController(ServiceManagerBuilder serviceManagerBuilder,
                                SignInManager<AMFUserLogin> signInManager,
                                UserManager<AMFUserLogin> userManager,
                                IIdentityServerInteractionService interaction,
                                ILoggerFactory loggerFactory) : base(serviceManagerBuilder)
        {
            this._idsInteractionService = interaction;
            this._signInManager = signInManager;
            this._userManager = userManager;

            this.Logger = loggerFactory.CreateLogger<AccountController>();
        }

        public ILogger Logger { get; private set; }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }


        /// <summary>
        /// Show the initial sign in page
        /// </summary>
        /// <param name="returnUrl">The url to return to after login.</param>
        /// <returns>The index view</returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl, string consumerName)
        {
            LoginModel retVal = new LoginModel();
            retVal.ReturnUrl = returnUrl;
            retVal.ConsumerName = consumerName;

            return View(retVal);
        }

        /// <summary>
        /// Logs a user into the system and authorizes a request token
        /// </summary>
        /// <param name="userName">The username to log in</param>
        /// <param name="password">The users password to sign in</param>
        /// <param name="oauthToken">The oauth token to authorize</param>
        /// <returns>The Grant Access view or the sign in screen again</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ProcessLogin(ProcessLoginInput input)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(input.UserName, input.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {                    
                    this.Logger.LogInformation(1, "User logged in.");
                    AMFUserLogin userLogin = this.ServiceManager.UserService.GetByEmail(input.UserName);
                    await _signInManager.SignInAsync(userLogin, isPersistent: false);

                    String foo = this.Request.Query["redirect_uri"];

                    if (String.IsNullOrEmpty(input.ReturnUrl) == true && userLogin.IsInRole(RoleType.Names.Administrator))
                    {
                        return this.Redirect("/Admin/Management/Index");
                    }
                    else
                    {
                        return RedirectToLocal(input.ReturnUrl);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", DefaultUserOptions.InvalidCredentialsErrorMessage);
            LoginModel loginModel = new LoginModel() { ReturnUrl = input.ReturnUrl };
            return this.View("Login", loginModel);
        }

        /// <summary>
        /// Returns the Signup view for registration.
        /// </summary>
        /// <returns>The MVC View for a registaration</returns>
        [AllowAnonymous]
        public ActionResult Register(string returnUrl)
        {
            RegisterModel retVal = new RegisterModel();
            retVal.ReturnUrl = returnUrl;
            return this.View(retVal);
        }


        /// <summary>
        /// Register a new user with the site
        /// </summary>
        /// <param name="registerModel">The incoming parameters used to register the user</param>
        /// <returns>The Registered user view</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessRegister(ProcessRegisterInput registerModel)
        {
            AMFUserLogin registerUser = null;

            if (ModelState.IsValid)
            {
                registerUser = this.ServiceManager.UserService.GetByEmail(registerModel.UserEmail);

                if (registerUser == null)
                {
                    registerUser = new AMFUserLogin();
                    registerUser.Email = registerModel.UserEmail;
                    registerUser.FirstName = registerModel.FirstName;
                    registerUser.LastName = registerModel.LastName;
                    registerUser.Role = RoleType.Id.User;
                    registerUser.UserStatus = UserStatus.Active;
                }

                var result = await _userManager.CreateAsync(registerUser, registerModel.Password);

                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    //    $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
                    await _signInManager.SignInAsync(registerUser, isPersistent: false);
//                    _logger.LogInformation(3, "User created a new account with password.");
                    return RedirectToLocal(registerModel.ReturnUrl);
                }
                AddErrors(result);
            }

            if (registerUser != null)
            {
                LoginModel model = new LoginModel() { ReturnUrl = registerModel.ReturnUrl };
                return this.View("Login", model);
            }
            else
            {
                EditModel model = new EditModel(registerUser);
                return this.View("Register", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            LoginModel retVal = new LoginModel();
            return this.View("Login", retVal);
        }

        /// <summary>
        /// this action returns the partial view to show the password hint
        /// </summary>
        /// <param name="emailAddress">The email address of the user</param>
        /// <returns>An MVC view</returns>
        public ActionResult PasswordHint(string emailAddress)
        {
            PasswordHintModel retVal = new PasswordHintModel();

            AMFUserLogin targetUser = this.ServiceManager.UserService.GetByEmail(emailAddress);

            if (targetUser != null)
            {
                retVal.PasswordHint = targetUser.PasswordHint;
            }

            return this.View(retVal);
        }

        public ActionResult ForgotPassword(string returnUrl)
        {
            LoginModel model = new LoginModel() { ReturnUrl = returnUrl };
            return this.View(model);
        }

        public ActionResult ResetPassword(string returnUrl, string userEmail, string resetToken)
        {
            LoginModel model = new LoginModel() { ReturnUrl = returnUrl };
            this.ServiceManager.UserService.ResetPassword(userEmail, EmailConfiguration.GetInstance());
            return this.View("Login", model);
        }

        /// <summary>
        /// Register a new user with the site
        /// </summary>
        /// <param name="editModel">The incoming parameters used to register the user</param>
        /// <returns>The Registered user view</returns>
        public ActionResult Edit(EditModel editModel)
        {
            AMFUserLogin registeredUser = null;

            if (editModel != null)
            {
                registeredUser = this.ServiceManager.UserService.Update(editModel.Id, editModel.FirstName, editModel.LastName);
               
                if(registeredUser != null)
                {
//                    registeredUser.UpdatePassword(editModel.Password);
                    return this.View("ProcessSignin", registeredUser);
                }
                else
                {
                    return this.View("Login");
                }
            }
            else
            {
                return this.View("Login");
            }
        }

        /// <summary>
        /// this action returns the partial view to show the password hint
        /// </summary>
        /// <returns>An MVC view</returns>
 //       public JsonResult GetByEmail(string emailAddress)
 //       {
 //           User retVal = new User();

//            if (this.CurrentPrincipal.User != null)
//            {
//                retVal = this.ServiceManager.UserService.GetByEmail(emailAddress);
//            }

//            return this.Json(retVal, JsonRequestBehavior.AllowGet);
 //       }
    }
}
