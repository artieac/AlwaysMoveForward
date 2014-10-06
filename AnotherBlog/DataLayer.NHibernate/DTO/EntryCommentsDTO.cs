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
    [NHibernate.Mapping.Attributes.Class(Table = "EntryComments")]
    public class EntryCommentsDTO 
    {
        public EntryCommentsDTO() : base()
        {
            this.CommentId = -1;
        }

        [NHibernate.Mapping.Attributes.Id(0, Column = "CommentId", UnsavedValue = "-1")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public int CommentId { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public int Status { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public string Link { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public string AuthorEmail { get; set; }

        [NHibernate.Mapping.Attributes.Property(Type="StringClob")]
        public string Text { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public string AuthorName { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public DateTime DatePosted { get; set; }

        [NHibernate.Mapping.Attributes.ManyToOne(Name = "Post", Class = "BlogPostDTO", ClassType = typeof(BlogPostDTO), Column = "EntryId")]
        public BlogPostDTO BlogPost { get; set; }
    }
}
