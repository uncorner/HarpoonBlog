using System.Data.Entity.ModelConfiguration;
using Harpoon.Core.Entities;

namespace Harpoon.Infrastructure.Ef.Mapping
{
    public class ImageMapping : EntityTypeConfiguration<Image>
    {
        public ImageMapping()
        {
            Property(e => e.Data)
                .IsRequired();

            Property(e => e.ContentType)
                .IsRequired()
                .HasMaxLength(50);
        }
        
    }
}