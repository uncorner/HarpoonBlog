using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Harpoon.Core.Entities;

namespace Harpoon.Infrastructure.Ef.Mapping
{
    public class PostMapping : EntityTypeConfiguration<Post>
    {
        public PostMapping()
        {
            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.IsPublished)
                .IsRequired();

            Property(e => e.CreatedAt)
                .IsRequired();

            Property(e => e.PublishedAt)
                .IsOptional();

            Property(e => e.UpdatedAt)
                .IsOptional();

            Property(e => e.ContentId)
                .IsRequired();

            Property(e => e.IsDeleted)
                .IsRequired();

            HasRequired(e => e.Content);
        }
        
    }
}