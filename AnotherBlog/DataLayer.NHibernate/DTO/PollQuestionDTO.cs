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
    [NHibernate.Mapping.Attributes.Class(Table = "PollQuestions")]
    public class PollQuestionDTO 
    {
        public PollQuestionDTO()
        {
            this.Id = -1;
        }

        [NHibernate.Mapping.Attributes.Id(0, Column = "PollQuestionId", UnsavedValue = "-1")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public int Id { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public string QuestionText { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public string Title { get; set; }

        [NHibernate.Mapping.Attributes.Bag(0, Table = "PollOptions", Cascade = "AllDeleteOrphan", Inverse = true)]
        [NHibernate.Mapping.Attributes.Key(1, Column = "Id")]
        [NHibernate.Mapping.Attributes.OneToMany(2, ClassType = typeof(PollOptionDTO))]
        public IList<PollOptionDTO> Options { get; set; }
    }
}
