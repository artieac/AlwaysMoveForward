<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminTool.Master" Inherits="System.Web.Mvc.ViewPage<AnotherBlog.MVC.Models.Admin.SiteAdminModel>" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContainer" runat="server">
    <!-- TinyMCE -->
    <script type="text/javascript" src="/Content/tiny_mce/tiny_mce.js"></script>
    <script type="text/javascript">
        tinyMCE.init({
            // General options
            mode: "textareas",
            theme: "AnotherBlog"
        });
    </script>
    <!-- /TinyMCE -->
    <div class="bodyContent">        
        <div class="contentPageTitle">
            <label>Edit User</label>
        </div> 
        <div class="editAreaContainer">
            <div class="editAreaSubContainer">
                <br />
                <form action="/Admin/EditUser" method="post">
                    <input type="hidden" name="performSave" value="true" />
                    <table>
                        <tr>
                            <th class="editItemLabel">user name:</th>
                            <td><input type="text" name="userName" id="userName" value="<%= ViewData.Model.CurrentUser.UserName %>" /></td>
                            <td><%= Html.ValidationMessage("userName") %></span></td>
                        </tr>
                        <tr>
                            <th  class="editItemLabel">password:</th>
                            <td><input type="password" name="password" id="password" /></td>
                        </tr>
                        <tr>
                            <th class="editItemLabel">email:</th>
                            <td><input type="text" name="email" id="email" value="<%= ViewData.Model.CurrentUser.Email %>" /></td>
                            <td><%= Html.ValidationMessage("email") %></span></td>
                        </tr>
                        <tr>
                            <th class="editItemLabel">display name:</th>
                            <td><input type="text" name="displayName" id="displayName" value="<%= ViewData.Model.CurrentUser.DisplayName %>" /></td>
                            <td><%= Html.ValidationMessage("displayName") %></span></td>
                        </tr>
                        <tr>                    
                            <th class="editItemLabel">site Administrator:</th>
                            <td><%= Html.CheckBox("isSiteAdmin", ViewData.Model.CurrentUser.IsSiteAdministrator) %></td>
                        </tr>
                        <tr>                    
                            <th class="editItemLabel">approved Commenter:</th>
                            <td><%= Html.CheckBox("approvedCommenter", ViewData.Model.CurrentUser.ApprovedCommenter) %></td>
                        </tr>
                        <tr>
                            <th class="editItemLabel">active:</th>
                            <td><%= Html.CheckBox("isActive", ViewData.Model.CurrentUser.IsActive)%></td>
                        </tr>
                        <tr>
                            <th class="editItemLabel">about:</th>
                            <td><textarea class="textAreaInput" id="userAbout" name="userAbout" cols="2"><%= ViewData.Model.CurrentUser.About %></textarea></td>
                        </tr>
                    </table>
                    <input type="hidden" name="userId" id="userId" value="<%= ViewData.Model.CurrentUser.UserId %>" />
                    <input style="margin-left:25%" type="submit" ID="saveButton" value="Save"/>
                </form>
                <br />
                <div id="userBlogsContainer" name="userBlogsContainer">
                    <form id="viewUserBlogs" action="/Admin/ManageUserBlogs" method="post">
                            <input type="hidden" name="userId" value="<%= ViewData.Model.CurrentUser.UserId %>" />
                    </form>
                     <script type="text/javascript">
                        EditUserInitializeUserBlogs();
                    </script>
                </div>
                <form id="userAddBlogForm" action="/Admin/AddUserBlog" method="post">
                    <label>Grant Access</label> 
                    <input type="hidden" name="userId" id="userId" value="<%= ViewData.Model.CurrentUser.UserId %>" />
                    <table>
                        <tr>
                            <th class="editItemLabel">blog:</th>
                            <td>
                                <select id="targetBlog" name="targetBlog">
                                <% foreach (Blog blogItem in ViewData.Model.Blogs)
                                   { 
                                %>  
                                    <option id="<%= blogItem.Name %>" name="<%= blogItem.Name %>" value="<%=blogItem.BlogId.ToString() %>"><%= blogItem.Name%></option>
                                <%
                                    }
                                %>  
                                </select>        
                            </td>
                        </tr>
                        <tr>
                            <th class="editItemLabel">role:</th>
                            <td>
                                <select id="blogRole" name="blogRole">
                                <% foreach (Role roleItem in ViewData.Model.Roles)
                                   { 
                                %>  
                                    <option id="<%= roleItem.Name %>" name="<%= roleItem.Name %>" value="<%=roleItem.RoleId.ToString() %>"><%= roleItem.Name%></option>
                                <%
                                    }
                                %>  
                                </select>        
                            </td>
                        </tr>
                    </table>
                    <br />
                    <input type="submit" id="submitAddNewBlog" value="Grant Blog Access"/>
                </form>
                <script type="text/javascript">
                    EditUserSetupUserBlogAjax();
                </script>
             </div>
        </div>
    </div>
</asp:Content>
