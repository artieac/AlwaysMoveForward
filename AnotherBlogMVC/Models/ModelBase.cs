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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Core.Service;

namespace AnotherBlog.MVC.Models
{
    public class ModelBase 
    {
        private string blogSubFolder;
        private DateTime targetMonth;

        public ModelBase()
        {
            targetMonth = DateTime.Now;
            blogSubFolder = "All";
        }

        #region Properties

        public Blog TargetBlog{ get; set;}
        public string BlogName{ get; set;}
        public string BlogSubFolder
        {
            get { return this.blogSubFolder; }
            set { this.blogSubFolder = value; }
        }

        public string ContentTitle{ get; set;}
        public IList<Blog> BlogList{ get; set;}
        public IList BlogDates{ get; set;}
        public IList BlogTags{ get; set;}
        public IList<BlogRollLink> BlogRoll{ get; set;}
        public IList<DateTime> CurrentMonthBlogDates{ get; set;}

        public DateTime TargetMonth
        {
            get { return targetMonth; }
            set { targetMonth = value;}
        }

        public IList<BlogExtension> RegisteredExtensions{ get; set;}

        #endregion

        public virtual string GeneratePageTitle()
        {
            string retVal = ""; 

            if(this.TargetBlog!=null)
            {
                retVal = " " + TargetBlog.Name;
            }
            
            return retVal;
        }

        public virtual string GeneratePageDescription()
        {
            string retVal = "";
            
            if(this.TargetBlog!=null)
            {
                retVal += " - " + TargetBlog.Name + " - " + TargetBlog.Description;
            }

            return retVal;
        }
    }
}
