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

using AnotherBlog.Common.Data;

namespace AnotherBlog.Common.Data.Repositories
{
    public interface IRepositoryManager
    {
        IUnitOfWork UnitOfWork { get; set; }
        IBlogEntryRepository BlogEntries{ get;}
        IBlogEntryTagRepository BlogEntryTags{ get;}
        IBlogExtensionRepository BlogExtensions{ get;}
        IBlogRepository Blogs{ get;}
        IBlogRollLinkRepository BlogLinks{ get;}
        IBlogUserRepository BlogUsers{ get;}
        IDbInfoRepository DbInfo{ get;}
        IEntryCommentRepository EntryComments{ get;}
        IExtensionConfigurationRepository ExtensionConfiguration { get; }
        IRoleRepository Roles{ get;}
        ISiteInfoRepository SiteInfo{ get;}
        ITagRepository Tags{ get;}
        IUserRepository Users{ get;}
    }
}
