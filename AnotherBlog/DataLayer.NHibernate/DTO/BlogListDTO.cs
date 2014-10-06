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
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table = "BlogLists")]
    public class BlogListDTO 
    {
        public BlogListDTO()
        {
            this.Id = -1;
        }

        [NHibernate.Mapping.Attributes.Id(0, Column = "Id", UnsavedValue = "-1")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public int Id { get; set; }

        [NHibernate.Mapping.Attributes.ManyToOne(Name = "Blog", Class = "BlogDTO", ClassType = typeof(BlogDTO), Column = "BlogId")]
        public BlogDTO Blog { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public string Name { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public bool ShowOrdered { get; set; }

        [NHibernate.Mapping.Attributes.Bag(0, Table = "BlogListItems", Cascade="AllDeleteOrphan", Inverse=true)]
        [NHibernate.Mapping.Attributes.Key(1, Column = "Id")]
        [NHibernate.Mapping.Attributes.OneToMany(2, ClassType = typeof(BlogListItemDTO))]
        public IList<BlogListItemDTO> Items { get; set; }
    }
}
