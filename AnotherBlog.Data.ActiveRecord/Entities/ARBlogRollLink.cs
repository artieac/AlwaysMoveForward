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

using CE=AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Data.ActiveRecord.Entities
{
    [ActiveRecord("BlogRollLinks")]
    public class ARBlogRollLink : CE.BlogRollLink
    {
        public ARBlogRollLink() : base()
        {

        }

        [PrimaryKey(PrimaryKeyType.Identity, "BlogRollLinkId")]
        public override int BlogRollLinkId{ get; set;}

        [Property("LinkName")]
        public override string LinkName{ get; set;}

        [Property("Url")]
        public override string Url{ get; set;}

        [BelongsTo("BlogId", Type=typeof(ARBlog))]
        public override CE.Blog Blog{ get; set;}
    }
}
