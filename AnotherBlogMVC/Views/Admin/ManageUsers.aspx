<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminTool.Master" Inherits="System.Web.Mvc.ViewPage<AnotherBlog.MVC.Models.Admin.SiteAdminModel>" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<%@ Import Namespace="AnotherBlog.MVC.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContainer" runat="server">
    <script src="/Scripts/Admin/ManageUsers.js" type="text/javascript"></script>
    <div class="bodyContent">        
        <div class="contentPageTitle">
            <label>Manage Users</label>
        </div> 
        <div class="editAreaContainer">
            <div class="editAreaSubContainer">
                <br />
                <a href="/Admin/EditUser">Add User</a>
                <table class="tableBase">
                    <thead>
                        <tr class="tableHeader">
                            <td width="20%">User</td>
                            <td width="20%">Display Name</td>
                            <td width="20%">Email</td>
                            <td>&nbsp;</td>
                        </tr>
                    </thead>
                    <tbody>
                    <% int rowCounter = 0;
                        
                       foreach (User blogUser in ViewData.Model.Users)
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
                            <td><a href="/Admin/EditUser?userId=<%= blogUser.UserId.ToString() %>"><%= blogUser.UserName %></a></td>
                            <td><%= blogUser.DisplayName %></td>
                            <td><%= blogUser.Email %></td>
                            <td><a href="/Admin/DeleteUser?userId=<%= blogUser.UserId.ToString() %>"><img src="/Content/images/action_delete.png" class="deleteComment" alt=""/></a></td>
                        </tr>
                    <%
                        }
                    %>
                    </tbody>
                </table>
                <div class="pager">    
                    <%= Html.Pager(ViewData.Model.Users.PageSize, ViewData.Model.Users.PageNumber, ViewData.Model.Users.TotalItemCount, "ManageUsers", null)%>
                </div>
                <a href="/Admin/EditUser">Add User</a>
            </div>
        </div>
    </div>
</asp:Content>
