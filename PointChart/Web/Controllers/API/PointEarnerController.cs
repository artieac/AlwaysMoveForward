using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.OAuth.Client;
using AlwaysMoveForward.PointChart.Web.Code.Filters;
using AlwaysMoveForward.PointChart.Web.Models.API;

namespace AlwaysMoveForward.PointChart.Web.Controllers.API
{
    public class PointEarnerController : BaseAPIController
    {
        [Route("api/PointEarners"), HttpGet()]
        [WebAPIAuthorization]
        public IList<PointChartUser> Get()
        {
            IList<PointChartUser> retVal = new List<PointChartUser>();

            if(this.CurrentPrincipal!=null)
            {
                if(this.CurrentPrincipal.CurrentUser != null)
                {
                    retVal = this.CurrentPrincipal.CurrentUser.PointEarners;
                }
            }

            return retVal;
        }

        [WebAPIAuthorization]
        public PointChartUser Get(string emailAddress)
        {
            DefaultOAuthToken accessToken = new DefaultOAuthToken();
            accessToken.Token = ((IRemoteOAuthUser)this.CurrentPrincipal.CurrentUser).AccessToken;
            accessToken.Secret = ((IRemoteOAuthUser)this.CurrentPrincipal.CurrentUser).AccessTokenSecret;
            return this.Services.UserService.FindByEmail(emailAddress, accessToken);
        }

        // POST api/<controller>
        [WebAPIAuthorization]
        public void Post([FromBody]PointEarnerInput pointEarnerData)
        {
            if(pointEarnerData != null)
            {
                this.Services.UserService.AddPointEarner(pointEarnerData.PointEarnerId, this.CurrentPrincipal.CurrentUser);
            }
        }

        // PUT api/<controller>/5
        [WebAPIAuthorization]
        public void Put(int id, [FromBody]PointEarnerInput pointEarnerData)
        {
            if(pointEarnerData != null)
            {

            }
        }

        // DELETE api/<controller>/5
        [WebAPIAuthorization]
        public void Delete(int id)
        {
        }
    }
}