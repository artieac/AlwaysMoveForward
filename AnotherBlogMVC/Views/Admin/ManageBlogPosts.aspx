<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminTool.Master" Inherits="System.Web.Mvc.ViewPage<AnotherBlog.MVC.Models.Admin.BlogAdminModel>" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<%@ Import Namespace="AnotherBlog.MVC.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContainer" runat="server">
    <script src="/Scripts/Admin/ManageBlogPosts.js" type="text/javascript"></script>
    <form id="adminForm" action="/Admin/ManageBlogPosts">
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
                        <th width="40%"><a href="/Admin/ManageBlogPosts?blogSubFolder=<%= ViewData.Model.TargetBlog.SubFolder %>&sortColumn=Title&sortAscending=<%= !ViewData.Model.SortAscending %>">Title</a></th>
                        <th width="20%"><a href="/Admin/ManageBlogPosts?blogSubFolder=<%= ViewData.Model.TargetBlog.SubFolder %>&sortColumn=Author&sortAscending=<%= !ViewData.Model.SortAscending %>">Author</a></th>
                        <th width="10%"><a href="/Admin/ManageBlogPosts?blogSubFolder=<%= ViewData.Model.TargetBlog.SubFolder %>&sortColumn=DateCreated&sortAscending=<%= !ViewData.Model.SortAscending %>">Date Created</a></th>
                        <th width="5%"><a href="/Admin/ManageBlogPosts?blogSubFolder=<%= ViewData.Model.TargetBlog.SubFolder %>&sortColumn=IsPublished&sortAscending=<%= !ViewData.Model.SortAscending %>">Is Published</a></th>
                        <th width="10%"><a href="/Admin/ManageBlogPosts?blogSubFolder=<%= ViewData.Model.TargetBlog.SubFolder %>&sortColumn=DatePosted&sortAscending=<%= !ViewData.Model.SortAscending %>">Date Posted</a></th>
                        <th width="5%"><a href="/Admin/ManageBlogPosts?blogSubFolder=<%= ViewData.Model.TargetBlog.SubFolder %>&sortColumn=TimesViewed&sortAscending=<%= !ViewData.Model.SortAscending %>">Times Viewed</a></th>
                        <th width="5%">Comments</th>
                        <th width="5%">Action</th>
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
                    <td><%= blogPost.TimesViewed %></td>
                    <td><%= blogPost.GetCommentCount().ToString()%></td>
                    <td><a href="/Admin/EditBlogPost?entryId=<%= blogPost.EntryId %>&blogSubFolder=<%= ViewData.Model.TargetBlog.SubFolder %>">edit</a></td>
                </tr>
                <%
                }       
                %>
            </table>
            <div class="pager">    
                <%= Html.Pager(ViewData.Model.EntryList.PageSize, ViewData.Model.EntryList.PageNumber, ViewData.Model.EntryList.TotalItemCount, "ManageBlogPosts", new { blogSubFolder = ViewData.Model.TargetBlog.SubFolder, sortColumn = ViewData.Model.SortColumn, sortAscending = ViewData.Model.SortAscending })%>
            </div>
            <a href="/Admin/EditBlogPost?blogSubFolder=<%= ViewData.Model.TargetBlog.SubFolder %>">Write new post</a>
        </div>
    </div>
</asp:Content>