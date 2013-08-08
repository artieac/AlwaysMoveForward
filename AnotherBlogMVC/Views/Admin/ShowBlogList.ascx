<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AnotherBlog.MVC.Models.Admin.BlogListModel>" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<% if (ViewData.Model.CurrentList != null)
   {  %>
<div id="listItemsSection" >
    <form id="editBlogListItemForm" action="/Admin/EditBlogListItem" method="post">
        <input type="hidden" id="blogSubFolder" name="blogSubFolder" value="<%= ViewData.Model.TargetBlog.SubFolder %>"/>
        <input type="hidden" name="blogListId" value="<%= ViewData.Model.CurrentList.Id %>" />
        <input type="hidden" id="editListItemId" name="editListItemId" value="" />
        <input type="hidden" id="editListItemName" name="editListItemName" value="" />
        <input type="hidden" id="editListItemRelatedLink" name="editListItemRelatedLink" value="" />
        <input type="hidden" id="editListItemDisplayOrder" name="editListItemDisplayOrder" value="" />
    </form>
    <form id="addNewListItemForm" action="/Admin/AddBlogListItem" method="post">
        <input type="hidden" name="blogSubFolder" value="<%= ViewData.Model.TargetBlog.SubFolder %>" />
        <input type="hidden" name="blogListId" value="<%= ViewData.Model.CurrentList.Id %>" />
        <table class="tableBase">
            <thead>
                <tr class="tableHeader">
                    <td width="1%">&nbsp;</td>
                    <td width="20%">Name</td>
                    <td width="20%">Link</td>
                    <td width="5%">Display Order</td>
                    <td width="5%">&nbsp;</td>
                </tr>
            </thead>
            <tbody>
            <% 
    int rowCounter = 0;

    foreach (BlogListItem blogListItem in ViewData.Model.CurrentListItems)
    {
        if (rowCounter % 2 == 0)
        {
            Response.Write("<tr class='tableRow1'>");
        }
        else
        {
            Response.Write("<tr class='tableRow2'>");
        }

        rowCounter++;%>
                    <td><input type="radio" name="selectedListItem" onclick="ManageBlogLists.HandleBlogListItemSelection('<%= blogListItem.Id %>');"/></td>
                    <td><input type="text" id="listItemName<%= blogListItem.Id%>" name="listItemName<%= blogListItem.Id%>" class="readonly" value="<%= blogListItem.Name %>" disabled="true"/></td>
                    <td><input type="text" id="listItemRelatedLink<%= blogListItem.Id%>" name="listItemRelatedLink<%= blogListItem.Id%>" class="readonly" value="<%= blogListItem.RelatedLink %>" disabled="true"/></td>
                    <td><input type="text" id="listItemDisplayOrder<%= blogListItem.Id%>" name="listItemDisplayOrder<%= blogListItem.Id%>" class="readonly" value="<%= blogListItem.DisplayOrder %>" disabled="true"/></td>
                    <td><a class="readonly" href="/Admin/DeleteBlogListItem?listItemId=<%= blogListItem.Id %>&blogSubFolder=<%= ViewData.Model.TargetBlog.SubFolder %>&blogListId=<%= ViewData.Model.CurrentList.Id %>"><img src="/Content/images/action_delete.png" class="deleteList" alt=""/></a><input type="button" name="listItemSaveButton_<%= blogListItem.Id %>" class="readonly hidden" value="Save" onclick="ManageBlogLists.SaveBlogListItemEdit(<%= blogListItem.Id %>);"/></td>
                </tr>
            <%}%>
                <tr>
                    <td><input type="radio" name="selectedListItem" onclick="ManageBlogLists.HandleBlogListItemSelection('-1');"/></td>
                    <td><input type="text" id="newListItemName" name="newListItemName" /></td>
                    <td><input type="text" id="newListItemRelatedLink" name="newListItemRelatedLink" /></td>
                    <td><input type="text" id="newListItemDisplayOrder" name="newListItemDisplayOrder" /></td>
                    <td><input type="submit" value="Add Item" /></td>
                </tr>
            </tbody>
        </table>
    </form>
</div>
<%}%>