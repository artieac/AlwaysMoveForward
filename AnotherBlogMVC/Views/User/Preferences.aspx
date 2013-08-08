<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AnotherBlog.MVC.Models.UserModel>" %>
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
            <label><%= ViewData.Model.ContentTitle %></label>
        </div> 
        <div class="editAreaContainer">
            <div class="editAreaSubContainer">
                <br />
                <form action='/<%= ViewData["blogSubFolder"].ToString() %>/User/SavePreferences' method="post">
                    <table>
                        <tr>
                            <th class="editItemLabel">user name:</th>
                            <td><%= ViewData.Model.CurrentUser.UserName %></td>
                        </tr>
                        <tr>
                            <th  class="editItemLabel">password:</th>
                            <td><input type="password" id="password" name="password" /></td>
                        </tr>
                        <tr>
                            <th class="editItemLabel">email:</th>
                            <td><input type="text" id="email" name="email" value="<%= ViewData.Model.CurrentUser.Email %>" /></td>
                        </tr>
                        <tr>
                            <th class="editItemLabel">display name:</th>
                            <td><input type="text" id="displayName" name="displayName" value="<%= ViewData.Model.CurrentUser.DisplayName %>" /></td>
                        </tr>
                        <tr>
                            <th class="editItemLabel">about:</th>
                            <td><textarea class="textAreaInput" id="userAbout" name="userAbout" cols="2"><%= ViewData.Model.CurrentUser.About %></textarea></td>
                        </tr>
                    </table>
                    <input style="margin-left:25%" type="submit" ID="saveButton" value="Save"/>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
