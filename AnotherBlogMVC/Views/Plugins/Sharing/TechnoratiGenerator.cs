using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnotherBlog.MVC.Views.Plugins.Sharing
{
    public class TechnoratiGenerator : ISharingSiteGenerator
    {
        #region ISharingSiteGenerator Members

        public string GenerateSharingHtml(string targetUrl, string title)
        {
            String retVal = "<a href=\"http://technorati.com/faves";
            retVal += "?add=" + targetUrl;
            retVal += "&title=" + HttpUtility.UrlEncode(title);
            retVal += "\" target=\"_blank\">";
            retVal += "<img border=0 src=\"/Content/images/technorati.gif\" alt=\"submit to technorati\" ></a>";

            return retVal;
        }

        #endregion
    }
}
