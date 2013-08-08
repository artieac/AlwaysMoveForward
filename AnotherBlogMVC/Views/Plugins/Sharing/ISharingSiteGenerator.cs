using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnotherBlog.MVC.Views.Plugins.Sharing
{
    public interface ISharingSiteGenerator
    {
        string GenerateSharingHtml(string targetUrl, string title);
    }
}
