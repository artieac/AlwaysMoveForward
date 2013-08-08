using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnotherBlog.MVC.Views.Plugins.Sharing
{
    public class RedditGenerator : ISharingSiteGenerator
    {
        #region ISharingSiteGenerator Members

        public string GenerateSharingHtml(string targetUrl, string title)
        {
            String retVal = "<a href=\"http://www.reddit.com/submit";
            retVal += "?url=" + targetUrl;
            retVal += "&title=" + HttpUtility.UrlEncode(title);
            retVal += "\" target=\"_blank\">";
            retVal += "<img src=\"http://www.reddit.com/static/spreddit5.gif\" alt=\"submit to reddit\" border=\"0\" /></a>";

            return retVal;
        }

        #endregion
    }
}
