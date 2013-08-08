<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminTool.Master" Inherits="System.Web.Mvc.ViewPage<AnotherBlog.MVC.Models.Admin.SiteAdminModel>" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<%@ Import Namespace="AnotherBlog.MVC.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContainer" runat="server">
    <div class="bodyContent">        
        <div class="contentPageTitle">
            <label>Edit User</label>
        </div> 
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
        <div class="editAreaContainer">
            <div class="editAreaSubContainer">
                <br />
                <form action='/Admin/EditBlog' method="post">
                    <input type="hidden" id="savingBlog" name="savingBlog" value="savingBlog" />
                    <table>
                        <tr>
                            <th class="editItemLabel">name:</th>
                            <td><input type="text" name="blogName" id="blogName" value="<%= ViewData.Model.TargetBlog.Name %>" /></td>
                            <td><%= Html.ValidationMessage("blogName") %></td>
                        </tr>
                        <tr>
                            <th class="editItemLabel">description:</th>
                            <td><input type="text" name="blogDescription" id="blogDescription" value="<%= ViewData.Model.TargetBlog.Description %>" /></td>
                            <td><%= Html.ValidationMessage("blogDescription") %></td>
                        </tr>
                        <tr>
                            <th class="editItemLabel">subfolder:</th>
                            <td><input type="text" name="targetSubFolder" id="targetSubFolder" value="<%= ViewData.Model.TargetBlog.SubFolder %>" /></td>
                            <td><%= Html.ValidationMessage("blogDescription") %></td>
                        </tr>
                        <tr>
                            <th  class="editItemLabel">theme:</th>
                            <td colspan="2">
                                <select id="blogTheme" name="blogTheme">
                                    <% foreach(string themeDirectory in Utils.GetThemeDirectories())
                                       {
                                           Response.Write(Utils.GenerateSelectOption(themeDirectory, ViewData.Model.TargetBlog.Theme));
                                       }%>
                                </select> 
                            </td>
                        </tr>
                        <tr>
                            <th class="editItemLabel">welcome:</th>
                            <td><input type="text" name="blogWelcome" id="blogWelcome" value="<%= ViewData.Model.TargetBlog.WelcomeMessage %>" /></td>
                            <td></td>
                        </tr>
                        <tr>
                            <th class="editItemLabel" valign="top">about:</th>
                            <td colspan="2"><textarea class="textAreaInput" cols="2" rows="20" name="blogAbout" id="blogAbout"><%= ViewData.Model.TargetBlog.About %></textarea></td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right"><input type="submit" value="save" /></td>
                        </tr>
                    </table>
                    <input type="hidden" id="blogId" name="blogId" value="<%= ViewData.Model.TargetBlog.BlogId %>" />
                    <br />
                </form>
            </div>
        </div>
    </div>
</asp:Content>
