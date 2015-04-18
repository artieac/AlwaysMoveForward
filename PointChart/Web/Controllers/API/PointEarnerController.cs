using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        public IList<PointChartUserModel> Get()
        {
            IList<PointChartUserModel> retVal = new List<PointChartUserModel>();

            if(this.CurrentPrincipal!=null)
            {
                if(this.CurrentPrincipal.CurrentUser != null)
                {
                    for (int i = 0; i < this.CurrentPrincipal.CurrentUser.PointEarners.Count; i++)
                    {
                        retVal.Add(new PointChartUserModel(this.CurrentPrincipal.CurrentUser.PointEarners[i]));
                    }
                }
            }

            return retVal;
        }

        [WebAPIAuthorization]
        public PointChartUserModel Get(string emailAddress)
        {
            DefaultOAuthToken accessToken = new DefaultOAuthToken();
            accessToken.Token = this.CurrentPrincipal.CurrentUser.AccessToken;
            accessToken.Secret = this.CurrentPrincipal.CurrentUser.AccessTokenSecret;
            return new PointChartUserModel(this.Services.UserService.FindByEmail(emailAddress, accessToken));
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