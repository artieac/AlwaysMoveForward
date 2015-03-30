using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.Web.Code.Filters;

namespace AlwaysMoveForward.PointChart.Web.Controllers
{
    public class ChartsController : BaseAPIController
    {
        // GET api/<controller>
        [RequestAuthorizationAttribute]
        public IList<Chart> Get()
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