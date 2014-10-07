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

namespace AlwaysMoveForward.Common.DataLayer.Repositories
{
    /// <summary>
    /// This class contains all the code to extract User data from the repository using LINQ
    /// </summary>
    /// <param name="dataContext"></param>
    public interface IUserRepository : IRepository<User, int>
    {
        User GetByUserName(string userName);
        User GetByUserNameAndPassword(string userName, string password);
        User GetByEmail(string userEmail);
        IList<User> GetBlogWriters(int blogId);
    }
}
