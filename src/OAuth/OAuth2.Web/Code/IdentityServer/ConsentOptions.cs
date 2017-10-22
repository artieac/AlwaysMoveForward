using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlwaysMoveForward.OAuth2.Web.Code.IdentityServer
{
    public class ConsentOptions
    {
        public static bool EnableOfflineAccess = true;

        public static string OfflineAccessDisplayName = "Offline Access";

        public static string OfflineAccessDescription = "Access to your applications and resources, even when you are offline";
    }
}
