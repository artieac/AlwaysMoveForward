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

using Castle.ActiveRecord;

using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Entities
{
    [ActiveRecord("Blogs")]
    public class BlogDTO
    {
        public BlogDTO()
            : base()
        {
            this.BlogId = -1;
        }

        [PrimaryKey(PrimaryKeyType.Identity, "BlogId", UnsavedValue="-1")]
        public int BlogId{ get; set;}

        [Property("Name")]
        public string Name { get; set; }

        [Property("Description", ColumnType = "StringClob")]
        public string Description { get; set; }

        [Property("SubFolder")]
        public string SubFolder { get; set; }

        [Property("About", ColumnType = "StringClob")]
        public string About { get; set; }

        [Property("WelcomeMessage", ColumnType = "StringClob")]
        public string WelcomeMessage { get; set; }

        [Property("ContactEmail")]
        public string ContactEmail { get; set; }

        [Property("Theme")]
        public string Theme { get; set; }

        [HasMany(typeof(BlogPostDTO), Cascade = ManyRelationCascadeEnum.SaveUpdate)]
        public IList<BlogPostDTO> Posts { get; set; }

        [HasMany(typeof(BlogUserDTO), Cascade = ManyRelationCascadeEnum.Delete)]
        public IList<BlogUserDTO> Users { get; set; }

    }
}
