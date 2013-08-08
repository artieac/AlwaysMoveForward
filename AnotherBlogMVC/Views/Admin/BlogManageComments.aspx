<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminTool.Master" CodeBehind="BlogManageComments.aspx.cs" Inherits="AnotherBlog.MVC.Views.Admin.BlogManageComments" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContainer" runat="server">
    <div class="bodyContent">        
        <div class="contentPageTitle">
            <label>Manage Comments</label>
        </div> 
        <div class="editAreaContainer">
            <div class="editAreaSubContainer">
                <br/>
                <div style="width:100%;text-align:right">
                    <form id="adminForm" action="/Admin/BlogManageComments" method="post">
                        <input type="hidden" id="blogSubFolder" name="blogSubFolder" value="<%= ViewData.Model.TargetBlog.SubFolder %>" />
                        <select id="commentFilter" name="commentFilter">
                            <%= this.GenerateFilterOption("All", ViewData.Model.CommentFilter) %>
                            <%= this.GenerateFilterOption("Approved", ViewData.Model.CommentFilter)%>
                            <%= this.GenerateFilterOption("Unapproved", ViewData.Model.CommentFilter)%>
                            <%= this.GenerateFilterOption("Deleted", ViewData.Model.CommentFilter)%>
                        </select>        
                        <input type="submit" value="refresh"/>
                    </form>
                </div>
                <div>
                    <table class="tableBase">
                        <thead>
                            <tr class="tableHeader">
                                <td width="20%">comment</td>
                                <td width="20%">author</td>
                                <td width="10%">date</td>
                                <td width="15%">status</td>
                                <td width="15%">action</td>
                            </tr>
                        </thead>
                        <tbody>
                        <% int rowCounter = 0;
                            
                           foreach (Comment blogComment in ViewData.Model.Comments)
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
                                <td width="20%"><%= blogComment.Text %></td>
                                <td width="20%"><%= blogComment.AuthorName %> - <%= blogComment.AuthorEmail %></td>
                                <td width="10%"><%= blogComment.DatePosted.ToShortDateString()%></td>
                                <td width="15%"><%= this.GenerateFilterText(blogComment.Status) %></td>
                                <td width="15%">
                                    <a href="/Admin/BlogApproveComment?blogSubFolder=<%= ViewData.Model.TargetBlog.SubFolder %>&id=<%= blogComment.CommentId %>"><img src="/Content/images/action_check.png" class="approveComment" alt="" /></a>
                                    <a href="/Admin/BlogDeleteComment?blogSubFolder=<%= ViewData.Model.TargetBlog.SubFolder %>&id=<%= blogComment.CommentId %>"><img src="/Content/images/action_delete.png" class="deleteComment" alt=""/></a>
                                </td>
                            </tr>
                        <%
                            }
                        %>
                        </tbody>
                    </table>
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>

