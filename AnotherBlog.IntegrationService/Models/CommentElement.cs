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
using System.Web;

namespace AnotherBlog.IntegrationService.Models
{
    public class CommentElement
    {
        public int CommentId { get; set; }
        public int Status { get; set; }
        public string Link { get; set; }
        public string AuthorEmail { get; set; }
        public string Text { get; set; }
        public string AuthorName { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
