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

using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.Utilities;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;

namespace AlwaysMoveForward.AnotherBlog.Common.DomainModel
{
    public class BlogPost
    {
        public const int MaxShortEntryLength = 1000;
        public static readonly DateTime StartDate = new DateTime(2009, 1, 1);

        public BlogPost()
        {
            this.EntryId = -1;
            this.DatePosted = BlogPost.StartDate;
            this.Tags = new List<Tag>();
        }

        public int EntryId { get; set; }
        public bool IsPublished { get; set; }
        public Blog Blog { get; set; }
        public User Author { get; set; }
        public string EntryText { get; set; }
        public string Title { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime DateCreated { get; set; }
        public int CommentCount { get; set; }
        public int TimesViewed { get; set; }
        public IList<Tag> Tags { get; set; }
                
        public string ShortEntryText
        {
            get
            {
                string retVal = Utils.StripHtml(this.EntryText);

                if (retVal.Length > BlogPost.MaxShortEntryLength)
                {
                    retVal = retVal.Substring(0, BlogPost.MaxShortEntryLength);
                }

                return retVal;
            }
        }

        public void SetPublishState(bool newState)
        {
            if (this.IsPublished != newState)
            {
                // the published state has changed
                if (newState == true)
                {
                    if (this.DatePosted.Date == BlogPost.StartDate)
                    {
                        this.DatePosted = DateTime.Now;
                    }
                }
            }
            else
            {
                if (newState == false)
                {
                    this.DatePosted = BlogPost.StartDate;
                }
            }

            this.IsPublished = newState;
        }
    }
}
