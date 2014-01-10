using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class SiteInfoDataMap : DataMapBase<SiteInfo, SiteInfoDTO>
    {
        public override SiteInfo MapProperties(SiteInfoDTO source, SiteInfo destination)
        {
            SiteInfo retVal = destination;

            if (source != null)
            {
                if (retVal == null)
                {
                    retVal = new SiteInfo();
                }
                retVal.About = source.About;
                retVal.ContactEmail = source.ContactEmail;
                retVal.DefaultTheme = source.DefaultTheme;
                retVal.Name = source.Name;
                retVal.SiteAnalyticsId = source.SiteAnalyticsId;
                retVal.SiteId = source.SiteId;
            }

            return retVal;
        }

        public override SiteInfoDTO MapProperties(SiteInfo source, SiteInfoDTO destination)
        {
            throw new NotImplementedException();
        }
    }
}
