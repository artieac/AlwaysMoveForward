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

using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Repositories
{
    public class BlogExtensionRepository : ActiveRecordRepository<BlogExtension, BlogExtensionDTO>, IBlogExtensionRepository
    {
        public BlogExtensionRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public override DataMapper.DataMapBase<BlogExtension, BlogExtensionDTO> DataMapper
        {
            get { throw new NotImplementedException(); }
        }

        public override string IdPropertyName
        {
            get { return "ExtensionId"; }
        }

        public BlogExtension GetByAssemblyName(string assemblyName)
        {
            return this.GetByProperty("AssemblyName", assemblyName);
        }
    }
}
