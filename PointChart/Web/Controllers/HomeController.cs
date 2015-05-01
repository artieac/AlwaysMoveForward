using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.Web.Code.Filters;
using AlwaysMoveForward.PointChart.Web.Models;

namespace AlwaysMoveForward.PointChart.Web.Controllers
{
    public class HomeController : BaseController
    {
        // GET: /Home/
        [CookieAuthenticationParser]
        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            model.OwnedCharts = this.Services.Charts.GetByCreator(this.CurrentPrincipal.CurrentUser);
            model.AssignedCharts = this.Services.Charts.GetByPointEarner(this.CurrentPrincipal.CurrentUser);

            return this.View(model);
        }

        [CookieAuthenticationParser]
        public ActionResult PointEarners()
        {
            return this.View();
        }

        [CookieAuthenticationParser]
        public ActionResult CollectPoints(long id)
        {
            DateTime selectedDate = DateTime.Now;
            return this.View(id);
        }

        [Route("Home/CollectPoints/{id}/{month}/{day}/{year}"), HttpGet()]
        [CookieAuthenticationParser]
        public ActionResult CollectPoints(long id, int month, int day, int year)
        {
            Models.UI.CollectPointsModel retVal = new Models.UI.CollectPointsModel();
            retVal.ChartId = id;
            retVal.SelectedDate = DateTime.Parse(month + "/" + day + "/" + year);
            return this.View(retVal);
        }
    }
}
