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

using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;

namespace AnotherBlog.Core
{
    public class DatabaseManager
    {
        public void UpdateDatabase(string databaseScriptPath)
        {
            //DbInfoRepository repository = new DbInfoRepository();
            //DbInfo dbInfo = repository.GetDbInfo();

            //if (dbInfo == null)
            //{
            //    this.CreateSchema(databaseScriptPath);
            //    this.InitializeData(databaseScriptPath);
            //}

            //this.UpdateSchema(databaseScriptPath);
        }

        protected void CreateSchema(string databaseScriptPath)
        {
//            string sqlScript = System.IO.File.ReadAllText(databaseScriptPath + "/AnotherBlogDb.sql");
//            contextManager.DataContext.ExecuteCommand(sqlScript);
        }

        protected void InitializeData(string databaseScriptPath)
        {
//            string initializationData = System.IO.File.ReadAllText(databaseScriptPath + "/AnotherBlogData.sql");
//            contextManager.DataContext.ExecuteCommand(initializationData);
        }

        protected void UpdateSchema(string databaseScriptPath)
        {

        }
    }
}
