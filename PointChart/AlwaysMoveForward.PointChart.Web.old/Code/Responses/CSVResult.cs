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
        private IList<string> columnHeaders;
        private string fileName;
        private IList<Dictionary<string, string>> dataRows;

        public string FileName
        {
            get { return fileName; }
        }

        public IList<Dictionary<string, string>> Rows
        {
            get { return dataRows; }
        }

        public CSVResult(IList<string> _columnHeaders, IList<Dictionary<string, string>> _dataRows, string _fileName)
        {
            dataRows = _dataRows;
            fileName = _fileName;
            columnHeaders = _columnHeaders;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            // Create HtmlTextWriter  
            StringWriter sw = new StringWriter();

            foreach (String header in columnHeaders)
            {
                sw.Write(header);
                sw.Write(",");
            }

            sw.WriteLine();

            for (int i = 0; i < dataRows.Count; i++)
            {
                foreach (string header in columnHeaders)
                {
                    string strValue = "";

                    if (dataRows[i].ContainsKey(header))
                    {
                        strValue = dataRows[i][header];
                    }

                    strValue = ReplaceSpecialCharacters(strValue);

                    sw.Write(strValue);
                    sw.Write(",");
                }

                sw.WriteLine();
            }

            WriteFile(fileName, "application/ms-excel", sw.ToString());
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