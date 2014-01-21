using System;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;

namespace AlwaysMoveForward.PointChart.Web.Code.Responses
{
    public class ExcelResult : ActionResult
    {
        public string FileName{ get; set;}
        public IList<IList<String>> HeaderPrefix { get; set; }
        public IList<string> ColumnHeaders { get; set; }
        public IList<Dictionary<string, string>> DataRows { get; set; }
        public TableStyle TableStyle { get; set; }
        public TableItemStyle HeaderStyle { get; set; }
        public TableItemStyle ItemStyle { get; set; }

        public ExcelResult(IList<IList<String>> headerPrefix, IList<string> columnHeaders, IList<Dictionary<string, string>> dataRows, string fileName)
            : this(headerPrefix, columnHeaders, dataRows, fileName, null, null, null)
        { }

        public ExcelResult(IList<IList<String>> headerPrefix, IList<string> columnHeaders, IList<Dictionary<string, string>> dataRows, string fileName, TableStyle tableStyle, TableItemStyle headerStyle, TableItemStyle itemStyle)
        {
            HeaderPrefix = headerPrefix;
            DataRows = dataRows;
            FileName = fileName;
            ColumnHeaders = columnHeaders;
            TableStyle = tableStyle;
            HeaderStyle = headerStyle;
            ItemStyle = itemStyle;

            // provide defaults  
            if (TableStyle == null)
            {
                TableStyle = new TableStyle();
                TableStyle.BorderStyle = BorderStyle.Solid;
                TableStyle.BorderColor = Color.Black;
                TableStyle.BorderWidth = Unit.Pixel(1);
            }

            if (HeaderStyle == null)
            {
                HeaderStyle = new TableItemStyle();
                HeaderStyle.BackColor = Color.LightGray;
            }

            if (ItemStyle == null)
            {
                ItemStyle = new TableItemStyle();
                ItemStyle.BorderStyle = BorderStyle.Solid;
                ItemStyle.BorderColor = Color.Black;
                ItemStyle.BorderWidth = Unit.Pixel(1);
            }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            String retVal = "<?xml version=\"1.0\"?>";
            retVal += "<ss:Workbook xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">";
            retVal += "<ss:Worksheet ss:Name=\"Sheet1\">";
            retVal += "<ss:Table>";

            retVal += "<ss:Column ss:Width=\"200\" />";

            for (int i = 1; i < this.ColumnHeaders.Count; i++)
            {
                retVal += "<ss:Column ss:Width=\"60\" />";
            }

            for(int i = 0; i < HeaderPrefix.Count; i++)
            {
                retVal += "<ss:Row>";

                for (int j = 0; j < HeaderPrefix[i].Count; j++)
                {
                    retVal += "<ss:Cell>";

                    if (j == 0)
                    {
                        retVal += "<ss:Data ss:Type=\"String\"><B>" + HeaderPrefix[i][j] + "</B></ss:Data>";
                    }
                    else
                    {
                        retVal += "<ss:Data ss:Type=\"String\">" + HeaderPrefix[i][j] + "</ss:Data>";
                    }
                    retVal += "</ss:Cell>";
                }

                retVal += "</ss:Row>";
            }

            retVal += "<ss:Row></ss:Row>";
            retVal += "<ss:Row>";
            
            foreach (String header in ColumnHeaders)
            {
                retVal += "<ss:Cell>";
                retVal += "<ss:Data ss:Type=\"String\"><B>" + header + "</B></ss:Data>";
                retVal += "</ss:Cell>";
            }

            retVal += "</ss:Row>";

            for (int i = 0; i < DataRows.Count; i++)
            {
                retVal += "<ss:Row>";

                foreach (string header in ColumnHeaders)
                {
                    string strValue = "";

                    if (DataRows[i].ContainsKey(header))
                    {
                        strValue = DataRows[i][header];
                    }

                    strValue = ReplaceSpecialCharacters(strValue);

                    retVal += "<ss:Cell>";
                    retVal += "<ss:Data ss:Type=\"String\">" + strValue + "</ss:Data>";
                    retVal += "</ss:Cell>";
                }

                retVal += "</ss:Row>";
            }

            retVal += "</ss:Table>";
            retVal += "</ss:Worksheet>";
            retVal += "</ss:Workbook>";

            WriteFile(FileName, "application/ms-excel", retVal);
        }

        private static string ReplaceSpecialCharacters(string value)
        {
            value = value.Replace("’", "'");
            value = value.Replace("“", "\"");
            value = value.Replace("”", "\"");
            value = value.Replace("–", "-");
            value = value.Replace("…", "...");
            return value;
        }

        private static void WriteFile(string fileName, string contentType, string content)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
            context.Response.Charset = "";
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.ContentType = contentType;
            context.Response.Write(content);
            context.Response.End();
        }
    }
}