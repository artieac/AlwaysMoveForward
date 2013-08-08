<%@ Control Language="C#" CodeBehind="BlogCalendar.ascx.cs" Inherits="AnotherBlog.MVC.Views.Shared.BlogCalendar" %>
<%@ Import Namespace="AnotherBlog.MVC" %>
<div class="contentSection">
    <div class="monthTitle">
        <% if (ViewData.Model.TargetBlog != null)
           {%>
                <a class="changeMonth" href="/<%= ViewData.Model.BlogSubFolder %>/Blog/Index/month/<%= this.GenerateDateFilter(ViewData.Model.TargetMonth.AddMonths(-1)) %>"> &lt; </a> 
                <a class="currentMonth" href="/<%= ViewData.Model.BlogSubFolder %>/Blog/Index/month/<%= this.GenerateDateFilter(ViewData.Model.TargetMonth) %>"><%= ViewData.Model.TargetMonth.ToString("MMMM")%></a>                 
                <a class="changeMonth" href="/<%= ViewData.Model.BlogSubFolder %>/Blog/Index/month/<%= this.GenerateDateFilter(ViewData.Model.TargetMonth.AddMonths(1)) %>"> &gt; </a>
           <%
            } 
            else
            {%>
                <a class="changeMonth" href="/Home/Index/month/<%= this.GenerateDateFilter(ViewData.Model.TargetMonth.AddMonths(-1)) %>"> &lt; </a>
                <a class="currentMonth" href="/Home/Index/month/<%= this.GenerateDateFilter(ViewData.Model.TargetMonth) %>"><%= ViewData.Model.TargetMonth.ToString("MMMM")%></a>
                <a class="changeMonth"href="/Home/Index/month/<%= this.GenerateDateFilter(ViewData.Model.TargetMonth.AddMonths(1)) %>"> &gt; </a>
          <%}%>
    </div> 
    <% 
    DateTime trackingDate = ViewData.Model.TargetMonth;
    trackingDate = trackingDate.AddDays(-(ViewData.Model.TargetMonth.Day - 1));

    switch (trackingDate.DayOfWeek)
    {
        case DayOfWeek.Sunday:
            break;
        case DayOfWeek.Monday:
            trackingDate = trackingDate.AddDays(-1);
            break;
        case DayOfWeek.Tuesday:
            trackingDate = trackingDate.AddDays(-2);
            break;
        case DayOfWeek.Wednesday:
            trackingDate = trackingDate.AddDays(-3);
            break;
        case DayOfWeek.Thursday:
            trackingDate = trackingDate.AddDays(-4);
            break;
        case DayOfWeek.Friday:
            trackingDate = trackingDate.AddDays(-5);
            break;
        case DayOfWeek.Saturday:
            trackingDate = trackingDate.AddDays(-6);
            break;
    }
    
    Response.Write("<table class=\"calendarDates\">");

    for (int weekIndex = 0; weekIndex < 6; weekIndex++)
    {
        Response.Write("<tr class=\"weekRow\">");

        for(int i= 0; i < 7; i++)
        {
            string cellHtml = "<td class=\"weekDayCell";
            
            if (trackingDate.Month == ViewData.Model.TargetMonth.Month)
            {
                cellHtml += " weekDayCellThisMonth";
            }
            else
            {
                cellHtml += " weekdayCellOtherMonth";
            }

            if (trackingDate.Date == DateTime.Now.Date)
            {
                cellHtml += " weekDayCellCurrentDay";
            }
            
            if (ViewData.Model.CurrentMonthBlogDates.Contains(trackingDate, new AnotherBlog.Core.Utilities.DateCompare()))
            {
                cellHtml += " weekdayCellSelected";
                cellHtml += "\">";

                if (ViewData.Model.TargetBlog != null)
                {
                    cellHtml += "<a href=\"/";
                    cellHtml += ViewData.Model.BlogSubFolder + "/Blog/Index/day/" + this.GenerateDateFilter(trackingDate) + "\">";
                    cellHtml += trackingDate.Day.ToString();
                    cellHtml += "</a></td>";
                }
                else
                {
                    cellHtml += "<a href=\"/";
                    cellHtml += "Home/Index/day/" + this.GenerateDateFilter(trackingDate) + "\">";
                    cellHtml += trackingDate.Day.ToString();
                    cellHtml += "</a></td>";
                }                    
            }
            else
            {
                cellHtml += "\">" + trackingDate.Day.ToString() + "</td>";
            }
            
            Response.Write(cellHtml);
            trackingDate = trackingDate.AddDays(1);                    
        }

        Response.Write("</tr>");
    }
    
    Response.Write("</table>");%>
</div>