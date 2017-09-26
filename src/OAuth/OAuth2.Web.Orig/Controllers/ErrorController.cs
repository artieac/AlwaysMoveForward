using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlwaysMoveForward.OAuth2.Web.Controllers
{
    /// <summary>
    /// A controller for handling error conditions.
    /// </summary>
    public class ErrorController : MVCControllerBase
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
