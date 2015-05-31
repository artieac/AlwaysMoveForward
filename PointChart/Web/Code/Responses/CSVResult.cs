using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;

namespace AlwaysMoveForward.PointChart.Web.Code.Responses
{
    public class CSVResult : ActionResult
    {
        public CSVResult(IList<string> columnHeaders, IList<Dictionary<string, string>> dataRows, string fileName)
        {
            this.Rows = dataRows;
            this.FileName = fileName;
            this.ColumnHeaders = columnHeaders;
        }

        public string FileName { get; private set; }
        public IList<string> ColumnHeaders { get; private set; }
        public IList<Dictionary<string, string>> Rows { get; private set; }

        public override void ExecuteResult(ControllerContext context)
        {
            // Create HtmlTextWriter  
            StringWriter sw = new StringWriter();

            foreach (string header in this.ColumnHeaders)
            {
                sw.Write(header);
                sw.Write(",");
            }

            sw.WriteLine();

            for (int i = 0; i < this.Rows.Count; i++)
            {
                foreach (string header in this.ColumnHeaders)
                {
                    string strValue = string.Empty;

                    if (this.Rows[i].ContainsKey(header))
                    {
                        strValue = this.Rows[i][header];
                    }

                    strValue = ReplaceSpecialCharacters(strValue);

                    sw.Write(strValue);
                    sw.Write(",");
                }

                sw.WriteLine();
            }

            WriteFile(this.FileName, "application/ms-excel", sw.ToString());
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