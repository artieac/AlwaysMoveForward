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
    public static class FileExtension
    {
        public enum FileType
        {
            Excel,
            CSV
        }

        public static ActionResult Excel(this Controller controller, IList<IList<string>> headerPrefix, IList<string> headers, IList<Dictionary<string, string>> rows, string fileName)
        {
            return new ExcelResult(headerPrefix, headers, rows, fileName, null, null, null);
        }

        public static ActionResult Excel(this Controller controller, IList<IList<string>> headerPrefix, IList<string> headers, IList<Dictionary<string, string>> rows, string fileName, TableStyle tableStyle, TableItemStyle headerStyle, TableItemStyle itemStyle)
        {
            return new ExcelResult(headerPrefix, headers, rows, fileName, tableStyle, headerStyle, itemStyle);
        }

        public static ActionResult CSV(this Controller controller, IList<string> headers, IList<Dictionary<string, string>> rows, string fileName)
        {
            return new CSVResult(headers, rows, fileName);
        }
    } 
}