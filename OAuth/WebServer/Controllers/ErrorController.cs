using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VP.Digital.Security.OAuth.WebServer.Controllers
{
    /// <summary>
    /// A controller for handling error conditions.
    /// </summary>
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// A default page for a 401 error.
        /// </summary>
        /// <returns></returns>
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
