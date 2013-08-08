<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminTool.Master" Inherits="System.Web.Mvc.ViewPage<AnotherBlog.MVC.Models.Admin.SiteAdminModel>" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<%@ Import Namespace="AnotherBlog.MVC.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContainer" runat="server">
    <div class="bodyContent">        
        <div class="contentPageTitle">
            <label>Edit Site Info</label>
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
                <form action='/Admin/EditSiteInfo' method="post">
                    <table>
                        <tr>
                            <th class="editItemLabel">name:</th>
                            <td><input type="text" id="siteName" name="siteName" value="<%= ViewData.Model.SiteInfo.Name %>" maxlength="50"/></td>
                            <td><%= Html.ValidationMessage("siteName") %></span></td>
                        </tr>
                        <tr>
                            <th class="editItemLabel">url:</th>
                            <td><input type="text" id="siteUrl" name="siteUrl" value="<%= ViewData.Model.SiteInfo.Url %>" maxlength="50"/></td>
                            <td><%= Html.ValidationMessage("siteUrl")%></span></td>
                        </tr>
                        <tr>
                            <th  class="editItemLabel">contact:</th>
                            <td><input type="text" id="siteContact" name="siteContact" value="<%= ViewData.Model.SiteInfo.ContactEmail %>" maxlength="50"/></td>
                            <td><%= Html.ValidationMessage("siteContact")%></span></td>
                        </tr>
                        <tr>
                            <th  class="editItemLabel">site analytics:</th>
                            <td><input type="text" id="siteAnalytics" name="siteAnalyticsId" value="<%= ViewData.Model.SiteInfo.SiteAnalyticsId %>" maxlength="50"/></td>
                            <td><%= Html.ValidationMessage("siteAnalyticsId")%></span></td>
                        </tr>
                        <tr>
                            <th  class="editItemLabel">theme:</th>
                            <td colspan="2">
                                <select id="defaultTheme" name="defaultTheme">
                                    <% foreach(string themeDirectory in Utils.GetThemeDirectories())
                                       {
                                           Response.Write(Utils.GenerateSelectOption(themeDirectory, ViewData.Model.SiteInfo.DefaultTheme));
                                       }%>
                                </select> 
                            </td>
                        </tr>
                        <tr>
                            <th  class="editItemLabel">about:</th>
                            <td colspan="2"><textarea class="textAreaInput" cols="80" rows="20" id="siteAbout" name="siteAbout"><%= ViewData.Model.SiteInfo.About %></textarea></td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                <input type="submit" ID="saveButton" value="Save"/>                
                            </td>
                        </tr>
                    </table>
                    <br />
                </form>
            </div>
        </div>
    </div>
</asp:Content>
