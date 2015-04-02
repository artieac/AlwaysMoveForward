using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AlwaysMoveForward.PointChart.Web.Code.Filters;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.Web.Controllers.API
{
    public class TaskController : BaseAPIController
    {     
        [Route("api/Tasks"), HttpGet()]
        [WebAPIAuthorization]
        public IEnumerable<Task> Get()
        {
            return this.Services.Tasks.GetByUser(this.CurrentPrincipal.CurrentUser);
        }

        // GET api/<controller>/5
        [WebAPIAuthorization]
        public Task Get(int id)
        {
            return this.Services.Tasks.GetById(id);
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