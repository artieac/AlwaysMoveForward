using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.Web.Models.API;

namespace AlwaysMoveForward.OAuth.Web.Controllers.API
{
    public class RequestTokensController : BaseAPIController
    {
        [Route("api/Consumer/{consumerKey}/RequestTokens"), HttpGet()]
        public IPagedList<RequestToken> GetByConsumerKey(int? page, string consumerKey, DateTime? startDate, DateTime? endDate)
        {
            IPagedList<RequestToken> retVal = new PagedList<RequestToken>();

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
                retVal = new PagedList<RequestToken>(this.Services.TokenService.GetByConsumerKey(consumerKey, searchStartDate, searchEndDate), currentPageIndex, AlwaysMoveForward.OAuth.Web.Code.Constants.PageSize);
            }

            return retVal;
        }

        [Route("api/User/{userName}/RequestTokens"), HttpGet()]
        public IPagedList<RequestToken> GetByUserName(int? page, string userName, DateTime? startDate, DateTime? endDate)
        {
            IPagedList<RequestToken> retVal = new PagedList<RequestToken>();

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
                retVal = new PagedList<RequestToken>(this.Services.TokenService.GetByUser(targetUser, searchStartDate, searchEndDate), currentPageIndex, AlwaysMoveForward.OAuth.Web.Code.Constants.PageSize);
            }

            return retVal;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}