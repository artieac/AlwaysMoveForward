using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnotherBlog.MVC.Views.Plugins.Sharing
{
    public class DelicousGenerator : ISharingSiteGenerator
    {

        #region ISharingSiteGenerator Members

        public string GenerateSharingHtml(string targetUrl, string title)
        {
            string retVal = "<a href=\"http://del.icio.us/post";
            retVal += "?url=" + targetUrl;
            retVal += "&title=" + HttpUtility.UrlEncode(title);

//            string tags = "";
//            for (int i = 0; i < base.Model.EntryTags.Count; i++)
//            {
//                if (i > 0)
//                {
//                    tags += " ";
//                }
//                tags += base.Model.EntryTags[i].Name.Replace(' ', '_');
//            }
//            if (tags != "")
//            {
//                retVal += "&tags=" + HttpUtility.UrlEncode(tags);
//            }
            retVal += "\" target=\"_blank\">";
            retVal += "<img border=0 src=\"/Content/images/delicious.gif\" alt=\"submit to delicious\"></a>";

            return retVal;
        }

        #endregion
    }
}
