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
        public string FileName { get; set; }
        public IList<IList<string>> HeaderPrefix { get; set; }
        public IList<string> ColumnHeaders { get; set; }
        public IList<Dictionary<string, string>> DataRows { get; set; }
        public TableStyle TableStyle { get; set; }
        public TableItemStyle HeaderStyle { get; set; }
        public TableItemStyle ItemStyle { get; set; }

        public ExcelResult(IList<IList<string>> headerPrefix, IList<string> columnHeaders, IList<Dictionary<string, string>> dataRows, string fileName)
            : this(headerPrefix, columnHeaders, dataRows, fileName, null, null, null)
        { }

        public ExcelResult(IList<IList<string>> headerPrefix, IList<string> columnHeaders, IList<Dictionary<string, string>> dataRows, string fileName, TableStyle tableStyle, TableItemStyle headerStyle, TableItemStyle itemStyle)
        {
            this.HeaderPrefix = headerPrefix;
            this.DataRows = dataRows;
            this.FileName = fileName;
            this.ColumnHeaders = columnHeaders;
            this.TableStyle = tableStyle;
            this.HeaderStyle = headerStyle;
            this.ItemStyle = itemStyle;

            // provide defaults  
            if (this.TableStyle == null)
            {
                this.TableStyle = new TableStyle();
                this.TableStyle.BorderStyle = BorderStyle.Solid;
                this.TableStyle.BorderColor = Color.Black;
                this.TableStyle.BorderWidth = Unit.Pixel(1);
            }

            if (this.HeaderStyle == null)
            {
                this.HeaderStyle = new TableItemStyle();
                this.HeaderStyle.BackColor = Color.LightGray;
            }

            if (this.ItemStyle == null)
            {
                this.ItemStyle = new TableItemStyle();
                this.ItemStyle.BorderStyle = BorderStyle.Solid;
                this.ItemStyle.BorderColor = Color.Black;
                this.ItemStyle.BorderWidth = Unit.Pixel(1);
            }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            string retVal = "<?xml version=\"1.0\"?>";
            retVal += "<ss:Workbook xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">";
            retVal += "<ss:Worksheet ss:Name=\"Sheet1\">";
            retVal += "<ss:Table>";

            retVal += "<ss:Column ss:Width=\"200\" />";

            for (int i = 1; i < this.ColumnHeaders.Count; i++)
            {
                retVal += "<ss:Column ss:Width=\"60\" />";
            }

            for (int i = 0; i < this.HeaderPrefix.Count; i++)
            {
                retVal += "<ss:Row>";

                for (int j = 0; j < this.HeaderPrefix[i].Count; j++)
                {
                    retVal += "<ss:Cell>";

                    if (j == 0)
                    {
                        retVal += "<ss:Data ss:Type=\"String\"><B>" + this.HeaderPrefix[i][j] + "</B></ss:Data>";
                    }
                    else
                    {
                        retVal += "<ss:Data ss:Type=\"String\">" + this.HeaderPrefix[i][j] + "</ss:Data>";
                    }
                    retVal += "</ss:Cell>";
                }

                retVal += "</ss:Row>";
            }

            retVal += "<ss:Row></ss:Row>";
            retVal += "<ss:Row>";

            foreach (string header in this.ColumnHeaders)
            {
                retVal += "<ss:Cell>";
                retVal += "<ss:Data ss:Type=\"String\"><B>" + header + "</B></ss:Data>";
                retVal += "</ss:Cell>";
            }

            retVal += "</ss:Row>";

            for (int i = 0; i < this.DataRows.Count; i++)
            {
                retVal += "<ss:Row>";

                foreach (string header in this.ColumnHeaders)
                {
                    string strValue = string.Empty;

                    if (this.DataRows[i].ContainsKey(header))
                    {
                        strValue = this.DataRows[i][header];
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

            WriteFile(this.FileName, "application/ms-excel", retVal);
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
            context.Response.Charset = string.Empty;
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.ContentType = contentType;
            context.Response.Write(content);
            context.Response.End();
        }
    }
}