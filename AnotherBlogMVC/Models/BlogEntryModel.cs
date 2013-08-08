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
using System.Web;

using AnotherBlog.Common.Utilities;
using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.MVC.Models
{
    public class BlogEntryModel : ModelBase
    {
        public BlogEntryModel()
            : base()
        {

        }

        public User CurrentUser{ get; set;}
        public BlogPost BlogEntry{ get; set;}
        public PagedList<BlogPost> EntryList{ get; set;}
        public IList<Tag> EntryTags{ get; set;}
        public IList<Comment> EntryComments{ get; set;}
        public BlogPost PreviousEntry{ get; set;}
        public BlogPost NextEntry{ get; set;}

        public override string GeneratePageTitle()
        {
            string retVal = " " + this.TargetBlog.Name;

            if (this.BlogEntry != null)
            {
                retVal += " " + this.BlogEntry.Title;

                if (this.EntryTags != null)
                {
                    for (int i = 0; i < this.EntryTags.Count; i++)
                    {
                        retVal += " " + this.EntryTags[i].Name;
                    }
                }
            }
            else
            {
                retVal += " " + this.TargetBlog.Description;
            }


            return retVal;
        }

        public override string GeneratePageDescription()
        {
            string retVal = " " + this.TargetBlog.Name;

            if (this.BlogEntry != null)
            {
                retVal += " " + this.BlogEntry.Title;

                if (this.EntryTags != null)
                {
                    for (int i = 0; i < this.EntryTags.Count; i++)
                    {
                        retVal += " " + this.EntryTags[i].Name;
                    }
                }
            }
            else
            {
                retVal += " " + this.TargetBlog.Description;
            }

            return retVal;
        }
    }
}
