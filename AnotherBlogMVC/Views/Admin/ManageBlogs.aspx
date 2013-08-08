<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/AdminTool.Master" Inherits="System.Web.Mvc.ViewPage<AnotherBlog.MVC.Models.Admin.SiteAdminModel>" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContainer" runat="server">
    <div class="bodyContent">        
        <div class="contentPageTitle">
            <label>Manage Blogs</label>
        </div> 
        <div class="editAreaContainer">
            <div class="editAreaSubContainer">
                <br />
                <a href="/Admin/EditBlog">Add Blog</a>
                <table class="tableBase">
                    <thead>
                        <tr class="tableHeader">
                            <td width="20%">Name</td>
                            <td width="20%">Description</td>
                        </tr>
                    </thead>
                    <tbody>
                    <% int rowCounter = 0;
                       
                       foreach (Blog blogItem in ViewData.Model.Blogs)
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
                            <td><a href="/Admin/EditBlog?blogId=<%= blogItem.BlogId.ToString() %>"><%= blogItem.Name %></a></td>
                            <td><%= blogItem.Description %></td>
                        </tr>
                    <%
                        }
                    %>
                    </tbody>
                </table>
                <a href="/Admin/EditBlog">Add Blog</a>
                <br />
            </div>
        </div>
    </div>        
</asp:Content>