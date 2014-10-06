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
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table = "BlogEntries")]
    public class BlogPostDTO 
    {
        public BlogPostDTO()
        {
            this.EntryId = -1;
        }

        [NHibernate.Mapping.Attributes.Id(0, Column = "EntryId", UnsavedValue = "-1")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public int EntryId { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public bool IsPublished { get; set; }

        [NHibernate.Mapping.Attributes.ManyToOne(Name = "Blog", Class = "BlogDTO", ClassType = typeof(BlogDTO), Column = "BlogId")]
        public BlogDTO Blog { get; set; }

        [NHibernate.Mapping.Attributes.ManyToOne(Name = "Author", Class = "UserDTO", ClassType = typeof(UserDTO), Column = "UserId")]
        public UserDTO Author { get; set; }

        [NHibernate.Mapping.Attributes.Property(Type="StringClob")]
        public string EntryText { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public string Title { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public DateTime DatePosted { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public DateTime DateCreated { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public int TimesViewed { get; set; }

        [NHibernate.Mapping.Attributes.Bag(0, Table = "BlogEntryTags", Cascade = "SaveUpdate")]
        [NHibernate.Mapping.Attributes.Key(1, Column = "BlogEntryId")]
        [NHibernate.Mapping.Attributes.ManyToMany(2, Column = "TagId", ClassType = typeof(TagDTO))]
        public IList<TagDTO> Tags { get; set; }

        [NHibernate.Mapping.Attributes.Bag(0, Table = "EntryComments", Cascade="AllDeleteOrphan", Inverse=true)]
        [NHibernate.Mapping.Attributes.Key(1, Column = "CommentId")]
        [NHibernate.Mapping.Attributes.OneToMany(2, ClassType = typeof(EntryCommentsDTO))]
        public IList<EntryCommentsDTO> Comments { get; set; }
    }
}
