<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AnotherBlog.MVC.Models.ModelBase>" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<% if(ViewData.Model.TargetBlog!=null) 
{%>
    <div class='guestLinkSection'>
        <div class='guestLinkSectionTitle'><%= ViewData.Model.BlogSubFolder %> Blog</div>
        <ul class='guestLinkList'>
            <li class='guestLinkItem'>
                <a href="/<%= ViewData.Model.BlogSubFolder %>/Blog/Index">Blog Posts</a>
            </li>
            <li class='guestLinkItem'>
                <a href="/<%= ViewData.Model.BlogSubFolder %>/Blog/About">About</a>
            </li>

            <% if (AnotherBlog.MVC.Utilities.Utils.IsUserInRole(this.Context.User, ViewData.Model.BlogSubFolder, Role.Administrator) == true || 
                   AnotherBlog.MVC.Utilities.Utils.IsUserInRole(this.Context.User, ViewData.Model.BlogSubFolder, Role.SiteAdministrator) == true ||
                   AnotherBlog.MVC.Utilities.Utils.IsUserInRole(this.Context.User, ViewData.Model.BlogSubFolder, Role.Blogger))
               { %>
                  <li class='guestLinkItem'>
                    <a href="/Admin/Index">Admin Tool</a>
                  </li>
            <% }%>
        </ul>
    </div>
<% }
   else
   {  
        if (AnotherBlog.MVC.Utilities.Utils.IsUserInRole(this.Context.User, ViewData.Model.BlogSubFolder, Role.Administrator) == true || 
            AnotherBlog.MVC.Utilities.Utils.IsUserInRole(this.Context.User, ViewData.Model.BlogSubFolder, Role.SiteAdministrator) == true ||
            AnotherBlog.MVC.Utilities.Utils.IsUserInRole(this.Context.User, ViewData.Model.BlogSubFolder, Role.Blogger))
        {%>
            <div class='guestLinkSection'>
                <div class='guestLinkSectionTitle'>Administer</div>
                <ul class='guestLinkList'>
                  <li class='guestLinkItem'>
                    <a href="/Admin/Index">Admin Tool</a>
                  </li>
                </ul>
            </div>
        <% }        
  }%>         
