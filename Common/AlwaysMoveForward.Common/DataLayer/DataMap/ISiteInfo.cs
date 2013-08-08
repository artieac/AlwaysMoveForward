using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.Common.DataLayer.DataMap
{
    public interface ISiteInfo
    {
        int SiteId { get; set; }
        string About { get; set; }
        string Name { get; set; }
        string ContactEmail { get; set; }
        string DefaultTheme { get; set; }
        string SiteAnalyticsId { get; set; }
    }
}
