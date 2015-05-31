/**
 * Copyright (c) 2009 Arthur Correa.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Common Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/cpl1.0.php
 *
 * Contributors:
 *    Arthur Correa – initial contribution
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace AlwaysMoveForward.PointChart.Web.Code.Extensions
{
    public static class FilterExtension
    {
        #region HtmlHelper extensions

        public static string FilterOption(this HtmlHelper htmlHelper, string optionName, string selectedOption)
        {
            return GenerateFilterOption(optionName, selectedOption);
        }

        #endregion
        public static string GenerateFilterOption(string optionName, string selectedOption)
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

        public static string CommentStatusText(this HtmlHelper htmlHelper, int commentStatus)
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