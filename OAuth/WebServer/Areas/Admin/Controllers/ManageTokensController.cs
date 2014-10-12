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
    /// Allow the admin tool to manage tokens
    /// </summary>
    [AdminAuthorizeAttribute(RequiredRoles = "Administrator")]
    public class ManageTokensController : VP.Digital.Security.OAuth.WebServer.Controllers.ControllerBase
    {
        /// <summary>
        /// Display a list of the tokens with applied filters
        /// </summary>
        /// <param name="userName">A username to search for</param>
        /// <param name="consumerKey">A consumer key to search for</param>
        /// <returns>A view</returns>
        public ActionResult Index(OAuthTokensModel model)
        {
            OAuthTokensModel retVal = new OAuthTokensModel();

            if(model == null)
            {
                model = new OAuthTokensModel();
            }

            if(model.StartDate == null || model.StartDate == DateTime.MinValue)
            {
                model.StartDate = DateTime.UtcNow.AddHours(-1);
            }

            if (model.EndDate == null || model.EndDate == DateTime.MinValue)
            {
                model.EndDate = DateTime.UtcNow;
            }

            retVal.UserName = model.UserName;

            if (!string.IsNullOrEmpty(model.UserName))
            {
                DigitalUserLogin tempUser = new DigitalUserLogin();
                tempUser.Email = model.UserName;
                retVal.Tokens = this.ServiceManager.TokenService.GetByUser(tempUser, model.StartDate, model.EndDate);
            }

            if (!string.IsNullOrEmpty(model.ConsumerKey))
            {
                retVal.Tokens = this.ServiceManager.TokenService.GetByConsumerKey(model.ConsumerKey, model.StartDate, model.EndDate);
            }

            retVal.UserName = model.UserName;
            retVal.ConsumerKey = model.ConsumerKey;
            retVal.StartDate = model.StartDate;
            retVal.EndDate = model.EndDate;

            return this.View(retVal);
        }
    }
}