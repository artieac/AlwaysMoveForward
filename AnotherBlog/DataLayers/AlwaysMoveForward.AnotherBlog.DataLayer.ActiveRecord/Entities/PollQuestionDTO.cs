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
    [ActiveRecord("PollQuestions")]
    public class PollQuestionDTO
    {
        public PollQuestionDTO()
        {
            this.Id = -1;
        }

        [PrimaryKey(PrimaryKeyType.Identity, "PollQuestionId", UnsavedValue = "-1")]
        public int Id { get; set; }

        [Property("QuestionText")]
        public String QuestionText { get; set; }

        [Property("Title")]
        public String Title { get; set; }

        [HasMany(typeof(PollOptionDTO), Inverse=true)]
        public IList<PollOptionDTO> Options { get; set; }
    }
}
