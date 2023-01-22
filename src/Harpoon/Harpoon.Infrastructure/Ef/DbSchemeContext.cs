using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Harpoon.Infrastructure.Ef.Mapping;

namespace Harpoon.Infrastructure.Ef
{
    public class DbSchemeContext : DbContext
    {
        static DbSchemeContext()
        {
            // creating database off
            Database.SetInitializer<DbSchemeContext>(null);
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // lazy loading off
            Configuration.LazyLoadingEnabled = false;

            // cascade deleting off
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            // NOTE: order of mapping is an important
            modelBuilder.Configurations
                .Add(new TagMapping())
                .Add(new PostContentMapping())
                .Add(new PostMapping())
                .Add(new ArticleMapping())
                .Add(new ChapterMapping())
                .Add(new PersonalSettingMapping())
                .Add(new ImageMapping())
                .Add(new CommentMapping())
                .Add(new GuestCommentMapping());
        }

    }
}
