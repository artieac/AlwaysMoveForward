﻿using System;
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
        public IList<PointChartUser> Get(string emailAddress)
        {
            DefaultOAuthToken accessToken = new DefaultOAuthToken();
            accessToken.Token = ((IRemoteOAuthUser)this.CurrentPrincipal.CurrentUser).AccessToken;
            accessToken.Secret = ((IRemoteOAuthUser)this.CurrentPrincipal.CurrentUser).AccessTokenSecret;
            return this.Services.UserService.SearchByEmail(emailAddress, accessToken);
        }

        [Route("api/PointEarner/{id}/Points"), HttpGet()]
        [WebAPIAuthorization]
        public PointInfo GetPointInformation(long id)
        {
            PointInfo retVal = new PointInfo(); ;

            IList<PointsSpent> pointsSpent = this.Services.PointService.GetPointsSpent(id);

            if(pointsSpent != null)
            {
                for(int i = 0; i < pointsSpent.Count; i++)
                {
                    retVal.PointsSpent += pointsSpent[i].Amount;
                }
            }

            IDictionary<long, IList<CompletedTask>> completedTasks = this.Services.CompletedTaskService.GetByPointEarner(id, this.CurrentPrincipal.CurrentUser);

            foreach(long chartId in completedTasks.Keys)
            {
                double chartPointsEarned = 0.0;

                for(int i = 0; i < completedTasks[chartId].Count; i++)
                {
                    chartPointsEarned = completedTasks[chartId][i].NumberOfTimesCompleted * completedTasks[chartId][i].PointValue;    
                }

                retVal.PointsEarned.Add(chartId, chartPointsEarned);
            }

            return retVal;
        }

        // POST api/<controller>
        [WebAPIAuthorization]
        public void Post([FromBody]PointEarnerInput pointEarnerData)
        {
            if(pointEarnerData != null)
            {
                if(pointEarnerData.PointEarnerId > 0)
                {
                    this.Services.UserService.AddExistingPointEarner(pointEarnerData.PointEarnerId, this.CurrentPrincipal.CurrentUser);
                }
                else
                {
                    DefaultOAuthToken accessToken = new DefaultOAuthToken();
                    accessToken.Token = ((IRemoteOAuthUser)this.CurrentPrincipal.CurrentUser).AccessToken;
                    accessToken.Secret = ((IRemoteOAuthUser)this.CurrentPrincipal.CurrentUser).AccessTokenSecret;
                    this.Services.UserService.AddNewPointEarner(pointEarnerData.OAuthServiceUserId, accessToken, this.CurrentPrincipal.CurrentUser);
                }
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
        public void Delete(long id)
        {
            this.Services.UserService.RemovePointEarner(id, this.CurrentPrincipal.CurrentUser);
        }
    }
}