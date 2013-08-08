<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IDictionary<string,string>>" %>
<%@ Import Namespace="System.Collections.Generic" %>
<div class="contentSection">
    <% 
        Response.Write("<script type=\"text/javascript\">");
        Response.Write("google_ad_client = '" + ViewData.Model["adClient"].ToString() + "';");
        Response.Write("google_ad_slot = '" + ViewData.Model["adSlot"].ToString() + "';");
        Response.Write("google_ad_width = '" + ViewData.Model["adWidth"].ToString() + "';"); 
        Response.Write("google_ad_height = '" + ViewData.Model["adHeight"].ToString() + "';");
        Response.Write("</script>");
    %>
    <script type="text/javascript" src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
    </script>            
</div>
