using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnotherBlog.MVC.Views.Plugins.Sharing
{
    public class StumbleUponGenerator : ISharingSiteGenerator
    {
        #region ISharingSiteGenerator Members

        public string GenerateSharingHtml(string targetUrl, string title)
        {
            string retVal = "<a href=\"http://www.stumbleupon.com/submit";
            retVal += "?url=" + targetUrl;
            retVal += "&title=" + HttpUtility.UrlEncode(title);
            retVal += "\" target=\"_blank\">";
            retVal += "<img border=0 src=\"http://cdn.stumble-upon.com/images/16x16_su_solid.gif\" alt=\"submit to stumbleupon\"></a>";

            return retVal;
        }

        #endregion
    }
}
