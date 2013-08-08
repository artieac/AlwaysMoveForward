<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminTool.Master" Inherits="System.Web.Mvc.ViewPage<AnotherBlog.MVC.Models.Admin.BlogAdminModel>" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<%@ Import Namespace="AnotherBlog.MVC.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContainer" runat="server">
    <!-- TinyMCE -->
    <script type="text/javascript" src="../../Content/tiny_mce/tiny_mce.js"></script>
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
            <label>Edit Blog Settings</label>
        </div> 
        <div class="editAreaContainer">
            <div class="editAreaSubContainer">
                <br />
                <form id="adminForm" action="/Admin/BlogPreferences" method="post">
                    <input type="hidden" id="blogSubFolder" name="blogSubFolder" value="<%= ViewData.Model.TargetBlog.SubFolder %>" />
                    <input type="hidden" id="performSave" name="performSave" value="true" />
                    <input type="hidden" id="defaultTheme" name="defaultTheme" value="<%= ViewData.Model.TargetBlog.Theme %>" />
                    <label>name:</label>&nbsp;<label><%= ViewData.Model.TargetBlog.Name%></label>
                    <br />
                    <label>welcome:</label>
                    <br />
                    <input type="text" id="blogWelcome" name="blogWelcome" value="<%= ViewData.Model.TargetBlog.WelcomeMessage %>"/>
                    <br />
                    <label>description:</label>
                    <br />
                    <input type="text" id="description" name="description" value="<%= ViewData.Model.TargetBlog.Description %>"/>
                    <br />
                    <label>about:</label>
                    <br />
                    <textarea class="textAreaInput" id="about" name="about" rows="20"><%= ViewData.Model.TargetBlog.About%></textarea>
                    <input style="margin-left:25%" type="submit" ID="saveButton" value="Save"/>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
