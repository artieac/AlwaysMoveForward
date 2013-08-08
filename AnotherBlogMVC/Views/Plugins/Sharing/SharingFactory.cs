using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnotherBlog.MVC.Views.Plugins.Sharing
{
    public class SharingFactory
    {
        public enum SharingSites 
        {
            Digg = 0,
            Delicious = 1,
            Reddit = 2,
            StumbleUpon = 3,
            Technorati = 4,
            Twitter = 5
        }

        public static SharingSites[] SharingSitesList = 
        { 
            SharingSites.Digg, 
//            SharingSites.Delicious, 
//            SharingSites.Reddit, 
//            SharingSites.StumbleUpon, 
            SharingSites.Technorati, 
            SharingSites.Twitter 
        };

        public static ISharingSiteGenerator GetHtmlGenerator(SharingSites targetSite)
        {
            ISharingSiteGenerator retVal = null;

            switch (targetSite)
            {
                case SharingSites.Digg:
                    retVal = new DiggGenerator();
                    break;
                case SharingSites.Delicious:
                    retVal = new DelicousGenerator();
                    break;
                case SharingSites.Reddit:
                    retVal = new RedditGenerator();
                    break;
                case SharingSites.StumbleUpon:
                    retVal = new StumbleUponGenerator();
                    break;
                case SharingSites.Technorati:
                    retVal = new TechnoratiGenerator();
                    break;
                case SharingSites.Twitter:
                    retVal = new TwitterGenerator();
                    break;
            }

            return retVal;
        }

        public static string GenerateSharingHtml(SharingSites targetSite, string targetUrl, string title)
        {
            String retVal = "";

            ISharingSiteGenerator htmlGenerator = SharingFactory.GetHtmlGenerator(targetSite);

            if (htmlGenerator != null)
            {
                retVal = htmlGenerator.GenerateSharingHtml(targetUrl, title);
            }

            return retVal;
        }
    }
}
