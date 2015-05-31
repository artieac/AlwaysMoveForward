using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using AlwaysMoveForward.MainSite.Models;

namespace AlwaysMoveForward.MainSite.Controllers
{
    public class HomeController : Controller
    {
        private const string CarouselImageFolder = "/content/images/Carousel";

        public CarouselModel CreateCarouselModel()
        {
            CarouselModel retVal = new CarouselModel();
            retVal.CarouselItems = new List<string>();

            string[] fileNames = Directory.GetFiles(Server.MapPath(HomeController.CarouselImageFolder));

            for (int i = 0; i < fileNames.Count(); i++ )
            {
                retVal.CarouselItems.Add(HomeController.CarouselImageFolder + "/" + fileNames[i].Substring(fileNames[i].LastIndexOf("\\") + 1));
            }

            return retVal;
        }

        public ActionResult Index()
        {
            return View(this.CreateCarouselModel());
        }

        public ActionResult About()
        {
            return View(this.CreateCarouselModel());
        }
    }
}
