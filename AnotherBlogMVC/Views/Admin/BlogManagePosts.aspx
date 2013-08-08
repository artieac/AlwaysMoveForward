<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminTool.Master" Inherits="System.Web.Mvc.ViewPage<AnotherBlog.MVC.Models.Admin.BlogAdminModel>" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<%@ Import Namespace="AnotherBlog.MVC.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContainer" runat="server">
    <form id="adminForm" action="/Admin/BlogManagePosts">
        <input type="hidden" id="blogSubFolder" name="blogSubFolder" value="<%= ViewData.Model.TargetBlog.SubFolder %>" />
        <input type="hidden" id="performSave" name="performSave" value="true" />        
    </form>
    <div class="bodyContent">        
        <div class="contentPageTitle">
            <label>Manage Blog Posts</label>
        </div> 
        <div>
            <a href="/Admin/EditBlogPost?blogSubFolder=<%= ViewData.Model.TargetBlog.SubFolder %>">Write new post</a>
            <table>
                <thead>
                    <tr>
                        <th width="40%">Title</th>
                        <th width="20%">Author</th>
                        <th width="10%">Date Created</th>
                        <th width="5%">Is Published</th>
                        <th width="10%">Date Posted</th>
                        <th width="5%">Comments</th>
                        <th width="10%">Action</th>
                    </tr>
                </thead>
                <% 
                    int rowCounter = 0;

                    foreach(BlogPost blogPost in ViewData.Model.EntryList) 
                    { 
                           if (rowCounter % 2 == 0)
                           {
                               Response.Write("<tr class='tableRow1'>");
                           }
                           else
                           {
                               Response.Write("<tr class='tableRow2'>");
                           }

                           rowCounter++;
                    %>

                    <td><%= blogPost.Title %></td>
                    <td><%= blogPost.Author.DisplayName%></td>
                    <td><%= blogPost.DateCreated %></td>
                    <td><%= blogPost.IsPublished %></td>
                    <td><%= blogPost.DatePosted %></td>
                    <td><%= blogPost.GetCommentCount().ToString()%></td>
                    <td><a href="/Admin/EditBlogPost?entryId=<%= blogPost.EntryId %>&blogSubFolder=<%= ViewData.Model.TargetBlog.SubFolder %>">edit</a></td>
                </tr>
                <%
                }       
                %>
            </table>
            <div class="pager">    
                <%= Html.Pager(ViewData.Model.EntryList.PageSize, ViewData.Model.EntryList.PageNumber, ViewData.Model.EntryList.TotalItemCount, "BlogManagePosts", new { blogSubFolder = ViewData.Model.TargetBlog.SubFolder })%>
            </div>
            <a href="/Admin/EditBlogPost?blogSubFolder=<%= ViewData.Model.TargetBlog.SubFolder %>">Write new post</a>
        </div>
    </div>
</asp:Content>
