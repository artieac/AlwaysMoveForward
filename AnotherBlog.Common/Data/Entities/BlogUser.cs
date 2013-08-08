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

using AnotherBlog.Common.Data.Map;

namespace AnotherBlog.Common.Data.Entities
{
    public class BlogUser : IBlogUser
    {
        public virtual int BlogUserId { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual User User { get; set; }
        public virtual Role UserRole { get; set; }
    }
}
