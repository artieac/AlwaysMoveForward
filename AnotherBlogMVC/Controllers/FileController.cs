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
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.IO;

using AnotherBlog.MVC.Models;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Core.Service;

namespace AnotherBlog.MVC.Controllers
{
    public class FileController : PublicController
    {
        public ActionResult Index()
        {
            // Add action logic here
            throw new NotImplementedException();
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator + "," + Role.Blogger)]
        public ActionResult Upload(string blogSubFolder, string blogEntryId)
        {
            ModelBase model = (ModelBase)this.InitializeDataModel(blogSubFolder, new ModelBase());

            foreach (string file in Request.Files)
            {
                HttpPostedFileBase uploadedFile = Request.Files[file] as HttpPostedFileBase;

                if (uploadedFile.ContentLength > 0)
                {
                    string targetPath = Services.UploadedFiles.GeneratePath(this.GetTargetBlog(blogSubFolder));

                    if (!Directory.Exists(targetPath))
                    {
                        Directory.CreateDirectory(targetPath);
                    }

                    string savedFileName = Path.Combine(targetPath, Path.GetFileName(uploadedFile.FileName));
                    uploadedFile.SaveAs(savedFileName);
                }
            }

            return View("FileUpload", "");
        }
    }
}
