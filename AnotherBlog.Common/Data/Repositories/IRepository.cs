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

using AnotherBlog.Common.Data;
using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Common.Data.Repositories
{
    public interface IRepository<DomainType> where DomainType : class
    {
        IUnitOfWork UnitOfWork { get; set; }

        DomainType CreateNewInstance();
        string IdPropertyName{ get;}
        DomainType GetById(int itemId);
        DomainType GetById(int itemId, Blog targetBlog);
        IList<DomainType> GetAll();
        IList<DomainType> GetAll(Blog targetBlog);
        IList<DomainType> GetAllByProperty(string idPropertyName, object idValue);
        IList<DomainType> GetAllByProperty(string idPropertyName, object idValue, Blog targetBlog);
        DomainType GetByProperty(string idPropertyName, object idValue);
        DomainType Save(DomainType itemToSave);
        bool Delete(DomainType itemToDelete);
    }
}
