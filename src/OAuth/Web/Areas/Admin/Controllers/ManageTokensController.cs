using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.BusinessLayer.Services;
using AlwaysMoveForward.OAuth.Web.Code.Filters;
using AlwaysMoveForward.OAuth.Web.Areas.Admin.Models;

namespace AlwaysMoveForward.OAuth.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Allow the admin tool to manage tokens
    /// </summary>
    [MVCAuthorization(Roles = "Administrator")]
    public class ManageTokensController : AlwaysMoveForward.OAuth.Web.Controllers.ControllerBase
    {
        /// <summary>
        /// Display a list of the tokens with applied filters
        /// </summary>
        /// <param name="userName">A username to search for</param>
        /// <param name="consumerKey">A consumer key to search for</param>
        /// <returns>A view</returns>
        public ActionResult Index(OAuthTokensModel model, int? page)
        {
            OAuthTokensModel retVal = new OAuthTokensModel();

            if(model == null)
            {
                model = new OAuthTokensModel();
            }

            if(model.StartDate == null || model.StartDate == DateTime.MinValue)
            {
                model.StartDate = DateTime.UtcNow.AddDays(-1);
            }

            if (model.EndDate == null || model.EndDate == DateTime.MinValue)
            {
                model.EndDate = DateTime.UtcNow;
            }

            retVal.UserName = model.UserName;

            int currentPageIndex = 0;

            if(page.HasValue)
            {
                currentPageIndex = page.Value - 1;
            }

            if (!string.IsNullOrEmpty(model.UserName))
            {
                AMFUserLogin tempUser = new AMFUserLogin();
                tempUser.Email = model.UserName;
                retVal.Tokens = new PagedList<RequestToken>(this.ServiceManager.TokenService.GetByUser(tempUser, model.StartDate, model.EndDate), currentPageIndex, AlwaysMoveForward.OAuth.Web.Models.PagedListModel<int>.PageSize);
            }

            if (!string.IsNullOrEmpty(model.ConsumerKey))
            {
                retVal.Tokens = new PagedList<RequestToken>(this.ServiceManager.TokenService.GetByConsumerKey(model.ConsumerKey, model.StartDate, model.EndDate), currentPageIndex, AlwaysMoveForward.OAuth.Web.Models.PagedListModel<int>.PageSize);
            }

            retVal.UserName = model.UserName;
            retVal.ConsumerKey = model.ConsumerKey;
            retVal.StartDate = model.StartDate;
            retVal.EndDate = model.EndDate;

            return this.View(retVal);
        }
    }
}