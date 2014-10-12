using System.Web.Mvc;

namespace VP.Digital.Security.OAuth.WebServer.Controllers
{
    /// <summary>
    /// Handles requests to ensure the website is active.
    /// </summary>
    public class LiveCheckController : Controller
    {
        /// <summary>
        /// Handles requests for the index.
        /// </summary>
        /// <returns>Everything is awesome.</returns>
        public ActionResult Index()
        {
            return this.Content("EVERYTHING IS AWESOME.");
        }
    }
}