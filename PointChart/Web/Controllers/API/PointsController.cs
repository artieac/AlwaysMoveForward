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
    public class CompleteTaskController : BaseAPIController
    {
        /// <summary>
        /// Get points for current user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="chartId"></param>
        /// <returns></returns>
        [Route("api/CompleteTasks"), HttpGet()]
        [WebAPIAuthorization]
        public IList<CompletedTask> Get()
        {
            IList<CompletedTask> retVal = new List<CompletedTask>();

            return retVal;
        }

        [Route("api/Chart/{id}/CompleteTask/{year}/{month}/{day}"), HttpGet()]
        [WebAPIAuthorization]
        public CompletedTasksModel GetCompletedTasksForWeek(long id, int year, int month, int day)
        {
            CompletedTasksModel retVal = new CompletedTasksModel();

            DateTime targetDate = DateTime.Parse(month + "/" + day + "/" + year);

            retVal.Calendar = new AlwaysMoveForward.PointChart.Web.Models.CalendarModel(id, targetDate);
            
            Chart targetChart = this.Services.Charts.GetById(id);
            retVal.CompletedTasks = new Dictionary<long, IDictionary<DateTime, CompletedTask>>();

            IList<CompletedTask> completedTasks = this.Services.CompletedTaskService.GetAllByChart(id, retVal.Calendar.WeekStartDate, retVal.Calendar.WeekStartDate.AddDays(7), this.CurrentPrincipal.CurrentUser);

            foreach (CompletedTask completedTask in completedTasks)
            {
                if (!retVal.CompletedTasks.ContainsKey(completedTask.TaskId))
                {
                    retVal.CompletedTasks.Add(completedTask.TaskId, new Dictionary<DateTime, CompletedTask>());
                }

                retVal.CompletedTasks[completedTask.TaskId].Add(completedTask.DateCompleted, completedTask);
            }

            return retVal;
        }

        [Route("api/Chart/{id}/CompleteTask"), HttpPost()]
        [WebAPIAuthorization]
        public CompletedTask Post(long id, [FromBody]TaskInput taskInput)
        {
            return this.Services.CompletedTaskService.CompleteTask(id, taskInput.Id, taskInput.TimesCompleted, this.CurrentPrincipal.CurrentUser);
        }

        [Route("api/Chart/{id}/Task/CompleteTask"), HttpPut()]
        [WebAPIAuthorization]
        public CompletedTask Put(int id, [FromBody]TaskInput taskInput)
        {
            return this.Services.CompletedTaskService.CompleteTask(id, taskInput.Id, taskInput.TimesCompleted, this.CurrentPrincipal.CurrentUser);
        }

        [Route("api/Chart/{id}/CompleteTask/{taskId}/{month}/{day}/{year}"), HttpDelete()]
        [WebAPIAuthorization]
        public void Delete(long id, long taskId, int month, int day, int year)
        {
            DateTime dateCompleted = DateTime.Parse(month + "/" + day + "/" + year);
            this.Services.CompletedTaskService.DeleteCompletedTask(id, taskId, dateCompleted, this.CurrentPrincipal.CurrentUser);
        }
    }
}