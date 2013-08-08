using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnotherBlog.MVC.Views.Plugins.Sharing
{
    public class DiggGenerator : ISharingSiteGenerator
    {
        #region ISharingSiteGenerator Members

        public string GenerateSharingHtml(string targetUrl, string title)
        {
            String retVal = "<script type=\"text/javascript\">";
            retVal += "digg_bgcolor = '#ff9900';";
            retVal += "digg_skin = 'icon';";
            retVal += "digg_window = 'new';";
            retVal += "digg_title = '" + title + "';";
            retVal += "</script>";
            retVal += "<script src=\"http://digg.com/tools/diggthis.js\" type=\"text/javascript\"></script>"; 
 
            return retVal;
        }

        #endregion
    }
}
