<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminTool.Master" Inherits="System.Web.Mvc.ViewPage<AnotherBlog.MVC.Models.Admin.BlogAdminModel>" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContainer" runat="server">
    <div class="bodyContent">        
        <div class="contentPageTitle">
            <label>Manage Links</label>
        </div> 
        <div class="editAreaContainer">
            <div class="editAreaSubContainer">
                <br />
                <table class="tableBase">
                    <thead>
                        <tr class="tableHeader">
                            <td width="20%">Name</td>
                            <td width="20%">Link</td>
                        </tr>
                    </thead>
                    <tbody>
                    <% 
                        int rowCounter = 0;
                         
                       foreach (BlogRollLink blogLinkItem in ViewData.Model.BlogRoll)
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

                            <td><a href="<%= blogLinkItem.Url %>"><%= blogLinkItem.LinkName %></a></td>
                            <td><%= blogLinkItem.Url %></td>
                        </tr>
                    <%
                        }
                    %>
                    </tbody>
                </table>
                <form id="adminForm" action="/Admin/BlogManageLinks" method=post>
                    <input type="hidden" id="blogSubFolder" name="blogSubFolder" value="<%= ViewData.Model.TargetBlog.SubFolder %>" />
                    <input type="hidden" id="performSave" name="performSave" value="true" />
                    <span>link name</span>
                    <input type="text" id="linkName" name="linkName" />
                    <span>link:</span>
                    <input type="text" id="url" name="url" />
                    <input type="hidden" id="addingLink" name="addingLink" value="addingLink" />
                    <input type="submit" value="Add Link" />
                </form>
                <br />
            </div>
        </div>
    </div>
</asp:Content>

