using System;
using System.Data.Entity;
using Harpoon.Core;
using Harpoon.Infrastructure.Ef;

namespace Harpoon.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        [ThreadStatic]
        private static DbContext dbContext;

        private bool disposed;
        private bool isCommited;
        private readonly bool isExternalContext;

        public UnitOfWork()
        {
            if (dbContext == null)
            {
                dbContext = new DbSchemeContext();
            }
            else
            {
                isExternalContext = true;
            }
        }

        public bool IsCommited
        {
            get
            {
                return isCommited;
            }
        }

        public void Commit()
        {
            CheckDisposed();

            if (isCommited)
            {
                throw new InfrastructureException("UnitOfWork was commited");
            }

            dbContext.SaveChanges();
            isCommited = true;
        }

        public static bool HasDbContext()
        {
            return dbContext != null;
        }

        public static DbContext GetDbContext()
        {
            return dbContext;
        }

        #region Disposing

        private void CheckDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        ~UnitOfWork()
        {
            DisposeObject(false);
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
                // context disposing
                if (!isExternalContext && dbContext != null)
                {
                    dbContext.Dispose();
                    dbContext = null;
                }
            }

            disposed = true;
        }

        #endregion
    }
}
