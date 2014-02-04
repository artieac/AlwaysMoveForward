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
using System.Text;

using NHibernate;
using NHibernate.Transform;
using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AlwaysMoveForward.Common.DomainModel.Poll;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;
using AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Repositories
{
    public class PollRepository : ActiveRecordRepository<PollQuestion, PollQuestionDTO>, IPollRepository
    {
        public PollRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override PollQuestion Create()
        {
            return new PollQuestion();
        }

        public override DataMapBase<PollQuestion, PollQuestionDTO> DataMapper
        {
            get { return new PollQuestionDataMap(); }
        }

        public override string IdPropertyName
        {
            get { return "Id"; }
        }

        public IList<PollQuestion> GetAllByActiveFlag(bool isActive)
        {
            DetachedCriteria criteria = DetachedCriteria.For<PollQuestionDTO>();
            criteria.Add(Expression.Eq("IsActive", isActive));
            return this.DataMapper.Map(Castle.ActiveRecord.ActiveRecordMediator<PollQuestionDTO>.FindAll(criteria));
        }

        public PollQuestion GetByPollOptionId(int pollOptionId)
        {
            PollQuestion retVal = null;

            DetachedCriteria criteria = DetachedCriteria.For<PollOptionDTO>();
            criteria.Add(Expression.Eq("Id", pollOptionId));
            PollOptionDTO foundOption = Castle.ActiveRecord.ActiveRecordMediator<PollOptionDTO>.FindOne(criteria);

            if (foundOption != null)
            {
                retVal = this.DataMapper.Map(foundOption.Question);
            }

            return retVal;
        }
    }
}
