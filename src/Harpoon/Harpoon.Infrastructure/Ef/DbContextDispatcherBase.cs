using System;
using System.Data.Entity;

namespace Harpoon.Infrastructure.Ef
{
    public abstract class DbContextDispatcherBase : IDisposable
    {
        private bool disposed;
        protected DbContext dbContext; 

        protected DbContextDispatcherBase(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public DbContext DbContext
        {
            get
            {
                CheckDisposed();
                return dbContext;
            }
        }

        #region Disposing

        protected abstract void DisposeDbContext();

        private void CheckDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        public void Dispose()
        {
            DisposeObject(true);
            GC.SuppressFinalize(this);
        }

        private void DisposeObject(bool fromDispose)
        {
            if (disposed)
            {
                return;
            }

            if (fromDispose)
            {
                DisposeDbContext();
                dbContext = null;
            }

            disposed = true;
        }

        #endregion

    }
}
