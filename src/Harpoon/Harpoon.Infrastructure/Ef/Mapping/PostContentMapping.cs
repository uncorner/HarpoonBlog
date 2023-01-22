using System.Data.Entity.ModelConfiguration;
using Harpoon.Core.Entities;

namespace Harpoon.Infrastructure.Ef.Mapping
{
    public class PostContentMapping : EntityTypeConfiguration<PostContent>
    {
        public PostContentMapping()
        {
            Property(e => e.Data)
                .IsRequired();
        }
    }
}