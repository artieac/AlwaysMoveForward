using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnotherBlog.Common.Data.Map
{
    public interface ISiteInfo
    {
        int SiteId { get; set; }
        string About { get; set; }
        string Name { get; set; }
        string Url { get; set; }
        string ContactEmail { get; set; }
        string DefaultTheme { get; set; }
        string SiteAnalyticsId { get; set; }
    }
}
