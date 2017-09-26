using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.Web.Models;
using AlwaysMoveForward.OAuth.Web.Code.Filters;
using System.Web.Mvc;

namespace AlwaysMoveForward.OAuth.Web.Controllers.API
{
    public class RequestTokensController : BaseAPIController
    {
        [Route("api/Consumer/{consumerKey}/RequestTokens"), HttpGet()]
        [WebApiAuthorization(Roles = RoleType.Names.Administrator)]
        public PagedListModel<RequestToken> GetByConsumerKey(int? page, string consumerKey, DateTime? startDate, DateTime? endDate)
        {
            IList<RequestToken> retVal = new List<RequestToken>();

            DateTime searchStartDate = DateTime.UtcNow.AddDays(-1);

            if(startDate.HasValue)
            {
                searchStartDate = startDate.Value;
            }

            DateTime searchEndDate = DateTime.UtcNow;

            if(endDate.HasValue)
            {
                searchEndDate = endDate.Value;
            }

            int currentPageIndex = 0;

            if (page.HasValue)
            {
                currentPageIndex = page.Value - 1;
            }

            if (!string.IsNullOrEmpty(consumerKey))
            {
                retVal = this.Services.TokenService.GetByConsumerKey(consumerKey, searchStartDate, searchEndDate);
            }

            return new PagedListModel<RequestToken>(retVal, currentPageIndex);
        }

        [Route("api/User/{userName}/RequestTokens"), HttpGet()]
        [WebApiAuthorization(Roles = RoleType.Names.Administrator)]
        public PagedListModel<RequestToken> GetByUserName(int? page, string userName, DateTime? startDate, DateTime? endDate)
        {
            IList<RequestToken> retVal = new List<RequestToken>();

            DateTime searchStartDate = DateTime.UtcNow.AddDays(-1);

            if (startDate.HasValue)
            {
                searchStartDate = startDate.Value;
            }

            DateTime searchEndDate = DateTime.UtcNow;

            if (endDate.HasValue)
            {
                searchEndDate = endDate.Value;
            }

            int currentPageIndex = 0;

            if (page.HasValue)
            {
                currentPageIndex = page.Value - 1;
            }

            if (!string.IsNullOrEmpty(userName))
            {
                AMFUserLogin targetUser = this.Services.UserService.GetByEmail(userName);
                retVal = this.Services.TokenService.GetByUser(targetUser, searchStartDate, searchEndDate);
            }

            return new PagedListModel<RequestToken>(retVal, currentPageIndex);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([System.Web.Http.FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [System.Web.Http.FromBody]string value)
        {
        }

        [Route("api/RequestToken/{id}"), HttpDelete()]
        [WebApiAuthorization(Roles = RoleType.Names.Administrator)]
        public void Delete(int id)
        {
            this.Services.TokenService.Delete(id);
        }
    }
}