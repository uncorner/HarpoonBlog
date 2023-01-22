using System.Data.Entity.ModelConfiguration;
using Harpoon.Core.Entities;

namespace Harpoon.Infrastructure.Ef.Mapping
{
    public class CommentMapping : EntityTypeConfiguration<Comment>
    {
        public CommentMapping()
        {
            ToTable("Comments");

            Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(2000);

            Property(e => e.CreatedAt)
                .IsRequired();

            Property(e => e.IsDeleted)
                .IsRequired();
        }
        
    }
}