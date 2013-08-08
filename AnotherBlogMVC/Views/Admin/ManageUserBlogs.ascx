<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<AnotherBlog.MVC.Models.Admin.SiteAdminModel>" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<%  if (ViewData.Model.CurrentUser != null)
    { %>
        <input type="hidden" id="userId" name="userId" value="<%= ViewData.Model.CurrentUser.UserId %>" />
<%    
    if (ViewData.Model.CurrentUser.UserBlogs.Count > 0)
    {       
        %>
                <table class="tableBase">
                    <thead>
                        <tr class="tableHeader">
                            <td width="20%">Blog</td>
                            <td width="40%">Description</td>
                            <td width="20%">Role</td>
                            <td width="20%">Action</td>
                        </tr>
                    </thead>
                    <tbody>
                <% foreach (BlogUser userBlog in ViewData.Model.CurrentUser.UserBlogs)
                   { 
                %>
                    <tr>
                        <td><%= userBlog.Blog.Name%></td>
                        <td><%= userBlog.Blog.Description%></td>
                        <td><%= userBlog.UserRole.Name%></td>
                        <td><a href="/Admin/DeleteUserRole?blogId=<%= userBlog.Blog.BlogId %>&userId=<%= userBlog.User.UserId %>"><img src="/Content/images/action_delete.png" class="deleteComment" alt=""/></a></td>
                    </tr>
                <%
    }
            %>
            </tbody>                    
            </table>
        <%
    }
    }
%>