using System.Web.Mvc;

namespace AlwaysMoveForward.AnotherBlog.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "EditPost",
                "Admin/{controller}/{action}/{blogSubFolder}/{id}",
                new { controller = "ManageBlog", action = "EditPost", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "PostSpecific",
                "Admin/{controller}/{action}/{blogSubFolder}/{blogPostId}/{filter}",
                new { controller = "ManageBlog", action = "EditPost", filter = UrlParameter.Optional }
);

        }
    }
}
