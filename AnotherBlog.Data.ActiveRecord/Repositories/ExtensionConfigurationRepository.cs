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
using System.Data;

using NHibernate;
using NHibernate.Transform;
using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AnotherBlog.Common.Data;
using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.ActiveRecord.Entities;

namespace AnotherBlog.Data.ActiveRecord.Repositories
{
    public class ExtensionConfigurationRepository : ActiveRecordRepository<ExtensionConfiguration, ExtensionConfigurationDTO, IExtensionConfiguration>, IExtensionConfigurationRepository
    {
        public ExtensionConfigurationRepository(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
            : base(unitOfWork, repositoryManager)
        {
        }

        public ExtensionConfiguration GetByConfigurationId(int configurationId)
        {
            return this.GetByProperty("ConfigurationId", configurationId);
        }

        public ExtensionConfiguration GetByExtensionId(int extensionId)
        {
            return this.GetByProperty("ExtensionId", extensionId);
        }

        public ExtensionConfiguration GetByExtensionIdAndBlog(int extensionId, int blogId)
        {
            return this.GetByProperty("ExtensionId", extensionId, blogId);
        }
    }
}
