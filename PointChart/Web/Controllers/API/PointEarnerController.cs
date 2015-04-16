﻿using System;
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
        public IList<PointChartUser> Get()
        {
            return new List<PointChartUser>();
        }

        [Route("api/PointEarner"), HttpGet()]
        [WebAPIAuthorization]
        public PointChartUser Get(string emailAddress)
        {
            DefaultOAuthToken accessToken = new DefaultOAuthToken();
            accessToken.Token = this.CurrentPrincipal.CurrentUser.AccessToken;
            accessToken.Secret = this.CurrentPrincipal.CurrentUser.AccessTokenSecret;
            return this.Services.UserService.FindByEmail(emailAddress, accessToken);
        }

        // POST api/<controller>
        [WebAPIAuthorization]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [WebAPIAuthorization]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [WebAPIAuthorization]
        public void Delete(int id)
        {
        }
    }
}