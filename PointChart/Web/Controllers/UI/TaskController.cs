using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.Web.Models;
using AlwaysMoveForward.PointChart.Web.Code.Filters;

namespace AlwaysMoveForward.PointChart.Web.Controllers.UI
{
    public class TaskController : BaseController
    {
        [MVCAuthorizationAttribute]
        public ActionResult Index()
        {
            TaskModel model = new TaskModel();
            model.Tasks = this.Services.Tasks.GetByUser(this.CurrentPrincipal.CurrentUser);
            return this.View(model);
        }

        [MVCAuthorizationAttribute]
        public ActionResult Add(string addTaskName, double addTaskPoints, int addTaskMaxPerDay)
        {
            TaskModel model = new TaskModel();

            Task newTask = this.Services.Tasks.Add(addTaskName, addTaskPoints, addTaskMaxPerDay, this.CurrentPrincipal.CurrentUser);

            if (newTask == null)
            {
                // tbd handle creation error.
            }

            model.Tasks = this.Services.Tasks.GetByUser(this.CurrentPrincipal.CurrentUser);

            return this.View("Index", model);
        }

        [MVCAuthorizationAttribute]
        public ActionResult Edit(int editTaskId, string editTaskName, double editTaskPoints, int editTaskMaxPerDay)
        {
            TaskModel model = new TaskModel();

            Task editedTask = this.Services.Tasks.Edit(editTaskId, editTaskName, editTaskPoints, editTaskMaxPerDay, this.CurrentPrincipal.CurrentUser);

            if (editedTask == null)
            {
                // tbd handle creation error.
            }

            model.Tasks = this.Services.Tasks.GetByUser(this.CurrentPrincipal.CurrentUser);

            return this.View("Index", model);
        }
    }
}