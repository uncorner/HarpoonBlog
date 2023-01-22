using System.Data.Entity.ModelConfiguration;
using Harpoon.Core.Entities;

namespace Harpoon.Infrastructure.Ef.Mapping
{
    public class ArticleMapping : EntityTypeConfiguration<Article>
    {
        public ArticleMapping()
        {
            Map(m =>
                    {
                        m.ToTable("Articles");
                        m.MapInheritedProperties();
                    });

            Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);
            
            HasMany(e => e.Tags)
                .WithMany(t => t.Articles)
                .Map(m =>
                {
                    m.ToTable("ArticleTags");
                    m.MapLeftKey("ArticleId");
                    m.MapRightKey("TagId");
                });

            HasMany(e => e.Comments)
                .WithRequired()
                .HasForeignKey(c => c.ArticleId);

            Property(e => e.PreviewContentId)
                .IsRequired();

            HasRequired(e => e.PreviewContent);
        }
    }
}
