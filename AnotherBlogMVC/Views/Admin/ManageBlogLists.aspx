<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminTool.Master" Inherits="System.Web.Mvc.ViewPage<AnotherBlog.MVC.Models.Admin.BlogListModel>" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContainer" runat="server">
    <script src="/Scripts/Admin/ManageBlogLists.js" type="text/javascript"></script>
    <% Dictionary<String, String> readonlyAttributes = new Dictionary<string,string>();
       readonlyAttributes.Add("class", "readonly");
       readonlyAttributes.Add("disabled", "true");
    %>
    <form id="adminForm" action="/Admin/ManageBlogLists">
        <input type="hidden" id="blogSubFolder" name="blogSubFolder" value="<%= ViewData.Model.TargetBlog.SubFolder %>" />
        <input type="hidden" id="performSave" name="performSave" value="true" />        
    </form>
    <form id="editBlogListForm" action="/Admin/EditBlogList" method="post">
        <input type="hidden" id="blogSubFolder" name="blogSubFolder" value="<%= ViewData.Model.TargetBlog.SubFolder %>"/>
        <input type="hidden" id="listId" name="listId" value="" />
        <input type="hidden" id="listName" name="listName" value="" />
        <input type="hidden" id="showOrdered" name="showOrdered" value="" />
    </form>
    <form id="showBlogListForm" action="/Admin/ShowBlogList" method="post">
        <input type="hidden" id="Hidden2" name="blogSubFolder" value="<%= ViewData.Model.TargetBlog.SubFolder %>" />
        <input type="hidden" id="blogListId" name="blogListId" value="" />
    </form>
    <div class="bodyContent">        
        <div class="contentPageTitle">
            <label>Manage Lists</label>
        </div> 
        <div class="editAreaContainer">
            <div class="editAreaSubContainer">
                <br />
                <form id="addBlogList" action="/Admin/AddBlogList" method=post>
                    <input type="hidden" id="Hidden1" name="blogSubFolder" value="<%= ViewData.Model.TargetBlog.SubFolder %>" />
                    <input type="hidden" name="performSave" value="true" />
                    <table class="tableBase">
                        <thead>
                            <tr class="tableHeader">
                                <td width="1%">&nbsp;</td>
                                <td width="20%">Name</td>
                                <td width="5%">Show Ordered</td>
                                <td width="10%">&nbsp;</td>
                            </tr>
                        </thead>
                        <tbody>
                        <% 
                            int rowCounter = 0;
                         
                           foreach (BlogList blogList in ViewData.Model.BlogLists)
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
                                <td><input type="radio" name="selectedList" onclick="ManageBlogLists.HandleBlogListSelection('<%= blogList.Id %>');"/></td>
                                <td><input type="text" id="blogListName<%= blogList.Id %>" name="blogListName<%= blogList.Id%>" class="readonly" value="<%= blogList.Name %>" disabled="true"/></td>
                                <td><%= Html.CheckBox("blogListShowOrdered" + blogList.Id, blogList.ShowOrdered, readonlyAttributes) %></td>
                                <td><a class="readonly" href="/Admin/DeleteBlogList?listId=<%= blogList.Id %>&blogSubFolder=<%= ViewData.Model.TargetBlog.SubFolder %>"><img src="/Content/images/action_delete.png" class="deleteList" alt=""/></a><input type="button" name="blogListSaveButton_<%= blogList.Id %>" class="readonly hidden" value="Save" onclick="ManageBlogLists.SaveBlogList(<%= blogList.Id %>);"/></td>
                            </tr>
                        <%
                            }
                        %>
                        <tr>
                            <td><input type="radio" name="selectedList" onclick="ManageBlogLists.HandleBlogListSelection('-1');"/></td>
                            <td><input type="text" name="newListName"/></td>
                            <td><%= Html.CheckBox("newListShowOrdered", false) %></td>                            
                            <td><input type="submit" value="Add List" /></td> 
                        </tr>
                        </tbody>
                    </table>
                </form>
                <div id="listItemsContainer">
                </div>
            </div>
        </div>
    </div>
</asp:Content>


