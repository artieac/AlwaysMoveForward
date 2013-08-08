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

namespace AlwaysMoveForward.Common.DataLayer.Repositories
{
    public interface IRepository<DomainType> where DomainType : class
    {
        IUnitOfWork UnitOfWork { get; set; }
        IRepositoryManager RepositoryManager { get; set; }

        DomainType Create();

        int UnsavedId { get; }
        string IdPropertyName{ get;}

        DomainType GetById(int itemId);
        DomainType GetById(int itemId, int blogId);
        DomainType GetByProperty(string idPropertyName, object idValue);
        DomainType GetByProperty(string idPropertyName, object idValue, int blogId);
        IList<DomainType> GetAll();
        IList<DomainType> GetAll(int blogId);
        IList<DomainType> GetAllByProperty(string idPropertyName, object idValue);
        IList<DomainType> GetAllByProperty(string idPropertyName, object idValue, int blogId);
        DomainType Save(DomainType itemToSave);
        bool Delete(DomainType itemToDelete);
        bool DeleteDependencies(DomainType parentItem);
    }
}
