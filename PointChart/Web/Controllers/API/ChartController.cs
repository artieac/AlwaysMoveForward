using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.Web.Code.Filters;
using AlwaysMoveForward.PointChart.Web.Models.API;

namespace AlwaysMoveForward.PointChart.Web.Controllers.API
{
    public class ChartController : BaseAPIController
    {
        [Route("api/Charts"), HttpGet()]
        [WebAPIAuthorization]
        public IList<Chart> Get(string chartRole)
        {
            IList<Chart> retVal = new List<Chart>();

            IEnumerable<KeyValuePair<string, string>> queryStringParams = this.Request.GetQueryNameValuePairs();

            foreach (KeyValuePair<string, string> queryStringItem in queryStringParams)
            {
                if (queryStringItem.Key == "chartRole")
                {
                    if (queryStringItem.Value == "creator")
                    {
                        retVal = this.Services.Charts.GetByCreator(this.CurrentPrincipal.CurrentUser);
                    }
                    else if (queryStringItem.Value == "pointEarner")
                    {
                        retVal = this.Services.Charts.GetByPointEarner(this.CurrentPrincipal.CurrentUser);
                    }
                }
            }

            return retVal;
        }

        [Route("api/PointEarner/{id}/Charts"), HttpGet()]
        [WebAPIAuthorization]
        public long GetPointsForCharts(long id)
        {
            return 0;
        }
        
        // GET api/<controller>/5
        [WebAPIAuthorization]
        public Chart Get(long id)
        {
            Chart retVal = null;

            if (id > 0)
            {
                retVal = this.Services.Charts.GetById(id);
            }
            else
            {
                retVal = new Chart();
            }

            return retVal;
        }

        // POST api/<controller>
        [WebAPIAuthorization]
        public Chart Post([FromBody]ChartInput taskInput)
        {
            return this.Services.Charts.AddChart(taskInput.Name, taskInput.PointEarnerId, taskInput.Tasks, this.CurrentPrincipal.CurrentUser);
        }

        // PUT api/<controller>/5
        [WebAPIAuthorization]
        public Chart Put(int id, [FromBody]ChartInput taskInput)
        {
            return this.Services.Charts.UpdateChart(id, taskInput.Name, taskInput.PointEarnerId, taskInput.Tasks, this.CurrentPrincipal.CurrentUser);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}