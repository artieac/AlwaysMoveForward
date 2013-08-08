<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SharingControl.ascx.cs" Inherits="AnotherBlog.Web.Views.Shared.SharingControl" %>
<% if (Model.BlogEntry != null)
   { %>
    <script type="text/javascript">
        digg_bgcolor = '#ff9900';
        digg_skin = 'icon';
        digg_window = 'new';
        digg_title = '<%= Model.BlogEntry.Title %>';
    </script>        
    <script src="http://digg.com/tools/diggthis.js" type="text/javascript"></script> 
    <a href="<%= this.RedditUrl %>" target="_blank"> <img src="http://www.reddit.com/static/spreddit5.gif" alt="submit to reddit" border="0" /></a>   
    <a href="<%= this.TechnoratiUrl %>" target="_blank"> <img border=0 src="/Content/images/technorati.gif" alt="submit to technorati" ></a>
    <a href="<%= this.StumbleUponUrl %>" target="_blank"> <img border=0 src="http://cdn.stumble-upon.com/images/16x16_su_solid.gif" alt="submit to stumbleupon"></a>
    <a href="<%= this.DeliciousUrl %>" target="_blank"> <img border=0 src="/Content/images/delicious.gif" alt="submit to delicious"></a>
<% } %>