using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Core.Entity;

namespace AnotherBlog.Core
{
    public class ModelContext
    {
        DataContextManager dataContext = null;

        public ModelContext()
        {

        }

        public DataContextManager DataContext
        {
            get
            {
                if (this.dataContext == null)
                {
                    this.dataContext = new DataContextManager(EntityConfiguration.connectionString);
                }

                return this.dataContext;
            }
        }

        public User CurrentUser
        {
            get
            {
                User retVal = null;

                if (System.Threading.Thread.CurrentPrincipal != null)
                {
                    retVal = System.Threading.Thread.CurrentPrincipal as User;
                }

                return retVal;
            }
        }
    }
}
