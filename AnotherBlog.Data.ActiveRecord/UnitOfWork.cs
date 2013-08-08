using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

using AnotherBlog.Common.Data;

namespace AnotherBlog.Data.ActiveRecord
{
    public class UnitOfWork : IUnitOfWork
    {
        TransactionScope currentTransaction;

        #region IUnitOfWork Members

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            if (currentTransaction == null)
            {
                currentTransaction = new TransactionScope(TransactionMode.New, isolationLevel, OnDispose.Commit);
            }
        }

        public void EndTransaction(bool canCommit)
        {
            if (currentTransaction != null)
            {
                if (canCommit)
                {
                    currentTransaction.VoteCommit();
                }
                else
                {
                    currentTransaction.VoteRollBack();
                }

                currentTransaction.Dispose();
                currentTransaction = null;
            }
        }

        public void Commit()
        {
            if (SessionScope.Current != null)
            {
                SessionScope.Current.Flush();
            }
        }


        #endregion
    }
}
