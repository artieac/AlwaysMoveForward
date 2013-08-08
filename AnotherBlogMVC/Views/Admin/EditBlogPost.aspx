<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminTool.Master" Inherits="System.Web.Mvc.ViewPage<AnotherBlog.MVC.Models.Admin.BlogAdminModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContainer" runat="server">
    <script src="/Scripts/Admin/ManageBlogPosts.js" type="text/javascript"></script>
    <!-- TinyMCE -->
    <script type="text/javascript" src="/Content/tiny_mce/tiny_mce.js"></script>
    <script type="text/javascript">
        tinyMCE.init({
            // General options
            mode: "textareas",
            theme: "AnotherBlog"
        });

        setTimeout("ManageBlogPosts.ExecuteBlogEntryAutoSave()", 300000);
        
        
    </script>
    <!-- /TinyMCE -->
    <div class="bodyContent">        
        <div class="contentPageTitle">
            <label>Edit Blog Post</label>
        </div> 
        <div class="editAreaContainer">
            <div class="editAreaSubContainer">
                <br />
                <form id="adminForm" action="/Admin/EditBlogPost" method="post">
                    <input type="hidden" id="entryId" name="entryId" value="<%= ViewData.Model.BlogPost.EntryId.ToString() %>" />
                    <input type="hidden" id="blogSubFolder" name="blogSubFolder" value="<%= ViewData.Model.TargetBlog.SubFolder %>" />
                    <input type="hidden" id="performSave" name="performSave" value="true" />
                    <label style="text-align:left">title:</label>
                    <%
                        Response.Write("<input id='isPublished' name='isPublished' type='checkbox'");

                        if (ViewData.Model.BlogPost.IsPublished)
                        {
                            Response.Write(" checked ");
                        }

                        Response.Write(">is published</input>");
                    %>
                    <br />
                    <input class="blogEntryTextBox" type="text" id="title" name="title" value="<%= ViewData.Model.BlogPost.Title %>" maxlength="50"/>
                    <br />
                    <label style="text-align:left">entry</label>
                    <br />
                    <textarea class="textAreaInput" cols="20" rows="20" id="entryText" name="entryText"><%= ViewData.Model.BlogPost.EntryText%></textarea>
                    <br />
                    <% String tagsAsString = "";
                    if (ViewData.Model.BlogPost != null)
                    {
                        if (ViewData.Model.PostTags.Count > 0)
                        {
                            tagsAsString = ViewData.Model.PostTags[0].Name;
                        }

                        for (int i = 1; i < ViewData.Model.PostTags.Count; i++)
                        {
                            tagsAsString += "," + ViewData.Model.PostTags[i].Name;
                        }
                    }
                    %>
                    <label style="text-align:left">tags</label>
                    <br />
                    <input class="blogEntryTextBox" type="text" id="tagInput" name="tagInput" value="<%= tagsAsString %>"/>
                    <br />
                    <input type="submit" ID="saveButton" value="Save"/>
                </form>
                <div style="display:none">
                    <form id="blogEntryAjaxForm" action="/Admin/AjaxBlogPostSave" method="post">
                        <%if (ViewData.Model.BlogPost.IsPublished)
                          {%>
                            <input type="hidden" id="ajaxIsPublished" name="ajaxIsPublished" value="on" />
                      <%}
                          else
                          { %>
                            <input type="hidden" id="ajaxIsPublished" name="ajaxIsPublished" value="off" />
                      <%} %>

                        <input type="hidden" id="ajaxEntryId" name="ajaxEntryId" value="<%= ViewData.Model.BlogPost.EntryId.ToString() %>" />
                        <input type="hidden" id="ajaxTitle" name="ajaxTitle" value="<%= ViewData.Model.BlogPost.Title %>" />
                        <input type="hidden" id="ajaxEntryText" name="ajaxEntryText" value="<%= ViewData.Model.BlogPost.EntryText%>" />                        
                        <input type="hidden" id="ajaxTagInput" name="ajaxTagInput" value="<%= tagsAsString %>"/>
                    </form>
                </div>
                <div id="fileUploadSection">
                    <% Html.RenderPartial("~/Views/File/FileUpload.ascx", ViewData.Model.TargetBlog.SubFolder); %>
                </div>                
            </div>
        </div>
    </div>
</asp:Content>
