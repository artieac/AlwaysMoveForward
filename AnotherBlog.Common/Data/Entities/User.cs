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

namespace AnotherBlog.Common.Data.Entities
{
    public class User
    {
        public virtual int UserId{ get; set;}
        public virtual string UserName{ get; set;}
        public virtual string Password{ get; set;}
        public virtual string Email{ get; set;}
        public virtual bool ApprovedCommenter{ get; set;}
        public virtual bool IsActive{ get; set;}
        public virtual bool IsSiteAdministrator{ get; set;}
        public virtual string About{ get; set;}
        public virtual IList<BlogUser> UserBlogs{ get; set;}
        public virtual string DisplayName{ get; set;}
        public virtual IList<BlogPost> BlogEntries{ get; set;}
    }
}
