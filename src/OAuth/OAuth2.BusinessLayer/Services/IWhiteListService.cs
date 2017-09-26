using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.OAuth2.Common.Configuration;

namespace AlwaysMoveForward.OAuth2.BusinessLayer.Services
{
    public interface IWhiteListService
    {
        bool AllowUnauthorizedAccess(Uri queryString, WhiteListConfiguration configuration);

        bool AllowUnauthorizedAccessToFolders(Uri queryString, string[] folderList);

        bool AllowUnauthorizedAccessToFolder(Uri queryString, string folder);

        bool AllowUnauthorizedAccessToFileTypes(Uri queryString, string[] fileTypeList);
    }
}
