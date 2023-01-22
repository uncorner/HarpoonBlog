using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Harpoon.Core;

namespace Harpoon.Infrastructure.Ef
{
    public abstract class RepositoryBase<TEntity>
        where TEntity : class
    {

        public void Add(TEntity entity)
        {
            ArgumentHelper.EnsureNotNull("entity", entity);

            using (var dispatcher = GetDbContextDispatcher())
            {
                dispatcher.DbContext
                    .Set<TEntity>()
                    .AddOrUpdate(entity);
            }
        }

        public void Remove(TEntity entity)
        {
            ArgumentHelper.EnsureNotNull("entity", entity);

            using (var dispatcher = GetDbContextDispatcher())
            {
                dispatcher.DbContext
                    .Set<TEntity>()
                    .Remove(entity);
            }
        }

        public IEnumerable<TEntity> FetchAll()
        {
            using (var dispatcher = GetDbContextDispatcher())
            {
                return dispatcher.DbContext
                    .Set<TEntity>()
                    .ToList();
            }
        }

        #region Context managing
        
        protected DbContextDispatcherBase GetDbContextDispatcher()
        {
            if (UnitOfWork.HasDbContext())
            {
                var dbContext = UnitOfWork.GetDbContext();
                return new ExternalDbContextDispatcher(dbContext);
            }

            return new InternalDbContextDispatcher();
        }

        #endregion

    }
}
