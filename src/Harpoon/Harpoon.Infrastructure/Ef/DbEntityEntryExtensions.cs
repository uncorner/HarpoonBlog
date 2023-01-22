using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;

namespace Harpoon.Infrastructure.Ef
{
    public static class DbEntityEntryExtensions
    {

        public static DbCollectionEntry<TEntity, TElement> InitializedCollection<TEntity, TElement>(
            this DbEntityEntry<TEntity> entry,
            Expression<System.Func<TEntity, ICollection<TElement>>> navigationProperty)
            where TElement : class
            where TEntity : class
        {
            var collection = entry.Collection(navigationProperty);

            if (collection.CurrentValue == null)
            {
                collection.CurrentValue = new List<TElement>();
            }

            return collection;
        }

    }
}