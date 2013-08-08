using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AnotherBlog.MVC.Utilities;

namespace AnotherBlog.MVC.Views.Blog
{
    public class Post : ViewPage<AnotherBlog.MVC.Models.BlogModel>
    {
        public string GetAuthorName
        {
            get
            {
                string retVal = "";

                AnotherBlog.Core.Utilities.SecurityPrincipal currentPrincipal = System.Threading.Thread.CurrentPrincipal as AnotherBlog.Core.Utilities.SecurityPrincipal;

                if (currentPrincipal != null)
                {
                    if (currentPrincipal.IsAuthenticated == true)
                    {
                        if (currentPrincipal.CurrentUser != null)
                        {
                            retVal = currentPrincipal.CurrentUser.UserName;
                        }
                    }
                }

                return retVal;
            }
        }

        public string GetAuthorEmail
        {
            get
            {
                string retVal = "";

                AnotherBlog.Core.Utilities.SecurityPrincipal currentPrincipal = System.Threading.Thread.CurrentPrincipal as AnotherBlog.Core.Utilities.SecurityPrincipal;

                if (currentPrincipal != null)
                {
                    if (currentPrincipal.IsAuthenticated == true)
                    {
                        if (currentPrincipal.CurrentUser != null)
                        {
                            retVal = currentPrincipal.CurrentUser.Email;
                        }
                    }
                }

                return retVal;
            }
        }

        public string PageTitle
        {
            get { return MvcApplication.SiteInfo.Name + " - " + ViewData.Model.TargetBlog.Name + " - " + ViewData.Model.BlogEntry.Title; }
        }

        public string PageDescription
        {
            get
            {
                string retVal = MvcApplication.SiteInfo.Name;
                retVal += " - " + ViewData.Model.BlogName;
                retVal += " - " + ViewData.Model.TargetBlog.Description;
                retVal += " - " + ViewData.Model.BlogEntry.Title;

                return retVal;
            }
        }

        public string DeliciousUrl
        {
            get
            {
                string retVal = "";
                if (base.Model.BlogEntry != null)
                {
                    retVal += "http://del.icio.us/post?url=";
                    retVal += this.Context.Request.Url.ToString();
                    retVal += "&title=";
                    retVal += HttpUtility.UrlEncode(base.Model.BlogEntry.Title);

                    string tags = "";
                    for (int i = 0; i < base.Model.EntryTags.Count; i++)
                    {
                        if (i > 0)
                        {
                            tags += " ";
                        }
                        tags += base.Model.EntryTags[i].Name.Replace(' ', '_');
                    }
                    if (tags != "")
                    {
                        retVal += "&tags=" + HttpUtility.UrlEncode(tags);
                    }
                }
                return retVal;
            }
        }

        public string RedditUrl
        {
            get
            {
                string retVal = "";

                if (base.Model.BlogEntry != null)
                {
                    retVal += "http://www.reddit.com/submit?url=";
                    retVal += this.Context.Request.Url.ToString();
                    retVal += "&title=";
                    retVal += HttpUtility.UrlEncode(base.Model.BlogEntry.Title);
                }

                return retVal;
            }
        }

        public string StumbleUponUrl
        {
            get
            {
                string retVal = "";
                if (base.Model.BlogEntry != null)
                {
                    retVal += "http://www.stumbleupon.com/submit?url=";
                    retVal += this.Context.Request.Url.ToString();
                    retVal += "&title=";
                    retVal += HttpUtility.UrlEncode(base.Model.BlogEntry.Title);
                }
                return retVal;
            }
        }

        public string TechnoratiUrl
        {
            get
            {
                string retVal = "";

                if (base.Model.BlogEntry != null)
                {
                    retVal += "http://technorati.com/faves?add=";
                    retVal += this.Context.Request.Url.ToString();
                    retVal += "&title=";
                    retVal += HttpUtility.UrlEncode(base.Model.BlogEntry.Title);

                    string tags = "";

                    for (int i = 0; i < base.Model.EntryTags.Count; i++)
                    {
                        if (i > 0)
                        {
                            tags += " ";
                        }
                        tags += base.Model.EntryTags[i].Name.Replace(' ', '_');
                    }
                    if (tags != "")
                    {
                        retVal += "&tags=" + HttpUtility.UrlEncode(tags);
                    }
                }
                return retVal;
            }
        }

        public string TwitterUrl
        {
            get
            {
                string retVal = "";
                if (base.Model.BlogEntry != null)
                {
                    retVal += "http://twitter.com/home?status=Currently reading about ";
                    retVal += base.Model.BlogEntry.Title;
                    retVal += " ";
                    retVal += Utils.GetTinyUrl(base.Model.BlogEntry);
                }
                return retVal;
            }
        }
    }
}
