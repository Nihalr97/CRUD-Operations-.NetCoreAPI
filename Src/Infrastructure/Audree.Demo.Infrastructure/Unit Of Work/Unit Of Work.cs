using Audree.Demo.Core.Contracts.IUnitOfWork;
using Audree.Demo.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Audree.Demo.Infrastructure.Unit_Of_Work
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly Plcontextclass _databaseContext;
        private bool _disposed = false;

        public UnitOfWork(Plcontextclass databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Commit()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
            _databaseContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing && _databaseContext != null)
            {
                _databaseContext.Dispose();
            }
            _disposed = true;
        }
    }
}
