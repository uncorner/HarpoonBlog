using System.Data.Entity.ModelConfiguration;
using Harpoon.Core.Entities;

namespace Harpoon.Infrastructure.Ef.Mapping
{
    public class ChapterMapping : EntityTypeConfiguration<Chapter>
    {
        public ChapterMapping()
        {
            Map(m =>
                {
                    m.ToTable("Chapters");
                    m.MapInheritedProperties();
                });

            Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            Property(e => e.TagName)
                .IsRequired()
                .HasMaxLength(50);
            
            Property(e => e.OrderValue)
                .IsRequired();
            
        }
        
    }
}