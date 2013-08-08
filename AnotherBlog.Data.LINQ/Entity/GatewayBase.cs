using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using log4net;
using log4net.Config;

using TheOffWing.AnotherBlog.Core;

namespace TheOffWing.AnotherBlog.Core.Entity
{
    // a common base class for gateway objects.  It primarily provides
    // funcitonality for error logging as well as containing the reference to 
    // a DataContextManager instance.
    public class GatewayBase
    {
        private ILog logger;
        DataContextManager dataContext = null;

        public GatewayBase(DataContextManager _dataContext)
        {
            this.dataContext = _dataContext;
        }

        public BlogDbDataContext DataContext
        {
            get { return this.dataContext.DataContext; }
        }

        public void SubmitChanges()
        {
            this.DataContext.SubmitChanges();
        }

        public ILog Logger
        {
            get
            {
                if(logger==null)
                {
                    logger = LogManager.GetLogger(this.GetType());
                }

                return this.logger;
            }
        }
    }
}
