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

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.MVC.Models
{
    public class HomeModel : ModelBase
    {
        public HomeModel()
            : base()
        {
            
        }

        public string ContentMessage{ get; set;}
        public IList<BlogPost> BlogEntries{ get; set;}
    }
}
