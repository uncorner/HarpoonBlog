using System.Data.Entity.Infrastructure;

namespace Harpoon.Infrastructure.Ef
{
    public static class DbQueryExtensions
    {
        public static DbQuery<TResult> Include<TResult>(this DbQuery<TResult> query, string path, bool needInclude)
        {
            return needInclude ? query.Include(path) : query;
        }
    }
    
}