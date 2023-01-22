using System.Data.Entity;

namespace Harpoon.Infrastructure.Ef
{
    public class ExternalDbContextDispatcher : DbContextDispatcherBase
    {
        private readonly DbContextConfigurationHolder configHolder;

        public ExternalDbContextDispatcher(DbContext dbContext) : base(dbContext)
        {
            configHolder = new DbContextConfigurationHolder(dbContext);
        }

        protected override void DisposeDbContext()
        {
            // >>> Don't dispose an external dbContext
            // Restore config
            configHolder.RestoreConfiguration(DbContext);
        }

    }
}
