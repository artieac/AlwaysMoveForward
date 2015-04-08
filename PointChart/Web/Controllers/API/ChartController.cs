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

        // GET api/<controller>/5
        [WebAPIAuthorization]
        public Chart Get(long id)
        {
            return this.Services.Charts.GetById(id);
        }

        // POST api/<controller>
        [WebAPIAuthorization]
        public void Post([FromBody]ChartInput taskInput)
        {
        }

        // PUT api/<controller>/5
        [WebAPIAuthorization]
        public void Put(int id, [FromBody]ChartInput taskInput)
        {
            for(int i = 0; i < taskInput.Tasks.Count; i++)
            {
                this.Services.Charts.AddTask(id, taskInput.Tasks[i].Id);
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}