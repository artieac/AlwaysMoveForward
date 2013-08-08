
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AnotherBlog.Core;

namespace AnotherBlog.Web.Views.Shared
{
    public class SharingControl : System.Web.Mvc.ViewUserControl<AnotherBlog.Web.Models.BlogEntryModel>
    {
        public String StumbleUponUrl
        {
            get
            {
                String retVal = "";
                
                if(this.Model.BlogEntry!=null)
                {
                    retVal += "http://www.stumbleupon.com/submit?url=";
                    retVal += this.Context.Request.Url.ToString();
                    retVal += "&title=";
                    retVal += HttpUtility.UrlEncode(this.Model.BlogEntry.Title);
                }

                return retVal;
            }
        }

        public String DeliciousUrl
        {
            get
            {
                String retVal = "";

                if (this.Model.BlogEntry != null)
                {
                    retVal += "http://del.icio.us/post?url=";
                    retVal += this.Context.Request.Url.ToString();
                    retVal += "&title=";
                    retVal += HttpUtility.UrlEncode(this.Model.BlogEntry.Title);

                    string tags = "";

                    for(int i = 0; i < this.Model.EntryTags.Count; i++)
                    {
                        if(i > 0)
                        {
                            tags += " ";
                        }
                        tags += this.Model.EntryTags[i].name.Replace(' ', '_');
                    }

                    if (tags != "")
                    {
                        retVal += "&tags=";
                        retVal += HttpUtility.UrlEncode(tags);
                    }

                }
                return retVal;
            }
        }

        public String TechnoratiUrl
        {
            get
            {
                String retVal = "";

                if (this.Model.BlogEntry != null)
                {
                    retVal += "http://technorati.com/faves?add=";
                    retVal += this.Context.Request.Url.ToString();
                    retVal += "&title=";
                    retVal += HttpUtility.UrlEncode(this.Model.BlogEntry.Title);
                    
                    string tags = "";

                    for (int i = 0; i < this.Model.EntryTags.Count; i++)
                    {
                        if (i > 0)
                        {
                            tags += " ";
                        }
                        tags += this.Model.EntryTags[i].name.Replace(' ', '_');
                    }

                    if (tags != "")
                    {
                        retVal += "&tags=";
                        retVal += HttpUtility.UrlEncode(tags);
                    }
                }

                return retVal;
            }
        }

        public String RedditUrl
        {
            get
            {
                String retVal = "";

                if (this.Model.BlogEntry != null)
                {
                    retVal += "http://www.reddit.com/submit?url=";
                    retVal += this.Context.Request.Url.ToString();
                    retVal += "&title=";
                    retVal += HttpUtility.UrlEncode(this.Model.BlogEntry.Title);
                }

                return retVal;
            }
        }
    }
}