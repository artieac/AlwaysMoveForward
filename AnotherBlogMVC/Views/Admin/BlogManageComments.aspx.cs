using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnotherBlog.MVC.Views.Admin
{
    public class BlogManageComments : ViewPage<AnotherBlog.MVC.Models.Admin.BlogAdminModel>
    {
        public string GenerateFilterOption(string optionName, string selectedOption)
        {
            string retVal = "<option";
            retVal += " id='" + optionName + "'";
            retVal += " name='" + optionName + "'";
            retVal += " value='" + optionName + "'";

            if (optionName == selectedOption)
            {
                retVal += " selected";
            }

            retVal += ">";
            retVal += optionName;
            retVal += "</option>";
            return retVal;
        }

        public string GenerateFilterText(int commentStatus)
        {
            string retVal = "Unapproved";

            switch (commentStatus)
            {
                case 0:
                    retVal = "Unapproved";
                    break;
                case 1:
                    retVal = "Approved";
                    break;
                case 2:
                    retVal = "Deleted";
                    break;
            }

            return retVal;
        }
    }
}
