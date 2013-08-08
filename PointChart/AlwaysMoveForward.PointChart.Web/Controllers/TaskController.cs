using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AlwaysMoveForward.PointChart.DataLayer.Entities;
using AlwaysMoveForward.PointChart.Web.Models;
using AlwaysMoveForward.PointChart.Web.Code.Filters;

namespace AlwaysMoveForward.PointChart.Web.Controllers
{
    public class TaskController : BaseController
    {
        [RequestAuthorizationAttribute]
        public ActionResult Index()
        {
            TaskModel model = new TaskModel();
            model.Tasks = this.Services.Tasks.GetByUser(this.CurrentPrincipal.CurrentUser);
            return View(model);
        }

        [RequestAuthorizationAttribute]
        public ActionResult Add(String addTaskName, double addTaskPoints, int addTaskMaxPerDay)
        {
            TaskModel model = new TaskModel();

            Task newTask = this.Services.Tasks.Add(addTaskName, addTaskPoints, addTaskMaxPerDay, this.CurrentPrincipal.CurrentUser);

            if (newTask == null)
            {
                // tbd handle creation error.
            }

            model.Tasks = this.Services.Tasks.GetByUser(this.CurrentPrincipal.CurrentUser);

            return View("Index", model);
        }

        [RequestAuthorizationAttribute]
        public ActionResult Edit(int editTaskId, String editTaskName, double editTaskPoints, int editTaskMaxPerDay)
        {
            TaskModel model = new TaskModel();

            Task editedTask = this.Services.Tasks.Edit(editTaskId, editTaskName, editTaskPoints, editTaskMaxPerDay, this.CurrentPrincipal.CurrentUser);

            if (editedTask == null)
            {
                // tbd handle creation error.
            }

            model.Tasks = this.Services.Tasks.GetByUser(this.CurrentPrincipal.CurrentUser);

            return View("Index", model);
        }

    }
}