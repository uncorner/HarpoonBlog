using System.Data.Entity;

namespace Harpoon.Infrastructure.Ef
{
    public class DbContextConfigurationHolder
    {
        private readonly bool autoDetectChangesEnabled;
        private readonly bool lazyLoadingEnabled;
        private readonly bool proxyCreationEnabled;
        private readonly bool validateOnSaveEnabled;

        public DbContextConfigurationHolder(DbContext dbContext)
        {
            autoDetectChangesEnabled = dbContext.Configuration.AutoDetectChangesEnabled;
            lazyLoadingEnabled = dbContext.Configuration.LazyLoadingEnabled;
            proxyCreationEnabled = dbContext.Configuration.ProxyCreationEnabled;
            validateOnSaveEnabled = dbContext.Configuration.ValidateOnSaveEnabled;
        }

        public void RestoreConfiguration(DbContext dbContext)
        {
            dbContext.Configuration.AutoDetectChangesEnabled = autoDetectChangesEnabled;
            dbContext.Configuration.LazyLoadingEnabled = lazyLoadingEnabled;
            dbContext.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
            dbContext.Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;
        }
    }
}
