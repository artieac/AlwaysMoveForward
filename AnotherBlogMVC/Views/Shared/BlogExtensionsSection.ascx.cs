using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AnotherBlog.Core;

namespace AnotherBlog.MVC.Views.Shared
{
    public class BlogExtensionsSection : ViewUserControl<AnotherBlog.MVC.Models.ModelBase>
    {
        public String RenderControls()
        {
//            AnotherBlog.Extension.Views.Twitter.ShowControl showControl = new AnotherBlog.Extension.Views.Twitter.ShowControl("artieAC");
//            return showControl.RenderHTML();
            return "";
        }
    }
}
