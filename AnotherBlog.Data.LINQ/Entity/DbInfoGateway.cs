using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheOffWing.AnotherBlog.Core.Entity
{
    public class DbInfoGateway : GatewayBase
    {
        public DbInfoGateway(DataContextManager dataContext) : base(dataContext)
        {

        }

        public DbInfo GetDbInfo()
        {
            DbInfo retVal = null;

            try
            {
                retVal = (from foundItem in this.DataContext.DbInfos select foundItem).Single();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;
        }
    }
}
