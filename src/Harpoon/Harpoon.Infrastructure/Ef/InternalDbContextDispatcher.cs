namespace Harpoon.Infrastructure.Ef
{
    public class InternalDbContextDispatcher : DbContextDispatcherBase
    {
        public InternalDbContextDispatcher() : base(new DbSchemeContext())
        {
        }

        protected override void DisposeDbContext()
        {
            dbContext.Dispose();
        }
        
    }
}
