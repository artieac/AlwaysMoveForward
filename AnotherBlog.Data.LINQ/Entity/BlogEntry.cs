using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

using TheOffWing.AnotherBlog.Core;
using TheOffWing.AnotherBlog.Core.Utilities;

namespace TheOffWing.AnotherBlog.Core.Entity
{
    /// <summary>
    /// Extend the LINQ generated BlogEntry class in case some sort of additional properties or methods
    /// need to be added to it. 
    /// </summary>
    public partial class BlogEntry 
    {
        private const int MaxShortEntryLength = 200;
        /// <summary>
        /// Strip out the HTML tags and return a shortened version of the blog entry.  This is for displaying
        /// a summary for blog index views as well as RSS feeds.
        /// </summary>
        public string ShortEntryText
        {
            get
            {
                string retVal = Utils.StripHtml(this.EntryText);
         
                if (retVal.Length > BlogEntry.MaxShortEntryLength)
                {
                    retVal = retVal.Substring(0, MaxShortEntryLength);
                }

                return retVal;
            }
        }
        /// <summary>
        /// Clean HTML script tags out of the blog input.
        /// </summary>
        public void CleanBlogText()
        {
            int scriptStart = this.EntryText.IndexOf("<script>");

            if (scriptStart > -1)
            {
                int scriptEnd = this.EntryText.IndexOf("</script>");
                this.EntryText.Remove(scriptStart, ((scriptEnd + 9) - scriptStart));
            }
        }
        /// <summary>
        /// Not all comments are allowed to be shown.  Only count the ones that can be shown. 
        /// </summary>
        public int ApprovedCommentCount
        {
            get
            {
                int retVal = 0;

                if (this.EntryComments != null)
                {
                    // Use LINQ to query into the EntryComments array collection for only comments that have a status of approved.
                    retVal = (from foundItem in this.EntryComments where foundItem.Status == EntryComment.CommentStatus.Approved && foundItem.EntryId == this.EntryId select foundItem).Count();
                }

                return retVal;
            }
        }
    }
}
