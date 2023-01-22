using System.Data.Entity.ModelConfiguration;
using Harpoon.Core.Entities;

namespace Harpoon.Infrastructure.Ef.Mapping
{
    public class GuestCommentMapping : EntityTypeConfiguration<GuestComment>
    {
        public GuestCommentMapping()
        {
            ToTable("GuestComments");

            Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);

            Property(e => e.Email)
                .IsOptional()
                .HasMaxLength(50);

            Property(e => e.Email)
                .IsOptional()
                .HasMaxLength(50);
        }
    }
}