using System.Web;
using System.Web.Mvc;

namespace AlwaysMoveForward.OAuth.WebServer
{
    /// <summary>
    /// Configure any filters upon startup
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Register all global filters
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}