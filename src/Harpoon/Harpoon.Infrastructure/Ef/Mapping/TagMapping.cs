using System.Data.Entity.ModelConfiguration;
using Harpoon.Core.Entities;

namespace Harpoon.Infrastructure.Ef.Mapping
{
    public class TagMapping : EntityTypeConfiguration<Tag>
    {
        public TagMapping()
        {
            Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(50);

            Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
