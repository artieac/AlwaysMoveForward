using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using AnotherBlog.Core.Entity;

namespace AnotherBlog.Core
{
    public class UploadedFileManager : ServiceBase
    {
        public UploadedFileManager(ModelContext modelContext) : base(modelContext)
        {
        }

        public string UploadedFileRoot(Blog targetBlog)
        {
            return "Content/UploadedFiles/" + targetBlog.SubFolder.ToString(); 
        }

        public string GeneratePath(Blog targetBlog)
        {
            string retVal = AppDomain.CurrentDomain.BaseDirectory;

            DateTime pathGenerator = DateTime.Now;
            retVal += this.UploadedFileRoot(targetBlog) + "/" + pathGenerator.Year + "/" + pathGenerator.Month;

            return retVal;
        }

        public List<string> GetRecentUploadedFiles_LocalPaths(Blog targetBlog)
        {
            List<string> retVal = new List<string>();

            DateTime currentDate = DateTime.Now;
            string currentMonthPath = AppDomain.CurrentDomain.BaseDirectory + this.UploadedFileRoot(targetBlog) + "/" + currentDate.Year + "/" + currentDate.Month;

            if(Directory.Exists(currentMonthPath))
            {
                string[] currentMonthFiles = Directory.GetFiles(currentMonthPath);
                retVal.AddRange(new List<string>(currentMonthFiles));
            }

            DateTime lastMonth = currentDate.AddMonths(-1);
            string lastMonthPath = AppDomain.CurrentDomain.BaseDirectory + this.UploadedFileRoot(targetBlog) + "/" + lastMonth.Year + "/" + lastMonth.Month;
            
            if(Directory.Exists(lastMonthPath))
            {
                string[] lastMonthFiles = Directory.GetFiles(lastMonthPath);
                retVal.AddRange(new List<string>(lastMonthFiles));
            }

            return retVal;
        }

        public List<string> GetRecentUploadedFiles_RelativePaths(Blog targetBlog)
        {
            List<string> retVal = new List<string>();

            DateTime currentDate = DateTime.Now;
            string currentMonthPath = "/" + this.UploadedFileRoot(targetBlog) + "/" + currentDate.Year + "/" + currentDate.Month;

            if (Directory.Exists(currentMonthPath))
            {
                string[] currentMonthFiles = Directory.GetFiles(currentMonthPath);
                retVal.AddRange(new List<string>(currentMonthFiles));
            }

            DateTime lastMonth = currentDate.AddMonths(-1);
            string lastMonthPath = "/" + this.UploadedFileRoot(targetBlog) + "/" + lastMonth.Year + "/" + lastMonth.Month;

            if (Directory.Exists(lastMonthPath))
            {
                string[] lastMonthFiles = Directory.GetFiles(lastMonthPath);
                retVal.AddRange(new List<string>(lastMonthFiles));
            }

            return retVal;
        }

    }
}
