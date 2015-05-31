using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AlwaysMoveForward.PointChart.Web.Code.Filters;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.Web.Models.API;

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
        public Task Post([FromBody]TaskInput taskData)
        {
            return this.Services.Tasks.Add(taskData.Name, taskData.Points, taskData.MaxPerDay, this.CurrentPrincipal.CurrentUser);
        }

        // PUT api/<controller>/5
        [WebAPIAuthorization]
        public Task Put(int id, [FromBody]TaskInput taskData)
        {
            return this.Services.Tasks.Edit(id, taskData.Name, taskData.Points, taskData.MaxPerDay, this.CurrentPrincipal.CurrentUser);
        }

        // DELETE api/<controller>/5
        [WebAPIAuthorization]
        public void Delete(int id)
        {
        }
    }
}