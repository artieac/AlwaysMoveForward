<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AnotherBlog.Common.Data.Entities.BlogPost>" %>
<%@ Import Namespace="AnotherBlog.MVC.Views.Plugins.Sharing" %>
<%  if (Model != null)
     {
         for (int i = 0; i < SharingFactory.SharingSitesList.Length; i++)
         {
             if (i > 0)
             {
                 Response.Write(" ");
             }
             
             Response.Write(SharingFactory.GenerateSharingHtml(SharingFactory.SharingSitesList[i], this.Context.Request.Url.ToString(), ViewData.Model.Title));
         }
     } %>                    
