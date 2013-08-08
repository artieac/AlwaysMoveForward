using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheOffWing.AnotherBlog.Core.Entity
{
    /// <summary>
    /// Extend the LINQ generated EntryComment class in case some sort of additional properties or methods
    /// need to be added to it. 
    /// </summary>
    public partial class EntryComment 
    {
        /// <summary>
        /// What are the allowed comment statuses?  
        /// </summary>
        public struct CommentStatus
        {
            public const int Unapproved = 0;
            public const int Approved   = 1;
            public const int Deleted    = 2;

        }
        /// <summary>
        /// Clean script references out of any comments (todo - change this to use the Utils method that does this)
        /// </summary>
        public void CleanCommentText()
        {
            int scriptStart = this.Comment.IndexOf("<script>");

            if (scriptStart > -1)
            {
                int scriptEnd = this.Comment.IndexOf("</script>");
                this.Comment.Remove(scriptStart, ((scriptEnd + 9) - scriptStart));
            }
        }
    }
}
