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
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Core.Service
{
    public class TagService: ServiceBase
    {
        internal TagService(ServiceManager serviceManager)
            : base(serviceManager)
        {

        }

        public Tag Create()
        {
            return this.Repositories.Tags.CreateNewInstance();
        }

        public IList<Tag> GetAll(Blog targetBlog)
        {
            return Repositories.Tags.GetAll(targetBlog);
        }

        public IList GetAllWithCount(Blog targetBlog)
        {
            return Repositories.Tags.GetAllWithCount(targetBlog);
        }

        public Tag GetByName(string name, Blog targetBlog)
        {
            return Repositories.Tags.GetByName(name, targetBlog);
        }

        public IList<Tag> GetByNames(string[] names, Blog targetBlog)
        {
            return Repositories.Tags.GetByNames(names, targetBlog);
        }

        public IList<Tag> AddTags(Blog targetBlog, string[] names)
        {
            List<Tag> retVal = new List<Tag>();

            for (int i = 0; i < names.Length; i++)
            {
                string trimmedName = names[i].Trim();

                if (trimmedName != String.Empty)
                {
                    Tag currentTag = Repositories.Tags.GetByName(trimmedName, targetBlog);

                    if (currentTag == null)
                    {
                        currentTag = this.Create();
                        currentTag.Name = trimmedName;
                        currentTag.Blog = targetBlog;
                        Repositories.Tags.Save(currentTag);
                    }

                    retVal.Add(currentTag);
                }
            }

            return retVal;
        }

        public bool Delete(Tag targetTag)
        {
            return this.Repositories.Tags.Delete(targetTag);
        }
    }
}
