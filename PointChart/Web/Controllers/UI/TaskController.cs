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
      
    }
}