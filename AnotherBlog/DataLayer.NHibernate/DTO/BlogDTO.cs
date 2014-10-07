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
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table = "Blogs")]
    public class BlogDTO
    {
        public BlogDTO()
            : base()
        {
            this.BlogId = -1;
        }

        [NHibernate.Mapping.Attributes.Id(0, Name="BlogId", Type = "Int32", Column = "BlogId", UnsavedValue = "-1")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public virtual int BlogId { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual string Name { get; set; }

        [NHibernate.Mapping.Attributes.Property(Type="StringClob")]
        public virtual string Description { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual string SubFolder { get; set; }

        [NHibernate.Mapping.Attributes.Property(Type="StringClob")]
        public virtual string About { get; set; }

        [NHibernate.Mapping.Attributes.Property(Type="StringClob")]
        public virtual string WelcomeMessage { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual string ContactEmail { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual string Theme { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual int CurrentPollId { get; set; }

        [NHibernate.Mapping.Attributes.Bag(0, Table = "BlogEntries", Cascade="Save-Update")]
        [NHibernate.Mapping.Attributes.Key(1, Column = "EntryId")]
        [NHibernate.Mapping.Attributes.OneToMany(2, ClassType = typeof(BlogListDTO))]
        public virtual IList<BlogPostDTO> Posts { get; set; }

        [NHibernate.Mapping.Attributes.Bag(0, Table = "BlogUser", Cascade="Delete", Inverse=true)]
        [NHibernate.Mapping.Attributes.Key(1, Column = "BlogUserId")]
        [NHibernate.Mapping.Attributes.OneToMany(2, ClassType = typeof(BlogUserDTO))]
        public virtual IList<BlogUserDTO> Users { get; set; }
    }
}
