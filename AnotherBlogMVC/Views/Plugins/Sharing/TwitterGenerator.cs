using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnotherBlog.MVC.Views.Plugins.Sharing
{
    public class TwitterGenerator : ISharingSiteGenerator
    {
        #region ISharingSiteGenerator Members

        public string GenerateSharingHtml(string targetUrl, string title)
        {
            string retVal = "<script type=\"text/javascript\">tweetmeme_style = 'compact';</script>";
            retVal += "<script type=\"text/javascript\" src=\"http://tweetmeme.com/i/scripts/button.js\"></script>";

            return retVal;
        }

        #endregion
    }
}
