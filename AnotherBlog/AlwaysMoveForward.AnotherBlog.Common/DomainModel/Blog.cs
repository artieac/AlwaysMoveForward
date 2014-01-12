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

using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;

namespace AlwaysMoveForward.AnotherBlog.Common.DomainModel
{
    public class Blog 
    {
        public Blog()
        {
            this.BlogId = -1;
        }

        public virtual int BlogId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string SubFolder { get; set; }
        public virtual string About { get; set; }
        public virtual string WelcomeMessage { get; set; }
        public virtual string ContactEmail { get; set; }
        public virtual string Theme { get; set; }
        public virtual int CurrentPollId { get; set; }
        public virtual IList<BlogPost> Posts { get; set; }
        public virtual IList<BlogUser> Users { get; set; }
    }
}
