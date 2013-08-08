using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

using log4net;
using log4net.Config;

using AnotherBlog.Core.Entity;

namespace AnotherBlog.Core
{
    public class ServiceBase
    {
        private ILog logger;
        protected ModelContext modelContext = null;

        public ServiceBase(ModelContext _modelContext)
        {
            this.modelContext = _modelContext;
        }

        public ModelContext ModelContext
        {
            get{ return this.modelContext;}
        }

        public ILog Logger
        {
            get
            {
                if (logger == null)
                {
                    logger = LogManager.GetLogger(this.GetType());
                }

                return this.logger;
            }
        }
    }
}
