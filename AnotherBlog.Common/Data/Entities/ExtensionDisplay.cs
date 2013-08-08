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

using AnotherBlog.Common.Data.Map;

namespace AnotherBlog.Common.Data.Entities
{
    public abstract class ExtensionDisplay
    {
        BlogExtensionDefinition controlDefinition;

        public ExtensionDisplay(BlogExtensionDefinition controlDefinition)
        {
            this.controlDefinition = controlDefinition;
        }

        public BlogExtensionDefinition ControlDefintion
        {
            get { return this.controlDefinition; }
        }

        public virtual string DisplayLinkText
        {
            get { return ""; }
        }

        public virtual bool HandleSubmission(int blogId, System.Collections.Specialized.NameValueCollection requestParams)
        {
            return false;
        }

        public virtual String GenerateHTML(int blogId, String submissionTarget)
        {
            return "";
        }
    }
}
