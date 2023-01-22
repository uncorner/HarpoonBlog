using System.Data.Entity;
using Harpoon.Infrastructure.Ef;

namespace Harpoon.Infrastructure
{
    public class FakeRepository : RepositoryBase<object>
    {
        public DbContext GetDbContext()
        {
            return GetDbContextDispatcher().DbContext;
        }

    }
}