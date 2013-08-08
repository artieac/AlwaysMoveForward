/**
 * Copyright (c) 2009 Arthur Correa.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Common Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/cpl1.0.php
 *
 * Contributors:
 *    Arthur Correa – initial contribution
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Common.Utilities;
using AnotherBlog.Common.Data.Map;

namespace AnotherBlog.Common.Data.Entities
{
    public class BlogPost : IBlogPost
    {
        public static int MaxShortEntryLength = 1000;

        public virtual int EntryId { get; set; }
        public virtual bool IsPublished { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual User Author { get; set; }
        public virtual string EntryText { get; set; }
        public virtual string Title { get; set; }
        public virtual DateTime DatePosted { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual IList<IComment> Comments { get; set; }
        public virtual IList<ITag> Tags { get; set; }

        public virtual int GetCommentCount()
        {
            int retVal = 0;

            if (this.Comments != null)
            {
                retVal = this.Comments.Count;
            }

            return retVal;
        }
        
        public virtual string ShortEntryText
        {
            get
            {
                string retVal = Utils.StripHtml(this.EntryText);

                if (retVal.Length > BlogPost.MaxShortEntryLength)
                {
                    retVal = retVal.Substring(0, MaxShortEntryLength);
                }

                return retVal;
            }
        }
    }
}
